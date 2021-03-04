using eShop.DataAccess;
using eShop.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.UITests
{
    [TestClass()]
    public class UserRepositoryTest
    {
        private UserRepository userRepository;
        private EshopDbContext dbContext = new EshopDbContext();
        [TestInitialize]
        public void Initialize()
        {
            userRepository = new UserRepository(dbContext);
        }

        [TestMethod]
        public void ReturnTrueIfNameExist()
        {
            string name = "nikolas";
            Assert.IsTrue(userRepository.NameExist(name));
        }

        [TestMethod]
        public void ReturnFalseIfNameDoesntExist()
        {
            string name = "didimos1998";
            Assert.IsFalse(userRepository.NameExist(name));
        }

        [TestMethod]
        public void CheckEmailExist()
        {
            string email = "autos_that@hot.com";
            Assert.IsNotNull(userRepository.GetByEmail(email));

            string emaildontExist = "aaa@hot.com";
            Assert.IsNull(userRepository.GetByEmail(emaildontExist));
        }

        [TestMethod]
        public void CheckGetByAddress()
        {
            string address = "Nikis 10";
            Assert.IsNotNull(userRepository.GetByAddress(address));

            address = "smolenski 14";
            Assert.IsNull(userRepository.GetByAddress(address));
        }

        [TestMethod]
        public void CheckGetByName()
        {
            string name = "Nikos Gkour";
            Assert.IsNotNull(userRepository.GetByName(name));

            name = "Agnwstos X";
            Assert.IsNull(userRepository.GetByName(name));
        }

        [TestMethod]
        public void CheckGetByPhone()
        {
            string phone = "690909333";
            Assert.IsNotNull(userRepository.GetByPhoneNumber(phone));

            phone = "210558890";
            Assert.IsNull(userRepository.GetByPhoneNumber(phone));
        }

    }
}
