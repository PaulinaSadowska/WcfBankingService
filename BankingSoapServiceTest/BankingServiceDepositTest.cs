﻿using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.SoapService;
using WcfBankingService.SoapService.DataContract.Response;
using WcfBankingService.SOAPService.DataContract;

namespace BankingSoapServiceTest
{
    [TestClass]
    public class BankingServiceDepositTest
    {
        private const string ValidAccountNumber = "";
        private const string NotExistingAccountNumber = "04112169001234567891234567";
        private const string InvalidAccountNumber = "12112169001234567891234567";
        private const string OtherBankAccountNumber = "04112168661234567891234567";
        private readonly string _accessToken;
        private readonly IBankingService _service;

        public BankingServiceDepositTest()
        {
            _service = new BankingService();
            _accessToken = "";//TODO - assign correct value from predefined accounts
        }


        [TestMethod]
        public void DepositMoney_ValidData_ReturnsSuccess()
        {
            var paymentData = new PaymentData()
            {
                AccountNumber = ValidAccountNumber,
                AccessToken = _accessToken,
                Amount = 200,
                OperationTitle = "WOW deposit"
            };
            var response = _service.Deposit(paymentData);
            Assert.AreEqual(ResponseStatus.Success, response.ResponseStatus);
        }

        [TestMethod]
        public void DepositMoney_WrongAccessToken_ReturnsAccessDenied()
        {
            var paymentData = new PaymentData()
            {
                AccountNumber = ValidAccountNumber,
                AccessToken = "wrong access Token",
                Amount = 200,
                OperationTitle = "WOW deposit"
            };
            var response = _service.Deposit(paymentData);
            Assert.AreEqual(ResponseStatus.AccessDenied, response.ResponseStatus);
        }

        [TestMethod]
        public void DepositMoney_InvalidAccountNumber_ReturnsWrongAccountNumberFormat()
        {
            var paymentData = new PaymentData()
            {
                AccountNumber = InvalidAccountNumber,
                AccessToken = _accessToken,
                Amount = 200,
                OperationTitle = "WOW deposit"
            };
            var response = _service.Deposit(paymentData);
            Assert.AreEqual(ResponseStatus.WrongAccountNumberFormat, response.ResponseStatus);
        }

        [TestMethod]
        public void DepositMoney_AccountNumberDoesNotExists_ReturnsAccountNumberDoesntExist()
        {
            var paymentData = new PaymentData()
            {
                AccountNumber = NotExistingAccountNumber,
                AccessToken = _accessToken,
                Amount = 200,
                OperationTitle = "WOW deposit"
            };
            var response = _service.Deposit(paymentData);
            Assert.AreEqual(ResponseStatus.AccountNumberDoesntExist, response.ResponseStatus);
        }

        [TestMethod]
        public void DepositMoney_AccountNumberFromOtherBank_ReturnsAccountNumberFromOtherBank()
        {
            var paymentData = new PaymentData()
            {
                AccountNumber = OtherBankAccountNumber,
                AccessToken = _accessToken,
                Amount = 200,
                OperationTitle = "WOW deposit"
            };
            var response = _service.Deposit(paymentData);
            Assert.AreEqual(ResponseStatus.AccountNumberFromOtherBank, response.ResponseStatus);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Deposit_paymentDataNull_throwsException()
        {
            _service.Deposit(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Deposit_amountLessThanZero_throwsException()
        {
            var paymentData = new PaymentData()
            {
                AccountNumber = ValidAccountNumber,
                AccessToken = _accessToken,
                Amount = -200,
                OperationTitle = "WOW deposit"
            };
            _service.Deposit(paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Deposit_AccountNumberNull_throwsException()
        {
            var paymentData = new PaymentData()
            {
                AccessToken = ValidAccountNumber,
                Amount = -200,
                OperationTitle = "WOW deposit"
            };
            _service.Deposit(paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Deposit_accessTokenNull_throwsException()
        {
            var paymentData = new PaymentData()
            {
                AccountNumber = ValidAccountNumber,
                Amount = -200,
                OperationTitle = "WOW deposit"
            };
            _service.Deposit(paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Deposit_operationTitleNull_throwsException()
        {
            var paymentData = new PaymentData()
            {
                AccountNumber = ValidAccountNumber,
                AccessToken = _accessToken,
                Amount = -200,
            };
            _service.Deposit(paymentData);
        }
    }
}
