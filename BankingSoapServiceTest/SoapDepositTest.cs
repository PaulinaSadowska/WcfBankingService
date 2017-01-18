using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Database.SavingData;
using WcfBankingService.Service.DataContract.Request;
using WcfBankingService.Service.DataContract.Response;
using WcfBankingService.Service.Soap;

namespace BankingSoapServiceTest
{
    [TestClass]
    public class SoapDepositTest
    {
        private readonly AccountsTestData _data;
        private readonly IBankingService _service;

        public SoapDepositTest()
        {
            _service = new BankingService(new MockDataInserter());
            _data = new AccountsTestData();
        }


        [TestMethod]
        public void Deposit_ValidData_ReturnsSuccess()
        {
            var paymentData = new DepositData()
            {
                AccountNumber = _data.ValidSenderAccountNumber,
                Amount = "200",
                OperationTitle = "WOW deposit"
            };
            var response = _service.Deposit(paymentData);
            Assert.AreEqual(ResponseStatus.Success, response.ResponseStatus);
        }


        [TestMethod]
        [ExpectedException(typeof(FaultException), "Wrong account number format")]
        public void Deposit_InvalidAccountNumber_ThrowsFaultException_WrongAccountNumberFormat()
        {
            var paymentData = new DepositData()
            {
                AccountNumber = _data.InvalidAccountNumber,
                Amount = "200",
                OperationTitle = "WOW deposit"
            };
            _service.Deposit(paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Account number does not exist")]
        public void Deposit_AccountNumberDoesNotExists_ThrowsFaultException_AccountNumberDoesntExist()
        {
            var paymentData = new DepositData()
            {
                AccountNumber = _data.NotExistingAccountNumber,
                Amount = "200",
                OperationTitle = "WOW deposit"
            };
            _service.Deposit(paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Deposit_WrongAmountFormat_ThrowsFaultException()
        {
            var paymentData = new DepositData()
            {
                AccountNumber = _data.ValidSenderAccountNumber,
                Amount = "20x0",
                OperationTitle = "WOW deposit"
            };
            _service.Deposit(paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Wrong account number format")]
        public void Deposit_AccountNumberFromOtherBank_ThrowsFaultException_WrongAccountNumberFormat()
        {
            var paymentData = new DepositData()
            {
                AccountNumber = _data.ListedBankAccountNumber,
                Amount = "200",
                OperationTitle = "WOW deposit"
            };
            _service.Deposit(paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Deposit_PaymentDataNull_ThrowsException()
        {
            _service.Deposit(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Deposit_amountLessThanZero_throwsException()
        {
            var paymentData = new DepositData()
            {
                AccountNumber = _data.ValidSenderAccountNumber,
                Amount = "-200",
                OperationTitle = "WOW deposit"
            };
            _service.Deposit(paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Deposit_AccountNumberNull_throwsException()
        {
            var paymentData = new DepositData()
            {
                Amount = "-200",
                OperationTitle = "WOW deposit"
            };
            _service.Deposit(paymentData);
        }


        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Deposit_OperationTitleNull_ThrowsException()
        {
            var paymentData = new DepositData()
            {
                AccountNumber = _data.ValidSenderAccountNumber,
                Amount = "-200",
            };
            _service.Deposit(paymentData);
        }
    }
}
