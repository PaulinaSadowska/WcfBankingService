using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using LinqToDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Balance;
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

        private const string ValidInnerAccountNumber = "1112223334445556";
        private const decimal ExpectedBalanceValue = 666.67m;
        private decimal PreviousBalanceValue = 111.11m;

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

        [TestMethod]
        public void SaveAccountBalance_validAccount_savesData()
        {
            Assert.AreNotEqual(ExpectedBalanceValue, GetBalanceValue());
            var account = new Account(
                new AccountNumber("", ValidInnerAccountNumber, ""), 
                new Balance(ExpectedBalanceValue)
                );
            _dataSaver.SaveAccountBalance(account);
            Assert.AreEqual(ExpectedBalanceValue, GetBalanceValue());
        }
    

        [TestCleanup]
        public void Cleanup()
        {
            using (var db = new DbBank())
            {
                db.AccessTokens.Where(p => p.Token == NewAccessToken).Delete();
                db.Accounts.Where(p => p.InnerAccountNumber == ValidInnerAccountNumber)
                    .Set(p => p.BalanceValue, PreviousBalanceValue)
                    .Update();
            }
        }

        private static decimal GetBalanceValue()
        {
            using (var db = new DbBank())
            {
                var query = from p in db.Accounts
                where p.InnerAccountNumber == ValidInnerAccountNumber
                select p;

                var account = query.ToList().FirstOrDefault();
                return account?.BalanceValue ?? 0.0m;

            }
        }

        private static bool ContainsToken()
        {
            return DbDataProvider.GetAccessTokenForUser(ValidUserId).Contains(NewAccessToken);
        }
    }
}
