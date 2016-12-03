using System.Collections.Generic;
using WcfBankingService.account.number;

namespace WcfBankingService.User
{
    public class User : IUser
    {
        private readonly string _password;
        public List<AccountNumber> AccoutNumbers;

        public User(string login, string password)
        {
            Login = login;
            _password = password;
            AccoutNumbers = new List<AccountNumber>();
        }

        public string GenerateAccessToken(string password)
        {
            return password == _password ? "some access token" : null; // TODO
        }

        public IEnumerable<AccountNumber> GetAccountNumbers(string accessToken)
        {
            return ContainsAccessToken(accessToken) ? AccoutNumbers : null;
        }

        private bool ContainsAccessToken(string accessToken)
        {
            return true; // TODO
        }

        public string Login { get; }

        public bool AddAccountNumber(string accessToken, AccountNumber accountNumber)
        {
            if (!ContainsAccessToken(accessToken) || AccoutNumbers.Contains(accountNumber)) return false;
            AccoutNumbers.Add(accountNumber);
            return true;
        }
    }
}