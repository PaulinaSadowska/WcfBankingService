using System.ServiceModel;
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
        [ExpectedException(typeof(FaultException), "Account number does not exist")]
        public void Transfer_BothAccountsFromThisBank_nonExistingReceiver_ThrowsFaultException_AccountNumberDoesntExist()
        {
            var transferData = new TransferData()
            {
                AccountNumber = NotExistingAccountNumber,
                Amount = 2000,
                SenderAccountNumber = ValidSenderAccountNumber,
                Title = "lorem ipsum"
            };
            _service.Transfer(transferData, ValidAccessToken);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Account number does not exist")]
        public void Transfer_BothAccountsFromThisBank_nonExistingSender_ThrowsFaultException_AccountNumberDoesntExist()
        {
            var transferData = new TransferData()
            {
                AccountNumber = ValidReceiverAccountNumber,
                Amount = 2000,
                SenderAccountNumber = NotExistingAccountNumber,
                Title = "lorem ipsum"
            };
            _service.Transfer(transferData, ValidAccessToken);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Wrong account number format")]
        public void Transfer_InvalidReceiverNumber_ThrowsFaultException_WrongAccountNumber()
        {
            var transferData = new TransferData()
            {
                AccountNumber = InvalidAccountNumber,
                Amount = 2000,
                SenderAccountNumber = ValidSenderAccountNumber,
                Title = "lorem ipsum"
            };
            _service.Transfer(transferData, ValidAccessToken);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Wrong account number format")]
        public void Transfer_InvalidSenderNumber_ThrowsFaultException_WrongAccountNumber()
        {
            var transferData = new TransferData()
            {
                AccountNumber = ValidReceiverAccountNumber,
                Amount = 2000,
                SenderAccountNumber = InvalidAccountNumber,
                Title = "lorem ipsum"
            };
            _service.Transfer(transferData, ValidAccessToken);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Insufficient funds on the account to perform the operation")]
        public void Transfer_ValidAccounts_SenderHasNotEnoughMoney_ThrowsFaultException_InsufficientFunds()
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
        [ExpectedException(typeof(FaultException), "The bank you want to transfer the money to does not exist")]
        public void Transfer_NotListedBanksReceiverNumber_ThrowsFaultException_BankNotExists()
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