using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using WcfBankingService.Database.SavingData;
using WcfBankingService.Service.DataContract.Response;
using WcfBankingService.Service.Soap;

namespace BankingSoapServiceTest
{
    [TestClass]
    public class SoapSignInTest
    {
        private const string CorrectLogin = "Login";
        private const string CorrectPassword = "Pass";

        private readonly IBankingService _service;

        public SoapSignInTest()
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
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void signIn_PasswordToShort_ThrowsFaultException()
        {
            _service.SignIn(CorrectLogin, "");
        }

    }
}
