using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using WcfBankingService.Database.SavingData;
using WcfBankingService.Service.Soap;
using WcfBankingService.SoapService;
using WcfBankingService.SoapService.DataContract.Response;

namespace BankingSoapServiceTest
{
    [TestClass]
    public class BankingServiceSignIn
    {
        private const string CorrectLogin = "Login";
        private const string CorrectPassword = "Pass";

        private readonly IBankingService _service;

        public BankingServiceSignIn()
        {
            
            _service = new BankingService(new MockDataInserter());
        }
    
        [TestMethod]
        public void signIn_CorrectLoginAndPassword_ReturnsSuccess()
        {
            var response = _service.SignIn(CorrectLogin, CorrectPassword);
            Assert.AreEqual(response.ResponseStatus, ResponseStatus.Success);
            Assert.IsNotNull(response.AccessToken);
        }

        [TestMethod]
        public void signIn_WrongLogin_ReturnsError()
        {
            var response = _service.SignIn("bad login", CorrectPassword);
            Assert.AreEqual(response.ResponseStatus, ResponseStatus.IncorrectLoginOrPassword);
            Assert.IsNull(response.AccessToken);
        }

        [TestMethod]
        public void signIn_WrongPassword_ReturnsError()
        {
            var response = _service.SignIn(CorrectLogin, "bad password");
            Assert.AreEqual(response.ResponseStatus, ResponseStatus.IncorrectLoginOrPassword);
            Assert.IsNull(response.AccessToken);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void signIn_LoginNull_ThrowsFaultException()
        {
            _service.SignIn(null, CorrectPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void signIn_PasswordNull_ThrowsFaultException()
        {
            _service.SignIn(CorrectLogin, null);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void signIn_LoginToShort_ThrowsFaultException()
        {
            _service.SignIn("", CorrectPassword);
            //todo - how short login can be?
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void signIn_PasswordToShort_ThrowsFaultException()
        {
            _service.SignIn(CorrectLogin, "");
            //todo - how short password can be?
        }

    }
}
