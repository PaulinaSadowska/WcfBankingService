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
        private const string ValidAccountNumber = "39112169001234567890987654";
        private const string NotExistingAccountNumber = "04112169001234567891234567";
        private const string InvalidAccountNumber = "12112169001234567891234567";
        private const string OtherBankAccountNumber = "04112168661234567891234567";
        private readonly IBankingService _service;

        public SoapDepositTest()
        {
            _service = new BankingService(new MockDataInserter());
        }


        [TestMethod]
        public void Deposit_ValidData_ReturnsSuccess()
        {
            var paymentData = new DepositData()
            {
                AccountNumber = ValidAccountNumber,
                Amount = 200,
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
                AccountNumber = InvalidAccountNumber,
                Amount = 200,
                OperationTitle = "WOW deposit"
            };
            var response = _service.Deposit(paymentData);
            Assert.AreEqual(ResponseStatus.WrongAccountNumber, response.ResponseStatus);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Account number does not exist")]
        public void Deposit_AccountNumberDoesNotExists_ThrowsFaultException_AccountNumberDoesntExist()
        {
            var paymentData = new DepositData()
            {
                AccountNumber = NotExistingAccountNumber,
                Amount = 200,
                OperationTitle = "WOW deposit"
            };
            var response = _service.Deposit(paymentData);
            Assert.AreEqual(ResponseStatus.AccountNumberDoesntExist, response.ResponseStatus);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Wrong account number format")]
        public void Deposit_AccountNumberFromOtherBank_ThrowsFaultException_WrongAccountNumberFormat()
        {
            var paymentData = new DepositData()
            {
                AccountNumber = OtherBankAccountNumber,
                Amount = 200,
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
                AccountNumber = ValidAccountNumber,
                Amount = -200,
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
                Amount = -200,
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
                AccountNumber = ValidAccountNumber,
                Amount = -200,
            };
            _service.Deposit(paymentData);
        }
    }
}
