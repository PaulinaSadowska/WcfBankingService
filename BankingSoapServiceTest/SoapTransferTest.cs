using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Database.SavingData;
using WcfBankingService.Service.DataContract.Request;
using WcfBankingService.Service.DataContract.Response;
using WcfBankingService.Service.Soap;

namespace BankingSoapServiceTest

{
    [TestClass]
    public class SoapTransferTest
    {
        private const string ValidSenderAccountNumber = "39112169001234567890987654";
        private const string ValidReceiverAccountNumber = "86112169001234567898765432";

        private const string NotExistingAccountNumber = "04112169001234567891234567";
        private const string InvalidAccountNumber = "12112169001234567891234567";

        private const string ListedBankAccountNumber = "66112241001122334455667788";
        private const string NotListedBankAccountNumber = "38333241001122334455667788";

        private const string ValidAccessToken = "QJAMYUPWOBXS";
        private readonly IBankingService _service;

        public SoapTransferTest()
        {
            _service = new BankingService(new MockDataInserter());
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
            var response = _service.Transfer(transferData, ValidAccessToken);
            Assert.AreEqual(ResponseStatus.Success, response.ResponseStatus);
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
            var response = _service.Transfer(transferData, ValidAccessToken);
            Assert.AreEqual(ResponseStatus.AccountNumberDoesntExist, response.ResponseStatus);
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
            var response = _service.Transfer(transferData, ValidAccessToken);
            Assert.AreEqual(ResponseStatus.AccountNumberDoesntExist, response.ResponseStatus);
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
            var response = _service.Transfer(transferData, ValidAccessToken);
            Assert.AreEqual(ResponseStatus.WrongAccountNumber, response.ResponseStatus);
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
            var response = _service.Transfer(transferData, ValidAccessToken);
            Assert.AreEqual(ResponseStatus.WrongAccountNumber, response.ResponseStatus);
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
            var response = _service.Transfer(transferData, ValidAccessToken);
            Assert.AreEqual(ResponseStatus.InsufficientFunds, response.ResponseStatus);
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
            var response = _service.Transfer(transferData, ValidAccessToken);
            Assert.AreEqual(ResponseStatus.BankNotExists, response.ResponseStatus);
        }
    }
}