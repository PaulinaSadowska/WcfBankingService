using System.Collections.Generic;
using WcfBankingService.account.number;

namespace WcfBankingService.User
{
    public class User : IUser
    {
        private readonly string _login;
        private readonly string _password;
        public IEnumerable<AccountNumber> AccoutNumbers { get; }

        public User(string login, string password)
        {
            _login = login;
            _password = password;
        }

        public string GenerateAccessToken(string password)
        {
            return password == _password ? "some access token" : null; // TODO
        }

        public IEnumerable<AccountNumber> GetAccountNumbers(string accessToken)
        {
            if (ContainsAccessToken(accessToken))
                return AccoutNumbers;
            return null;
        }

        private bool ContainsAccessToken(string accessToken)
        {
            return true; // TODO
        }

        public string GetLogin()
        {
            return _login;
        }
    }
}