using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Users.Access;

namespace BankTest.User
{
    [TestClass]
    public class StandardPasswordHasherTest
    {
        private readonly StandardPasswordHasher _hasher;
        private const string Password = "Pass";
        
        public StandardPasswordHasherTest()
        {
            _hasher = new StandardPasswordHasher();
        }

        [TestMethod]
        public void GetHashForPassword_ValidData_HashAsExpected()
        {
            var expectedHashedPassword = _hasher.HashPassword(Password);
            var bytes = Convert.FromBase64String(expectedHashedPassword);
            var salt = bytes.Skip(StandardPasswordHasher.PasswordHashLength).ToArray();
            var passwordHash = _hasher.HashPassword(Password, salt);
            Assert.AreEqual(expectedHashedPassword, passwordHash);
        }
    }
}
