using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using LinqToDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Accounts.Number;
using WcfBankingService.Database.DataProvider;
using WcfBankingService.Database.Model;
using WcfBankingService.Database.SavingData.Helper;

namespace BankingSoapServiceTest.Database
{
    [TestClass]
    public class DataSaverTest
    {
        private const int ValidUserId = 2;
        private const int NotValidUserId = 200;
        private const string NewAccessToken = "someAccessToken";

        private readonly DataSaver _dataSaver;

        public DataSaverTest()
        {
            _dataSaver = new DataSaver();
        }

        [TestMethod]
        public void SaveAccessToken_validUserId_savesData()
        {
            
            Assert.IsFalse(ContainsToken());
            _dataSaver.SaveToken(ValidUserId, NewAccessToken);
            Assert.IsTrue(ContainsToken());
        }

        [TestMethod]
        public void SaveAccessToken_notValidUserId_savesData()
        {
            Assert.IsFalse(ContainsToken());
            _dataSaver.SaveToken(NotValidUserId, NewAccessToken);
            Assert.IsTrue(ContainsToken());
        }

        [TestCleanup]
        public void Cleanup()
        {
            using (var db = new DbBank())
            {
                db.AccessTokens.Where(p => p.Token == NewAccessToken).Delete();

            }
        }

    
        private static bool ContainsToken()
        {
            return DbDataProvider.GetAccessTokenForUser(ValidUserId).Contains(NewAccessToken);
        }
    }
}
