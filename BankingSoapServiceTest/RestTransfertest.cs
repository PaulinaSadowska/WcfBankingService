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
        private const string ValidSenderAccountNumber = "39112169001234567890987654";
        private const string ValidReceiverAccountNumber = "86112169001234567898765432";

        private const string NotExistingAccountNumber = "04112169001234567891234567";
        private const string InvalidAccountNumber = "12112169001234567891234567";

        private const string ListedBankAccountNumber = "66112241001122334455667788";
        private const string NotListedBankAccountNumber = "38333241001122334455667788";

        private const string ValidAccessToken = "QJAMYUPWOBXS";
        private readonly IBankingRestService _service;

        public RestTransferTest()
        {
            _service = new BankingRestService(new MockDataInserter());
        }


        [TestMethod]
        public void Transfer_BothAccountsFromThisBank_ReturnsSuccess()
        {
            var transferData = new TransferData()
            {
                AccountNumber = ValidReceiverAccountNumber,
                Amount = 2000,
                SenderAccountNumber = ValidSenderAccountNumber,
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
                AccountNumber = NotExistingAccountNumber,
                Amount = 2000,
                SenderAccountNumber = ValidSenderAccountNumber,
                Title = "lorem ipsum"
            };
            var response = _service.Transfer(transferData);
            Assert.AreEqual(ResponseStatus.AccountNumberDoesntExist.ToString(), response.Error);
        }

        [TestMethod]
        public void Transfer_BothAccountsFromThisBank_nonExistingSender_ReturnsAccountNumberDoesntExist()
        {
            var transferData = new TransferData()
            {
                AccountNumber = ValidReceiverAccountNumber,
                Amount = 2000,
                SenderAccountNumber = NotExistingAccountNumber,
                Title = "lorem ipsum"
            };
            var response = _service.Transfer(transferData);
            Assert.AreEqual(ResponseStatus.AccountNumberDoesntExist.ToString(), response.Error);
        }

        [TestMethod]
        public void Transfer_InvalidReceiverNumber_ReturnsWrongAccountNumber()
        {
            var transferData = new TransferData()
            {
                AccountNumber = InvalidAccountNumber,
                Amount = 2000,
                SenderAccountNumber = ValidSenderAccountNumber,
                Title = "lorem ipsum"
            };
            var response = _service.Transfer(transferData);
            Assert.AreEqual(ResponseStatus.WrongAccountNumber.ToString(), response.Error);
        }

        [TestMethod]
        public void Transfer_InvalidSenderNumber_ReturnsWrongAccountNumber()
        {
            var transferData = new TransferData()
            {
                AccountNumber = ValidReceiverAccountNumber,
                Amount = 2000,
                SenderAccountNumber = InvalidAccountNumber,
                Title = "lorem ipsum"
            };
            var response = _service.Transfer(transferData);
            Assert.AreEqual(ResponseStatus.WrongAccountNumber.ToString(), response.Error);
        }

        [TestMethod]
        public void Transfer_ValidAccounts_SenderHasNotEnoughMoney_ReturnsInsufficientFunds()
        {
            var transferData = new TransferData()
            {
                AccountNumber = ValidReceiverAccountNumber,
                Amount = 2000000000,
                SenderAccountNumber = ValidSenderAccountNumber,
                Title = "lorem ipsum"
            };
            var response = _service.Transfer(transferData);
            Assert.AreEqual(ResponseStatus.InsufficientFunds.ToString(), response.Error);
        }

        [TestMethod]
        public void Transfer_NotListedBanksReceiverNumber_ReturnsBankNotExists()
        {
            var transferData = new TransferData()
            {
                AccountNumber = NotListedBankAccountNumber,
                Amount = 2000,
                SenderAccountNumber = ValidSenderAccountNumber,
                Title = "lorem ipsum"
            };
            var response = _service.Transfer(transferData);
            Assert.AreEqual(ResponseStatus.BankNotExists.ToString(), response.Error);
        }
    }
}