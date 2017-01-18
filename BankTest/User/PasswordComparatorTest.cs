using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Users.Access;

namespace BankTest.User
{
    [TestClass]
    public class PasswordComparatorTest
    {
        private const string Password = "Pass";
        private const string HashedPassword = "p0IyTkFIQmWZ2fbihRZ1NS76GwXcWUwFvk1EESL+8AmPpo3v";

        private readonly PasswordComparator _comparator;

        public PasswordComparatorTest()
        {
            _comparator = new PasswordComparator();
        }

        [TestMethod]
        public void ComparePasswords_PasswwordAndItsHash_AreTheSame()
        {
           Assert.IsTrue(_comparator.ArePasswordsSame(HashedPassword, Password));
        }
    }
}
