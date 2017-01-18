using System.Collections.Generic;
using System.Linq;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;
using WcfBankingService.Database.DataProvider;
using WcfBankingService.Service.DataContract.Response;

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
            var token = GetUser(login)?.GenerateAccessToken(password);
            if(token == null)
                throw new BankException(ResponseStatus.IncorrectLoginOrPassword);
            return token;
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

        public ICollection<IAccount> GetAllAccounts(string login, string accessToken)
        {
            return GetUser(login)?.GetAllAccounts(accessToken);
        }

        public ICollection<string> GetAllAccountNumbers(string login, string accessToken)
        {
            return GetUser(login)?.GetAllAccountNumbers(accessToken);
        }

        public IAccount GetAccount(string accessToken, AccountNumber accoutNumber)
        {
            IAccount searchedAccount = null;
            foreach (var user in _users)
            {
                if (!user.ContainsAccount(accoutNumber)) continue;
                var a = user.GetAccount(accessToken, accoutNumber);
                if (a != null)
                    searchedAccount = a;
            }
            return searchedAccount;
        }

        public IPublicAccount GetAccount(AccountNumber accoutNumber)
        {
            IPublicAccount searchedAccount = null;
            foreach (var user in _users)
            {
                if (!user.ContainsAccount(accoutNumber)) continue;
                var a = user.GetAccount(accoutNumber);
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