using System.Collections.Generic;
using System.Linq;
using LinqToDB.DataProvider;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;
using WcfBankingService.Database;
using WcfBankingService.Database.DataProvider;

namespace WcfBankingService.Users
{
    public class UserManager : IUserManager
    {
        private readonly List<IUser> _users;

        public UserManager()
        {
            _users = new List<IUser>();
        }

        public UserManager(IBankDataProvider dataProvider)
        {
            _users = dataProvider.GetStoredData();
        }

        public string SignIn(string login, string password)
        {
            return GetUser(login)?.GenerateAccessToken(password);
        }

        public bool SignUp(string login, string password)
        {
            if (ContainsUser(login))
                return false;

            _users.Add(new User(login, password));
            return true;
        }

        public bool ContainsUser(string login)
        {
            return _users.Any(user => user.Login.Equals(login));
        }

        public bool AddAccount(string login, string accessToken, IAccount account)
        {
            var user = GetUser(login);
            return user != null && user.AddAccount(accessToken, account);
        }

        public IEnumerable<IAccount> GetAllAccounts(string login, string accessToken)
        {
            var user = GetUser(login);
            return user?.GetAllAccounts(accessToken);
        }

        public IAccount GetAccount(string accessToken, AccountNumber accoutNumber)
        {
            IAccount searchedAccount = null;
            foreach (var user in _users)
            {
                var a = user.GetAccount(accessToken, accoutNumber);
                if (a != null)
                    searchedAccount = a;
            }
            return searchedAccount;
        }

        private IUser GetUser(string login)
        {
            var searchedUsers = _users?.Where(user => user.Login.Equals(login));
            return searchedUsers?.FirstOrDefault();
        }
    }
}