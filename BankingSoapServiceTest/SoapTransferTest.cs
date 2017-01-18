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
        private readonly AccountsTestData _data;
        private readonly IBankingService _service;

        public SoapTransferTest()
        {
            _service = new BankingService(new MockDataInserter());
            _data = new AccountsTestData();;
        }


        [TestMethod]
        public void Transfer_BothAccountsFromThisBank_ReturnsSuccess()
        {
            var transferData = new SoapTransferData()
            {
                ReceiverAccountNumber = _data.ValidReceiverAccountNumber,
                Amount = "20",
                SenderAccountNumber = _data.ValidSenderAccountNumber,
                Title = "lorem ipsum",
                AccessToken = _data.ValidAccessToken
            };
            var response = _service.Transfer(transferData);
            Assert.AreEqual(ResponseStatus.Success, response.ResponseStatus);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Account number does not exist")]
        public void Transfer_BothAccountsFromThisBank_nonExistingReceiver_ThrowsFaultException_AccountNumberDoesntExist()
        {
            var transferData = new SoapTransferData()
            {
                ReceiverAccountNumber = _data.NotExistingAccountNumber,
                Amount = "20",
                SenderAccountNumber = _data.ValidSenderAccountNumber,
                Title = "lorem ipsum",
                AccessToken = _data.ValidAccessToken
            };
            _service.Transfer(transferData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Account number does not exist")]
        public void Transfer_BothAccountsFromThisBank_nonExistingSender_ThrowsFaultException_AccountNumberDoesntExist()
        {
            var transferData = new SoapTransferData()
            {
                ReceiverAccountNumber = _data.ValidReceiverAccountNumber,
                Amount = "20",
                SenderAccountNumber = _data.NotExistingAccountNumber,
                Title = "lorem ipsum",
                AccessToken = _data.ValidAccessToken
            };
            _service.Transfer(transferData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Wrong account number format")]
        public void Transfer_InvalidReceiverNumber_ThrowsFaultException_WrongAccountNumber()
        {
            var transferData = new SoapTransferData()
            {
                ReceiverAccountNumber = _data.InvalidAccountNumber,
                Amount = "20",
                SenderAccountNumber = _data.ValidSenderAccountNumber,
                Title = "lorem ipsum",
                AccessToken = _data.ValidAccessToken
            };
            _service.Transfer(transferData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Wrong account number format")]
        public void Transfer_InvalidSenderNumber_ThrowsFaultException_WrongAccountNumber()
        {
            var transferData = new SoapTransferData()
            {
                ReceiverAccountNumber = _data.ValidReceiverAccountNumber,
                Amount = "20",
                SenderAccountNumber = _data.InvalidAccountNumber,
                Title = "lorem ipsum",
                AccessToken = _data.ValidAccessToken
            };
            _service.Transfer(transferData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Insufficient funds on the account to perform the operation")]
        public void Transfer_ValidAccounts_SenderHasNotEnoughMoney_ThrowsFaultException_InsufficientFunds()
        {
            var transferData = new SoapTransferData()
            {
                ReceiverAccountNumber = _data.ValidReceiverAccountNumber,
                Amount = "2000000000",
                SenderAccountNumber = _data.ValidSenderAccountNumber,
                Title = "lorem ipsum",
                AccessToken = _data.ValidAccessToken
            };
            _service.Transfer(transferData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "The bank you want to transfer the money to does not exist")]
        public void Transfer_NotListedBanksReceiverNumber_ThrowsFaultException_BankNotExists()
        {
            var transferData = new SoapTransferData()
            {
                ReceiverAccountNumber = _data.NotListedBankAccountNumber,
                Amount = "20",
                SenderAccountNumber = _data.ValidSenderAccountNumber,
                Title = "lorem ipsum",
                AccessToken = _data.ValidAccessToken
            };
            _service.Transfer(transferData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Transfer_WrongAmountFormat_ThrowsFaultException()
        {
            var transferData = new SoapTransferData()
            {
                ReceiverAccountNumber = _data.ValidReceiverAccountNumber,
                Amount = "20a2",
                SenderAccountNumber = _data.ValidSenderAccountNumber,
                Title = "lorem ipsum",
                AccessToken = _data.ValidAccessToken
            };
            _service.Transfer(transferData);
        }
    }
}