using System;
using System.Collections.Generic;
using System.Linq;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Balance;
using WcfBankingService.Accounts.Number;
using WcfBankingService.Database.Model;
using WcfBankingService.Users;

namespace WcfBankingService.Database.DataProvider
{
    public class DbDataProvider : IBankDataProvider
    {
        private static IAccountNumberFactory _accountNumberFactory;

        public DbDataProvider(IAccountNumberFactory accountNumberFactory)
        {
            _accountNumberFactory = accountNumberFactory;
        }

        public List<IUser> GetStoredData()
        {
            var users = GetUsersFromDb();
            return users;
        }

        private static List<IUser> GetUsersFromDb()
        {
            using (var db = new DbBank())
            {
                var query = from p in db.Users
                            select p;
                return query.ToList()
                    .Select(user => CreateUser(user.login, user.password, user.Id))
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
            var number = _accountNumberFactory.CreateAccountNumber(account.InnerAccountNumber);
            return new Account(number, new Balance(account.BalanceValue));
        }

        private static List<string> GetAccessTokenForUser(int id)
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