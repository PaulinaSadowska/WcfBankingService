using System.Collections.Generic;
using WcfBankingService.account.number;

namespace WcfBankingService.user
{
    public class User : IUser
    {
        private readonly string _login;
        private readonly string _password;
        public IEnumerable<AccountNumber> AccoutNumber { get; }

        public User(string login, string password)
        {
            _login = login;
            _password = password;
        }

        public string GenerateAccessToken(string password)
        {
            return password == _password ? "some access token" : null; // TODO
        }

        public bool ContainsAccessToken(string accessToken)
        {
            return true; // TODO
        }

        public string GetLogin()
        {
            return _login;
        }
    }
}