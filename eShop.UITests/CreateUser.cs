using eShop.DataAccess;
using eShop.DataAccess.DTOs;
using eShop.DataAccess.IRepositories;
using eShop.DataAccess.Repositories;
using eShop.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace eShop.UITests
{
    [TestClass()]
    public class CreateUser
    {
        private UserDTO _userDto;
        //private static EshopDbContext DbContext = new EshopDbContext();
        //private readonly UserRepository userRepository = new UserRepository(DbContext);
        //private readonly CredentialRepository credentialRepository = new CredentialRepository(DbContext);


        [TestInitialize]
        public void Initialize()
        {
            _userDto = new UserDTO()
            {
                FirstName = "Panos",
                LastName = "Gkantis",
                AddressNo1 = "Smolenski",
                Email = "panik@gmail.com",
                PhoneNumber = "6970000000",
                Country = new Country(),
                VATNumber = "1514880",
                AddressNo2 = null,
                Password = "4425nikos!",
                Username = "nikolas"
            };
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldThrowExceptionIfUserNameExist()
        {
            //UserService userService = new UserService(userRepository, credentialRepository);
            UserDTO user = new UserDTO()
            {
                Username = "nikolas"
            };
            try
            {
                //userService.CreateUser(user);
            }
            catch(Exception ex)
            {
                Assert.AreEqual("Username Already Exist",ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowArgumentNullExceptionWhenModelIsNull()
        {
            //UserService userService = new UserService(userRepository, credentialRepository);
            try
            {
                //User user = new User();
                //userService.CreateUser(new UserDTO());
            }
            catch(ArgumentNullException ex)
            {
                Assert.AreEqual("User cannot be null model", ex.ParamName);
                throw;
            }
        }
    }
}
