using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Accounts.Number.ControlSum;

namespace BankTest.Account
{
    [TestClass]
    public class AccountNumberTest
    {
        private const string ValidNumber = "1234567891234567";
        private const string BankId = "11216900";

        private IControlSumCalculator _controlSumCalculator;
        
        public AccountNumberTest()
        {
            _controlSumCalculator = new MockControlSumCalculator();
        }

        [TestMethod]
        public void CreateAccountNumber_validInputData_AccountNumberCreated()
        {
            Assert.Fail("not implemented");
        }
    }
}
