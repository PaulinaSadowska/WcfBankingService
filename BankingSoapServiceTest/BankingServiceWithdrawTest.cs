using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Database.SavingData;
using WcfBankingService.Service.DataContract.Request;
using WcfBankingService.Service.DataContract.Response;
using WcfBankingService.Service.Soap;
using WcfBankingService.SoapService.DataContract.Response;

namespace BankingSoapServiceTest
{
    [TestClass]
    public class BankingServiceWithdrawTest
    {
        private const string ValidAccountNumber = "39112169001234567890987654";
        private readonly string _accessToken;
        private readonly IBankingService _service;

        public BankingServiceWithdrawTest()
        {
            _service = new BankingService(new MockDataInserter());
            _accessToken = "876123456433";
        }

        [TestMethod]
        public void Withdraw_ValidData_ReturnsSuccess()
        {
            var paymentData = new WithdrawData()
            {
                AccountNumber = ValidAccountNumber,
                AccessToken = _accessToken,
                Amount = 2,
                OperationTitle = "WOW withdraw"
            };
            var response = _service.Withdraw(paymentData);
            Assert.AreEqual(ResponseStatus.Success, response.ResponseStatus);
        }


        [TestMethod]
        public void Withdraw_AmountGreaterThanBalance_ReturnsInsufficientFunds()
        {
            var paymentData = new WithdrawData()
            {
                AccountNumber = ValidAccountNumber,
                AccessToken = _accessToken,
                Amount = 20000000000000000,
                OperationTitle = "WOW withdraw"
            };
            var response = _service.Withdraw(paymentData);
            Assert.AreEqual(ResponseStatus.InsufficientFunds, response.ResponseStatus);
        }
    }
}
