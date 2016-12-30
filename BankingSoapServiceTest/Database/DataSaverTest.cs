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
using WcfBankingService.operation;

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
        private const decimal PreviousBalanceValue = 111.11m;

        private const int ValidAccountId = 1;
        private const string NewOperationTitle = "New Operation";
        private const decimal ExpectedAmount = 112.86m;

        private readonly DataSaver _dataSaver;

        public DataSaverTest()
        {
            _dataSaver = new DataSaver();
        }

        [TestMethod]
        public void SaveAccessToken_validUserId_savesData()
        {
            Assert.IsFalse(ContainsToken(ValidUserId));
            _dataSaver.SaveToken(ValidUserId, NewAccessToken);
            Assert.IsTrue(ContainsToken(ValidUserId));
        }

        [TestMethod]
        public void SaveAccessToken_notValidUserId_savesData()
        {
            Assert.IsFalse(ContainsToken(NotValidUserId));
            _dataSaver.SaveToken(NotValidUserId, NewAccessToken);
            Assert.IsTrue(ContainsToken(NotValidUserId));
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

        [TestMethod]
        public void SaveOperationRecord_validAccountId_savesData()
        {
            var operationRecord = new OperationRecord
            {
                BalanceAfterOperation = ExpectedBalanceValue,
                Amount = ExpectedAmount,
                Source = ValidInnerAccountNumber,
                Title = NewOperationTitle
            };
            Assert.IsNull(GetSavedOperation(operationRecord));
            _dataSaver.SaveOperationToHistory(ValidAccountId, operationRecord);
            var savedRecord = GetSavedOperation(operationRecord);
            Assert.IsNotNull(savedRecord);
            Assert.AreEqual(ExpectedBalanceValue, savedRecord.BalanceAfterOperation);
            Assert.AreEqual(ExpectedAmount, savedRecord.Amount);
            Assert.AreEqual(ValidInnerAccountNumber, savedRecord.Source);
            Assert.AreEqual(NewOperationTitle, savedRecord.Title);
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
                db.OperationRecord.Where(p => p.Title == NewOperationTitle).Delete();
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

        private static DbOperationRecord GetSavedOperation(OperationRecord operationRecord)
        {
            using (var db = new DbBank())
            {
                var query = from p in db.OperationRecord
                    where p.Title == operationRecord.Title
                    select p;

                return query.ToList().FirstOrDefault();
            }
        }

        private static bool ContainsToken(int userId)
        {
            return DbDataProvider.GetAccessTokenForUser(userId).Contains(NewAccessToken);
        }
    }
}