using System.Collections.Generic;
using System.Linq;
using WcfBankingService.account;
using WcfBankingService.account.number;

namespace WcfBankingService.User
{
    public class User : IUser
    {
        private readonly string _password;
        public List<Account> Accouts;
        public string Login { get; }

        public User(string login, string password)
        {
            Login = login;
            _password = password;
            Accouts = new List<Account>();
        }

        public string GenerateAccessToken(string password)
        {
            return password == _password ? "some access token" : null; // TODO
        }

        public IEnumerable<Account> GetAllAccounts(string accessToken)
        {
            return ContainsAccessToken(accessToken) ? Accouts : null;
        }

        public Account GetAccount(string accessToken, AccountNumber accountNumber)
        {
            return ContainsAccessToken(accessToken) ? Accouts?.FirstOrDefault(accout => accout.AccountNumber.Equals(accountNumber)) : null;
        }

        public bool AddAccount(string accessToken, Account account)
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