using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Users;
using WcfBankingService.Users.Access;

namespace BankTest.User
{
    [TestClass]
    public class AccessTokenGeneratorTest
    {

        [TestMethod]
        public void GenerateRandomString_validLength_returnsRandomString()
        {
            const int size = 8;
            var randomString = AccessTokenGenerator.Generate(size);
            Assert.IsNotNull(randomString);
            Assert.AreEqual(size, randomString.Length);
        }

        [TestMethod]
        public void GenerateRandomStrings_validLength_returnsDifferentStrings()
        {
            const int size = 8;
            var accessTokens = new List<string>();
            for (var i = 0; i < 100; i++)
            {
                var newToken = AccessTokenGenerator.Generate(size);
                if (accessTokens.Contains(newToken))
                {
                    Assert.Fail($"token {newToken} already exists on the list! ({i} iteration)");
                }
                accessTokens.Add(newToken);
            }
        }
    }
}
