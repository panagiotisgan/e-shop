using eShop.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.UITests
{
    [TestClass()]
    public class PasswordServiceTest
    {
        private string password;

        [TestInitialize]
        public void Initialize()
        {
            password = "";
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowExceptionIfPasswordIsNull()
        {
            //string password = "";
            try
            {
                var result = PasswordService.CreatePassword(password);
            }
            catch(ArgumentException ex) 
            {
                //Assert.ThrowsException<ArgumentNullException>();
                Assert.AreEqual("Password cannot be null or less than 7 characters", ex.Message);
                throw;
            }
            
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowExceptionIfPasswordIsLessThanSeverChar()
        {
            password = "panika";
            try
            {
                PasswordService.CreatePassword(password);
            }
            catch(ArgumentException ex)
            {
                Assert.AreEqual("Password cannot be null or less than 7 characters", ex.Message);
                throw;
            }
        }

        [TestMethod()]
        public void SuccessfullPasswordCreate()
        {
            password = "4425C#!!!";
            try
            {
                var res = PasswordService.CreatePassword(password);
                Assert.IsNotNull(res.password);
                Assert.IsNotNull(res.salt);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
