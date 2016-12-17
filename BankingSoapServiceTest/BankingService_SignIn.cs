using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using WcfBankingService;
using WcfBankingService.SoapService;
using WcfBankingService.SoapService.DataContract;

namespace BankingSoapServiceTest
{
    [TestClass]
    public class BankingServiceSignIn
    {
        private const string CorrectLogin = "Admin";
        private const string CorrectPassword = "Pass";

        private readonly IBankingService _service;

        public BankingServiceSignIn()
        {
            _service = new BankingService();
        }
    
        [TestMethod]
        public void signIn_CorrectLoginAndPassword_ReturnsSuccess()
        {
            var response = _service.signIn(CorrectLogin, CorrectPassword);
            Assert.AreEqual(response, OperationResponse.Success);
            Assert.AreEqual(response.Message(), "");
        }

        [TestMethod]
        public void signIn_WrongLogin_ReturnsError()
        {
            var response = _service.signIn("bad login", CorrectPassword);
            Assert.AreEqual(response, OperationResponse.IncorrectLogin);
        }

        [TestMethod]
        public void signIn_WrongPassword_ReturnsError()
        {
            var response = _service.signIn(CorrectLogin, "badd password");
            Assert.AreEqual(response, OperationResponse.IncorrectPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void signIn_LoginNull_ThrowsFaultException()
        {
            _service.signIn(null, CorrectPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void signIn_PasswordNull_ThrowsFaultException()
        {
            _service.signIn(CorrectLogin, null);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void signIn_LoginToShort_ThrowsFaultException()
        {
            _service.signIn("", CorrectPassword);
            //todo - how shotr login can be?
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void signIn_PasswordToShort_ThrowsFaultException()
        {
            _service.signIn(CorrectLogin, "");
            //todo - how short password can be?
        }

    }
}
