using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Accounts.Number;

namespace BankTest.account
{
    [TestClass]
    public class ControlSumCalculatorTest
    {
        private const string AccountNumber = "0000261395100000";
        private const string BankId = "10101023";
        private const string ControlSum = "83";

        private readonly IControlSumCalculator _controlSumCalculator;

  
        public ControlSumCalculatorTest()
        {
            _controlSumCalculator = new StandardControlSumCalculator();
        }


        [TestMethod]
        public void ControlSumCalculator_validInputData_ReturnsControlSum()
        { 
            var controlSum = _controlSumCalculator.Calculate(BankId, AccountNumber);
            Assert.AreEqual(controlSum, ControlSum);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ControlSumCalculator_bankIdNull_ThrowsException()
        {
            _controlSumCalculator.Calculate(null, AccountNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ControlSumCalculator_NumberNull_ThrowsException()
        {
            _controlSumCalculator.Calculate(BankId, AccountNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ControlSumCalculator_bankIdToShort_ThrowsException()
        {
            _controlSumCalculator.Calculate("112169", AccountNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ControlSumCalculator_bankIdToLong_ThrowsException()
        {
            _controlSumCalculator.Calculate(BankId+"00", AccountNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ControlSumCalculator_NumberToShort_ThrowsException()
        {
            _controlSumCalculator.Calculate(BankId, "12345678");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ControlSumCalculator_NumberToLong_ThrowsException()
        {
            _controlSumCalculator.Calculate(BankId, AccountNumber+"00");
        }
    }
}
