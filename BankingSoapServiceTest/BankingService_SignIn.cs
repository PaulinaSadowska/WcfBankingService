using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using WcfBankingService;
using WcfBankingService.SoapService.DataContract;

namespace BankingSoapServiceTest
{
    [TestClass]
    public class BankingService_SignIn
    {
        private const string CORRECT_LOGIN = "Admin";
        private const string CORRECT_PASSWORD = "Pass";

        private IBankingService service;

        public BankingService_SignIn()
        {
            service = new BankingService();
        }
    
        [TestMethod]
        public void signIn_CorrectLoginAndPassword_ReturnsSuccess()
        {
            OperationResponse response = service.signIn(CORRECT_LOGIN, CORRECT_PASSWORD);
            Assert.AreEqual(response, OperationResponse.Success);
            Assert.AreEqual(response.Message(), "");
        }

        [TestMethod]
        public void signIn_WrongLogin_ReturnsError()
        {
            OperationResponse response = service.signIn("bad login", CORRECT_PASSWORD);
            Assert.AreEqual(response, OperationResponse.IncorrectLogin);
        }

        [TestMethod]
        public void signIn_WrongPassword_ReturnsError()
        {
            OperationResponse response = service.signIn(CORRECT_LOGIN, "badd password");
            Assert.AreEqual(response, OperationResponse.IncorrectPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void signIn_LoginNull_ThrowsFaultException()
        {
            service.signIn(null, CORRECT_PASSWORD);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void signIn_PasswordNull_ThrowsFaultException()
        {
            service.signIn(CORRECT_LOGIN, null);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void signIn_LoginToShort_ThrowsFaultException()
        {
            service.signIn("", CORRECT_PASSWORD);
            //todo - how shotr login can be?
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void signIn_PasswordToShort_ThrowsFaultException()
        {
            service.signIn(CORRECT_LOGIN, "");
            //todo - how short password can be?
        }

    }
}
