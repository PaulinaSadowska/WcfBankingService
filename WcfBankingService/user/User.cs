using System.Collections.Generic;
using System.Linq;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.User
{
    public class User : IUser
    {
        private readonly string _password;
        private List<IAccount> _accouts;
        private List<string> _accessTokens;
        public string Login { get; }

        public User(string login, string password)
        {
            Login = login;
            _password = password;
            _accouts = new List<IAccount>();
            _accessTokens = new List<string>();
        }

        public string GenerateAccessToken(string password)
        {
            if (password == _password)
            {
                var accessToken = "some key";
                _accessTokens.Add(accessToken);
                return accessToken;

            }
            return null;
        }

        public IEnumerable<IAccount> GetAllAccounts(string accessToken)
        {
            return ContainsAccessToken(accessToken) ? _accouts : null;
        }

        public IAccount GetAccount(string accessToken, AccountNumber accountNumber)
        {
            return ContainsAccessToken(accessToken) ? _accouts?.FirstOrDefault(account => account.AccountNumber.Equals(accountNumber)) : null;
        }

        public bool AddAccount(string accessToken, IAccount account)
        {
            if (!ContainsAccessToken(accessToken) || _accouts.Contains(account)) return false;
            _accouts.Add(account);
            return true;
        }

        private bool ContainsAccessToken(string accessToken)
        {
            return _accessTokens.Contains(accessToken);
        }
    }
}