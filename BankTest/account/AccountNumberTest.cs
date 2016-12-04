using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Accounts.Number;

namespace BankTest.Account
{
    [TestClass]
    public class AccountNumberTest
    {
        private const string VALID_NUMBER = "1234567891234567";
        private const string BANK_ID = "11216900";

        private IControlSumCalculator ControlSumCalculator;
        
        public AccountNumberTest()
        {
            ControlSumCalculator = new MockControlSumCalculator();
        }

        [TestMethod]
        public void CreateAccountNumber_validInputData_AccountNumberCreated()
        {
            Assert.Fail("not implemented");
        }
    }
}
