using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Database.SavingData;
using WcfBankingService.Service.DataContract.Request;
using WcfBankingService.Service.DataContract.Response;
using WcfBankingService.Service.Rest;

namespace BankingSoapServiceTest
{
    [TestClass]
    public class RestTransferTest
    {
        private AccountsTestData _data;
        private readonly IBankingRestService _service;

        public RestTransferTest()
        {
            _service = new BankingRestService(new MockDataInserter());
            _data = new AccountsTestData();;
        }


        [TestMethod]
        public void Transfer_BothAccountsFromThisBank_ReturnsAccessDenied()
        {
            var transferData = new TransferData()
            {
                AccountNumber = _data.ValidReceiverAccountNumber,
                Amount = 2000,
                SenderAccountNumber = _data.ValidSenderAccountNumber,
                Title = "lorem ipsum"
            };
            var response = _service.Transfer(transferData);
            Assert.AreEqual(ResponseStatus.AccessDenied.ToString(), response.Error);
        }

        [TestMethod]
        public void Transfer_SenderFromOtherBankBank_ReceiverFromThisBank_ReturnsErrorNull()
        {
            var transferData = new TransferData()
            {
                AccountNumber = _data.ValidReceiverAccountNumber,
                Amount = 2000,
                SenderAccountNumber = _data.ListedBankAccountNumber,
                Title = "lorem ipsum"
            };
            var response = _service.Transfer(transferData);
            Assert.AreEqual(null, response.Error);
        }

        [TestMethod]
        public void Transfer_BothAccountsFromThisBank_nonExistingReceiver_ReturnsAccountNumberDoesntExist()
        {
            var transferData = new TransferData()
            {
                AccountNumber = _data.NotExistingAccountNumber,
                Amount = 2000,
                SenderAccountNumber = _data.ValidSenderAccountNumber,
                Title = "lorem ipsum"
            };
            var response = _service.Transfer(transferData);
            Assert.AreEqual(ResponseStatus.AccountNumberDoesntExist.ToString(), response.Error);
        }

        [TestMethod]
        public void Transfer_ReceiverFromThisBank_nonExistingSender_ReturnsErrorNull()
        {
            var transferData = new TransferData()
            {
                AccountNumber = _data.ValidReceiverAccountNumber,
                Amount = 2000,
                SenderAccountNumber = _data.NotExistingAccountNumber,
                Title = "lorem ipsum"
            };
            var response = _service.Transfer(transferData);
            Assert.AreEqual(null, response.Error);
        }

        [TestMethod]
        public void Transfer_InvalidReceiverNumber_ReturnsWrongAccountNumber()
        {
            var transferData = new TransferData()
            {
                AccountNumber = _data.InvalidAccountNumber,
                Amount = 2000,
                SenderAccountNumber = _data.ValidSenderAccountNumber,
                Title = "lorem ipsum"
            };
            var response = _service.Transfer(transferData);
            Assert.AreEqual(ResponseStatus.WrongAccountNumber.ToString(), response.Error);
        }

        [TestMethod]
        public void Transfer_InvalidSenderNumber_ReturnsAccessDenied()
        {
            var transferData = new TransferData()
            {
                AccountNumber = _data.ValidReceiverAccountNumber,
                Amount = 2000,
                SenderAccountNumber = _data.InvalidAccountNumber,
                Title = "lorem ipsum"
            };
            var response = _service.Transfer(transferData);
            Assert.AreEqual(ResponseStatus.WrongAccountNumber.ToString(), response.Error);
        }

        [TestMethod]
        public void Transfer_NotListedBanksReceiverNumber_ReturnsBankNotExists()
        {
            var transferData = new TransferData()
            {
                AccountNumber = _data.NotListedBankAccountNumber,
                Amount = 2000,
                SenderAccountNumber = _data.ValidSenderAccountNumber,
                Title = "lorem ipsum"
            };
            var response = _service.Transfer(transferData);
            Assert.AreEqual(ResponseStatus.OtherBankAccount.ToString(), response.Error);
        }
    }
}