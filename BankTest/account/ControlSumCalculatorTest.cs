using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.account;
using WcfBankingService.account.number;

namespace BankTest
{
    [TestClass]
    public class ControlSumCalculatorTest
    {
        private IControlSumCalculator controlSumCalculator;
  
        public ControlSumCalculatorTest()
        {
            controlSumCalculator = new StandardControlSumCalculator();
        }


        [TestMethod]
        public void ControlSumCalculator_validInputData_ReturnsControlSum()
        {
            String controlSum = controlSumCalculator.Calculate("11216900", "1234567891234567");
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ControlSumCalculator_bankIdNull_ThrowsException()
        {
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ControlSumCalculator_NumberNull_ThrowsException()
        {
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ControlSumCalculator_bankIdToShort_ThrowsException()
        {
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ControlSumCalculator_bankIdToLong_ThrowsException()
        {
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ControlSumCalculator_NumberToShort_ThrowsException()
        {
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ControlSumCalculator_NumberToLong_ThrowsException()
        {
            Assert.Fail();
        }
    }
}
