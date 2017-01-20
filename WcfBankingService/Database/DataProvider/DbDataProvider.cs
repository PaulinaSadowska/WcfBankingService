using System.Collections.Generic;
using System.Linq;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Balance;
using WcfBankingService.Accounts.Number;
using WcfBankingService.Database.Model;
using WcfBankingService.operation;
using WcfBankingService.Users;

namespace WcfBankingService.Database.DataProvider
{
    /// <summary>
    /// <see cref="IBankDataProvider"/>
    /// </summary>
    public class DbDataProvider : IBankDataProvider
    {
        private static IAccountNumberFactory _accountNumberFactory;

        public DbDataProvider(IAccountNumberFactory accountNumberFactory)
        {
            _accountNumberFactory = accountNumberFactory;
        }

        /// <summary>
        /// <see cref="IBankDataProvider.GetStoredData"/>
        /// </summary>
        public List<IUser> GetStoredData()
        {
            return GetUsersFromDb();
        }

        private static List<IUser> GetUsersFromDb()
        {
            using (var db = new DbBank())
            {
                var query = from p in db.Users
                            select p;
                return query.ToList()
                    .Select(user => CreateUser(user.Login, user.HashedPassword, user.Id))
                    .ToList();
            }
        }

        private static IUser CreateUser(string login, string password, int userId)
        {
            return new User(login, password, GetAccountsForUser(userId), GetAccessTokenForUser(userId));
        }

        private static List<IAccount> GetAccountsForUser(int id)
        {
            using (var db = new DbBank())
            {
                var query = from p in db.Accounts
                            where p.UserId == id
                            select p;
                return query.ToList()
                    .Select(CreateAccount)
                    .Cast<IAccount>()
                    .ToList();
            }
        }

        private static Account CreateAccount(DbAccount account)
        {
            var number = _accountNumberFactory.GetAccountNumberFromInner(account.InnerAccountNumber);
            return new Account(number, new Balance(account.BalanceValue), GetOperationRecords(account.Id));
        }

        private static List<OperationRecord> GetOperationRecords(int id)
        {
            using (var db = new DbBank())
            {
                var query = from p in db.OperationRecord
                            where p.AccountId == id
                            select p;
                return query.ToList()
                    .Select(GetOperationRecord)
                    .ToList();
            }
        }

        private static OperationRecord GetOperationRecord(DbOperationRecord record)
        {
            return new OperationRecord
            {
                Title =  record.Title,
                Source = record.Source,
                Debet = record.Debet,
                Credit =  record.Credit,
                BalanceAfterOperation = record.BalanceAfterOperation
            };
        }

        public static List<string> GetAccessTokenForUser(int id)
        {
            using (var db = new DbBank())
            {
                var query = from p in db.AccessTokens
                            where p.UserId == id
                            select p;
                return query.ToList()
                    .Select(token => token.Token)
                    .ToList();
            }
        }
    }
}