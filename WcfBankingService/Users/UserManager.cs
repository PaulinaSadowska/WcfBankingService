using System.Collections.Generic;
using System.Linq;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;
using WcfBankingService.Database.DataProvider;
using WcfBankingService.Service.DataContract.Response;

namespace WcfBankingService.Users
{
    /// <summary>
    /// <see cref="IUserManager"/>
    /// </summary>
    public class UserManager : IUserManager
    {
        private readonly List<IUser> _users;

        /// <summary>
        /// creates user manager with empty user list
        /// </summary>
        public UserManager()
        {
            _users = new List<IUser>();
        }

        /// <summary>
        /// creates user manager with user list from data provider
        /// </summary>
        /// <param name="dataProvider">provides user informatins from the data source</param>
        public UserManager(IBankDataProvider dataProvider)
        {
            _users = dataProvider.GetStoredData();
        }

        /// <summary>
        /// Sign in. If login or password incorreect - throws bank exception
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        /// <returns>new access token</returns>
        public string SignIn(string login, string password)
        {
            var token = GetUser(login)?.GenerateAccessToken(password);
            if(token == null)
                throw new BankException(ResponseStatus.IncorrectLoginOrPassword);
            return token;
        }

        /// <summary>
        /// Sign up. 
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        /// <returns>If user with givel login already exists - return false. Otherwise return true</returns>
        public bool SignUp(string login, string password)
        {
            if (ContainsUser(login))
                return false;

            _users.Add(new User(login, password));
            return true;
        }

        /// <summary>
        /// Checks if user manager contains user with given login
        /// </summary>
        /// <param name="login"></param>
        /// <returns>true if contains user with given login</returns>
        public bool ContainsUser(string login)
        {
            return _users.Any(user => user.Login.Equals(login));
        }

        /// <summary>
        /// <see cref="IUserManager.AddAccount"/>
        /// </summary>
        public bool AddAccount(string login, string accessToken, IAccount account)
        {
            var user = GetUser(login);
            return user != null && user.AddAccount(accessToken, account);
        }

        /// <summary>
        /// <see cref="IUserManager.GetAllAccounts"/>
        /// </summary>
        public ICollection<IAccount> GetAllAccounts(string login, string accessToken)
        {
            return GetUser(login)?.GetAllAccounts(accessToken);
        }

        /// <summary>
        /// <see cref="IUserManager.GetAllAccountNumbers"/>
        /// </summary>
        public ICollection<string> GetAllAccountNumbers(string login, string accessToken)
        {
            return GetUser(login)?.GetAllAccountNumbers(accessToken);
        }

        /// <summary>
        /// <see cref="IUserManager.GetAccount"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="IUserManager.GetAccount"/>
        /// </summary>
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