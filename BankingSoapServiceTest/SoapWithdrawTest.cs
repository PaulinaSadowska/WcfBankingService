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
        private readonly AccountsTestData _data;
        private readonly IBankingService _service;

        public SoapWithdrawTest()
        {
            _service = new BankingService(new MockDataInserter());
            _data = new AccountsTestData();
        }

        [TestMethod]
        public void Withdraw_ValidData_ReturnsSuccess()
        {
            var paymentData = new WithdrawData()
            {
                AccountNumber = _data.ValidSenderAccountNumber,
                AccessToken = _data.ValidAccessToken,
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
                AccountNumber = _data.ValidSenderAccountNumber,
                AccessToken = _data.ValidAccessToken,
                Amount = "20000000000000000",
                OperationTitle = "WOW withdraw"
            };
            _service.Withdraw(paymentData);
        }
    }
}
