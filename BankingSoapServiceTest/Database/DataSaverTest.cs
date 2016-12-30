using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Accounts.Number;
using WcfBankingService.Database.DataProvider;
using WcfBankingService.Database.SavingData.Helper;

namespace BankingSoapServiceTest.Database
{
    [TestClass]
    public class DataSaverTest
    {
        private const int ValidUserId = 2;
        private readonly DataSaver _dataSaver;

        public DataSaverTest()
        {
            _dataSaver = new DataSaver();
        }

        [TestMethod]
        public void getUserId_existingLogin_returnsId()
        {
            var newAccessToken = "someAccessToken";
            Assert.IsFalse(GetTokens().Contains(newAccessToken));
            _dataSaver.SaveToken(ValidUserId, newAccessToken);
            Assert.IsTrue(GetTokens().Contains(newAccessToken));
        }

        private List<string> GetTokens()
        {
            return DbDataProvider.GetAccessTokenForUser(ValidUserId);
        }
    }
}
