using System.Collections.Generic;
using System.Linq;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.User
{
    public class User : IUser
    {
        private readonly string _password;
        public List<IAccount> Accouts;
        public string Login { get; }

        public User(string login, string password)
        {
            Login = login;
            _password = password;
            Accouts = new List<IAccount>();
        }

        public string GenerateAccessToken(string password)
        {
            return password == _password ? "some access token" : null; // TODO
        }

        public IEnumerable<IAccount> GetAllAccounts(string accessToken)
        {
            return ContainsAccessToken(accessToken) ? Accouts : null;
        }

        public IAccount GetAccount(string accessToken, AccountNumber accountNumber)
        {
            return ContainsAccessToken(accessToken) ? Accouts?.FirstOrDefault(account => account.AccountNumber.Equals(accountNumber)) : null;
        }

        public bool AddAccount(string accessToken, IAccount account)
        {
            if (!ContainsAccessToken(accessToken) || Accouts.Contains(account)) return false;
            Accouts.Add(account);
            return true;
        }

        private bool ContainsAccessToken(string accessToken)
        {
            return true; // TODO
        }
    }
}