using eShop.DataAccess.DTOs;
using eShop.DataAccess.AdditionalDetailsModels;
using eShop.DataAccess.IRepositories;
using eShop.DataAccess.Services;
using eShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess
{
    public class UserService
    {
        private readonly IUserUnitOfWork _userUnitOfWork;
        private readonly ICredentialUnitOfWork _credentialUnitOfWork;
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ICityRepository _cityRepository;
        public UserService(IUserUnitOfWork userUnitOfWork, ICredentialUnitOfWork _credentialUnitOfWork,
            ICountryRepository countryRepository, IStateRepository stateRepository, ICityRepository cityRepository, ICredentialUnitOfWork credentialUnitOfWork)
        {
            this._userUnitOfWork = userUnitOfWork;
            this._countryRepository = countryRepository;
            this._stateRepository = stateRepository;
            this._cityRepository = cityRepository;
            this._credentialUnitOfWork = credentialUnitOfWork;
        }

        public List<string> CreateUser(UserDTO userDTO, long countryId, long stateId, long cityId)
        {
            //TO-DO
            //να ελέγχω το password αν περιέχει τουλάχιστον έναν αριθμό, ένα γράμμα μικρό ή κεφαλαίο και έναν
            //χαρακτήρα(!_$)
            List<string> errors = new List<string>();
            CreateAccountErrorsModel accountErrorsModel = new CreateAccountErrorsModel();
            accountErrorsModel.ErrorMessage = errors;
            accountErrorsModel.IsValid = true;

            errors.Clear();

            if (userDTO.Role == Role.Admin && _userUnitOfWork.UserDbRepository.AdminExist())
            {
                errors.Add("Admin already exist in store. Invalid action.");
                accountErrorsModel.ErrorMessage = errors;
                accountErrorsModel.IsValid = false;
            }

            if (_credentialUnitOfWork.CredentialDbRepository.NameExist(userDTO.Username))
            {
                errors.Add("Username Already Exist");
                accountErrorsModel.ErrorMessage = errors;
                accountErrorsModel.IsValid = false;
            }
            if (String.IsNullOrWhiteSpace(userDTO.Password) || userDTO.Password.Length < 7)
            {
                errors.Add("Password cannot be null or less than 7 characters.");
                accountErrorsModel.ErrorMessage = errors;
                accountErrorsModel.IsValid = false;
            }
            if (countryId == 0)
            {
                errors.Add("Country cannot be null");
                accountErrorsModel.ErrorMessage = errors;
                accountErrorsModel.IsValid = false;
            }
            if (stateId == 0)
            {
                errors.Add("State cannot be null");
                accountErrorsModel.ErrorMessage = errors;
                accountErrorsModel.IsValid = false;
            }
            if (cityId == 0)
            {
                errors.Add("City cannot be null");
                accountErrorsModel.ErrorMessage = errors;
                accountErrorsModel.IsValid = false;
            }

            if (userDTO == null)
                throw new ArgumentNullException("Cannot pass empty user");

            if (!accountErrorsModel.IsValid)
                return accountErrorsModel.ErrorMessage;

            var passAndSalt = PasswordService.CreatePassword(userDTO.Password);
            var country = _countryRepository.GetById(countryId);
            var state = _stateRepository.GetById(stateId);
            var city = _cityRepository.GetById(cityId);
            if (passAndSalt.password != null && passAndSalt.salt != null)
            {
                Credential credential = new Credential()
                {
                    Password = passAndSalt.password,
                    Salt = passAndSalt.salt,
                    Username = userDTO.Username
                };

                try
                {
                    _credentialUnitOfWork.CredentialDbRepository.CreateEntity(credential);
                    var result = _credentialUnitOfWork.CredentialDbRepository.Save();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                User user = new User()
                {
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Email = userDTO.Email,
                    CredentialId = credential.Id,
                    Credential = credential,
                    PhoneNumber = userDTO.PhoneNumber,
                    AddressNo1 = userDTO.AddressNo1,
                    AddressNo2 = userDTO.AddressNo2,
                    VATNumber = userDTO.VATNumber,
                    CountryId = country.Id,
                    Country = country,
                    StateName = state.Name,
                    CityName = city.Name,
                    ZipCode = userDTO.ZipCode,
                    IsActiveAccount = false,
                    Role = userDTO.Role!=null ? userDTO.Role : Role.User
                };

                if (user == null)
                    throw new ArgumentNullException("User cannot be null model");
                try
                {
                    _userUnitOfWork.UserDbRepository.CreateEntity(user);
                    _userUnitOfWork.UserDbRepository.Save();
                }
                catch (Exception ex)
                {
                    _credentialUnitOfWork.CredentialDbRepository.DeleteEntity(credential.Id);
                    _credentialUnitOfWork.CredentialDbRepository.Save();
                    throw new Exception(ex.Message);
                }

                //var res = await GmailSendEmail.AccessTokenCreator();
                //if (res.Count() > 0)
                //{
                //    GmailSendEmail.SendMailWithXOAUTH2(res[1], res[0], user.Email);
                //}
                //var res = GmailSendEmail.SetupEmail(user.Email, 
                //                                    "Your register was successful!", 
                //                                    "Thanks for registration to our site");
            }
            return accountErrorsModel.ErrorMessage;
        }

        //Tha epistrefei User
        public User UpdateUser(UserUpdateDTO userUpdate)
        {
            var userToUpd = _userUnitOfWork.UserDbRepository.GetById(userUpdate.UserId);

            if (userToUpd == null)
                throw new ArgumentNullException("Empty User");

            try
            {
                userToUpd.FirstName = userUpdate.FirstName;
                userToUpd.Email = userUpdate.Email;
                userToUpd.AddressNo1 = userUpdate.AddressNo1;
                userToUpd.AddressNo2 = userUpdate.AddressNo2;
                userToUpd.LastName = userUpdate.LastName;
                userToUpd.VATNumber = userUpdate.VATNumber;
                userToUpd.PhoneNumber = userUpdate.PhoneNumber;
                userToUpd.Country = userUpdate.Country;

                _userUnitOfWork.UserDbRepository.UpdateEntity(userToUpd);
                _userUnitOfWork.UserDbRepository.Save();

                return userToUpd;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured during update");
            }
        }

        public int DeleteUser(long userId)
        {
            var entityForDelete = _userUnitOfWork.UserDbRepository.GetById(userId);
            if (entityForDelete == null)
                throw new ArgumentNullException("Empty User");

            _userUnitOfWork.UserDbRepository.DeleteEntity(userId);

            return _userUnitOfWork.UserDbRepository.Save();
        }

    }
}
