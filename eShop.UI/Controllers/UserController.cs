using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eShop.DataAccess;
using eShop.DataAccess.DTOs;
using eShop.DataAccess.IRepositories;
using eShop.DataAccess.Repositories;
using eShop.Model;
using eShop.UI.UIServices;
using eShop.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eShop.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly ICredentialRepository _credentialRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ICityRepository _cityRepository;

        private List<Country> countries = new List<Country>();
        public UserController(IUserRepository userRepository, ICredentialRepository credentialRepository, 
            ICountryRepository countryRepository, IStateRepository stateRepository, ICityRepository cityRepository)
        {
            this._userRepository = userRepository;
            this._credentialRepository = credentialRepository;
            this._countryRepository = countryRepository;
            this._stateRepository = stateRepository;
            this._cityRepository = cityRepository;
            this._userService = new UserService(_userRepository, _credentialRepository,
                _countryRepository,_stateRepository,_cityRepository);
            this.countries = _countryRepository.GetAll().ToList();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            //CountryServices countryServices = new CountryServices(_countryRepository);
            CreateUserVM createUserVM = new CreateUserVM();
            //List<Country> countries = countryServices.GetCountries().ToList();

            createUserVM.Countries = this.countries.Select(country => new SelectListItem
            {
                Text = country.Name,
                Value = country.Id.ToString(),
                Selected = false
            }).ToList();

            return View(createUserVM);
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserVM createUserVM, long cityId, long stateId, long countryId)
        {
            UserDTO userDto = new UserDTO();
            CreateUserVM userNull = new CreateUserVM();
            if (createUserVM!=null)
            {
                userDto.AddressNo1 = createUserVM.userDTO.AddressNo1;
                userDto.Email = createUserVM.userDTO.Email;
                userDto.AddressNo2 = createUserVM.userDTO.AddressNo2;
                userDto.FirstName = createUserVM.userDTO.FirstName;
                userDto.LastName = createUserVM.userDTO.LastName;
                userDto.Country = createUserVM.userDTO.Country;
                userDto.PhoneNumber = createUserVM.userDTO.PhoneNumber;
                userDto.VATNumber = createUserVM.userDTO.VATNumber;
                userDto.ZipCode = createUserVM.userDTO.ZipCode;
                userDto.Password = createUserVM.userDTO.Password;
                userDto.Username = createUserVM.userDTO.Username;
                userDto.Role = createUserVM.userDTO.Role!=null?createUserVM.userDTO.Role:Role.User;
            }
            ViewData["Errors"] = _userService.CreateUser(userDto, countryId, stateId, cityId);

            return View("CreateUser",
                new CreateUserVM()
                {
                    Countries = this.countries.Select(country => new SelectListItem
                    {
                        Text = country.Name,
                        Value = country.Id.ToString(),
                        Selected = false
                    }).ToList()
                });
        }

        [HttpGet]
        [Authorize]
        public IActionResult DetailsUser()
        {
            long idToNum = 0;
            long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier),out idToNum);

            var user = _userRepository.GetById(idToNum);

            return View(new UserDetailsVM() { 
                FirstName = user.FirstName,
                LastName = user.LastName,
                AddressNo1 = user.AddressNo1
            });
        }

        //Func<long,JsonResult> FeatchStateAndCity

        public JsonResult GetStateById(long Id)
        {
            List<State> states = new List<State>();

            states = Id != 0 ? _stateRepository.GetStateByCountryId(Id).ToList():new List<State>();
            states.Insert(0, new State { Id = 0, Name = "-" });
            return Json(new SelectList(states,"Id","Name"));
        }

        public JsonResult GetCityById(long Id)
        {
            List<City> cities = new List<City>();

            cities = Id != 0 ? _cityRepository.GetCitiesByStateId(Id).ToList() : new List<City>();
            cities.Insert(0, new City { Id = 0, Name = "-" });
            return Json(new SelectList(cities, "Id", "Name"));
        }
    }
}