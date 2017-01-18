using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Accounts.Number;

namespace BankTest.Account
{
    [TestClass]
    public class AccountNumberFactoryTest
    {
        private const string InnerNumber = "1234567891234567";
        private const string BankId = "00112169";

        private readonly AccountNumberFactory _accountNumberFactory;
        
        public AccountNumberFactoryTest()
        {
            _accountNumberFactory = new AccountNumberFactory(BankId, new MockControlSumCalculator());
        }

        [TestMethod]
        public void CreateAccountNumber_validInputData_ReturnsAccountNumber()
        {
            var accountNumber = _accountNumberFactory.GetAccountNumberFromInner(InnerNumber);
            Assert.IsNotNull(accountNumber);
            Assert.AreEqual(accountNumber.BankId, BankId);
            Assert.AreEqual(accountNumber.InnerNumber, InnerNumber);
        }

        [TestMethod]
        public void CreateAccountNumber_InnerNumberNull_ReturnsNull()
        {
            var accountNumber = _accountNumberFactory.GetAccountNumberFromInner(null);
            Assert.IsNull(accountNumber);
        }

        [TestMethod]
        public void GetAccountNumber_validData_ReturnsAccountNumber()
        {
            var accountNumber = _accountNumberFactory.GetBankAccountNumber($"00{BankId}{InnerNumber}");
            Assert.IsNotNull(accountNumber);
            Assert.AreEqual(BankId, accountNumber.BankId);
            Assert.AreEqual(InnerNumber, accountNumber.InnerNumber);
        }

        [TestMethod]
        public void GetAccountNumber_wrongBankId_ReturnsNull()
        {
            const string wrongBankId = "11216976";
            var accountNumber = _accountNumberFactory.GetBankAccountNumber($"00{wrongBankId}{InnerNumber}");
            Assert.IsNull(accountNumber);
        }

        [TestMethod]
        public void GetAccountNumber_accountNumberNull_ReturnsNull()
        {
            var accountNumber = _accountNumberFactory.GetBankAccountNumber(null);
            Assert.IsNull(accountNumber);
        }

        [TestMethod]
        public void GetAccountNumber_innerNumberToShort_ReturnsNull()
        {
            const string wrongInnerNumber = "00123456743";
            var accountNumber = _accountNumberFactory.GetBankAccountNumber($"00{BankId}{wrongInnerNumber}");
            Assert.IsNull(accountNumber);
        }
    }
}
