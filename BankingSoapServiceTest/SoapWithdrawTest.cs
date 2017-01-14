using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Database.SavingData;
using WcfBankingService.Service.DataContract.Request;
using WcfBankingService.Service.DataContract.Response;
using WcfBankingService.Service.Soap;

namespace BankingSoapServiceTest
{
    [TestClass]
    public class SoapWithdrawTest
    {
        private const string ValidAccountNumber = "39112169001234567890987654";
        private readonly string _accessToken;
        private readonly IBankingService _service;

        public SoapWithdrawTest()
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
                Amount = "2",
                OperationTitle = "WOW withdraw"
            };
            var response = _service.Withdraw(paymentData);
            Assert.AreEqual(ResponseStatus.Success, response.ResponseStatus);
        }


        [TestMethod]
        [ExpectedException(typeof(FaultException), "Insufficient funds on the account to perform the operation")]
        public void Withdraw_AmountGreaterThanBalance_ThrowsFaultException_InsufficientFunds()
        {
            var paymentData = new WithdrawData()
            {
                AccountNumber = ValidAccountNumber,
                AccessToken = _accessToken,
                Amount = "20000000000000000",
                OperationTitle = "WOW withdraw"
            };
            _service.Withdraw(paymentData);
        }
    }
}
