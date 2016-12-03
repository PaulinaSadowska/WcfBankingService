using System.Collections.Generic;
using System.Linq;
using WcfBankingService.account.number;

namespace WcfBankingService.user
{
    public class UserManager : IUserManager
    {
        private List<User> _users;

        public UserManager()
        {
            _users = new List<User>();
        }

        public UserManager(List<User> users)
        {
            _users = users;
        }

        public string SignIn(string login, string password)
        {
            var searchedUser = _users?.Where(user => user.Login.Equals(login));
            return searchedUser?.FirstOrDefault()?.GetAccessToken(password);
        }

        public bool SignUp(string login, string password)
        {
            throw new System.NotImplementedException();
        }

        public bool AddAccountNumber(string accessToken, AccountNumber accountNumber)
        {
            throw new System.NotImplementedException();
        }

        public bool ContainsUser(string login)
        {
            return _users.Any(user => user.Login.Equals(login));
        }

        public IEnumerable<AccountNumber> GetAccountNumbersFromUser(string accessToken)
        {
            throw new System.NotImplementedException();
        }
    }
}