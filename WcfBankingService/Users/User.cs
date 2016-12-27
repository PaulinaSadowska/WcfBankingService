using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.Users
{
    public class User : IUser
    {
        public static readonly int AccessTokenLength = 12;
        private readonly string _password;
        private readonly List<IAccount> _accouts;
        private readonly List<string> _accessTokens;
        public string Login { get; }

        public User(string login, string password) 
            : this(login, password, new List<IAccount>(), new List<string>())
        {
            
        }

        public User(string login, string password, List<IAccount> accounts) 
            : this(login, password, accounts, new List<string>())
        {

        }

        public User(string login, string password, List<IAccount> accounts, List<string> accessTokens )
        {
            Login = login;
            _password = password;
            _accouts = accounts;
            _accessTokens = accessTokens;
        }

        public string GenerateAccessToken(string password)
        {
            if (password != _password) return null;

            var accessToken = AccessTokenGenerator.Generate(AccessTokenLength);
            _accessTokens.Add(accessToken);
            return accessToken;
        }

        public IEnumerable<IAccount> GetAllAccounts(string accessToken)
        {
            Authorize(accessToken);
            return _accouts;
        }

        public IAccount GetAccount(string accessToken, AccountNumber accountNumber)
        {
            Authorize(accessToken);
            return _accouts?.FirstOrDefault(account => account.AccountNumber.Equals(accountNumber));
        }

        public bool ContainsAccount(AccountNumber accountNumber)
        {
            return _accouts?.FirstOrDefault(account => account.AccountNumber.Equals(accountNumber)) != null;
        }

        public bool AddAccount(string accessToken, IAccount account)
        {
            Authorize(accessToken);
            if (_accouts.Contains(account))
            {
                return false;
            }
            _accouts.Add(account);
            return true;
        }

        private void Authorize(string accessToken)
        {
            if(!_accessTokens.Contains(accessToken))
                throw new AuthenticationException("Wrong access token");
        }
    }
}