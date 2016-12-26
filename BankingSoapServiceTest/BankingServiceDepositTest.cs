using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.SoapService;
using WcfBankingService.SoapService.DataContract.Response;
using WcfBankingService.SOAPService.DataContract;

namespace BankingSoapServiceTest
{
    [TestClass]
    public class BankingServiceDepositTest
    {
        private const string ValidAccountNumber = "";
        private readonly string _accessToken;
        private readonly IBankingService _service;

        public BankingServiceDepositTest()
        {
            _service = new BankingService();
            _accessToken = "";//TODO - assign correct value from predefined accounts
        }


        [TestMethod]
        public void DepositMoney_ValidData_ReturnsSuccess()
        {
            var paymentData = new PaymentData()
            {
                AccountNumber = ValidAccountNumber,
                AccessToken = _accessToken,
                Amount = 200,
                OperationTitle = "WOW deposit"
            };
            var response = _service.Deposit(paymentData);
            Assert.AreEqual(ResponseStatus.Success, response.ResponseStatus);
        }

        [TestMethod]
        public void DepositMoney_WrongAccessToken_ReturnsAccessDenied()
        {
            var paymentData = new PaymentData()
            {
                AccountNumber = ValidAccountNumber,
                AccessToken = "wrong access Token",
                Amount = 200,
                OperationTitle = "WOW deposit"
            };
            var response = _service.Deposit(paymentData);
            Assert.AreEqual(ResponseStatus.AccessDenied, response.ResponseStatus);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Deposit_paymentDataNull_throwsException()
        {
            _service.Deposit(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Deposit_amountLessThanZero_throwsException()
        {
            var paymentData = new PaymentData()
            {
                AccountNumber = ValidAccountNumber,
                AccessToken = _accessToken,
                Amount = -200,
                OperationTitle = "WOW deposit"
            };
            _service.Deposit(paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Deposit_AccountNumberNull_throwsException()
        {
            var paymentData = new PaymentData()
            {
                AccessToken = ValidAccountNumber,
                Amount = -200,
                OperationTitle = "WOW deposit"
            };
            _service.Deposit(paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Deposit_accessTokenNull_throwsException()
        {
            var paymentData = new PaymentData()
            {
                AccountNumber = ValidAccountNumber,
                Amount = -200,
                OperationTitle = "WOW deposit"
            };
            _service.Deposit(paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Deposit_operationTitleNull_throwsException()
        {
            var paymentData = new PaymentData()
            {
                AccountNumber = ValidAccountNumber,
                AccessToken = _accessToken,
                Amount = -200,
            };
            _service.Deposit(paymentData);
        }
    }
}
