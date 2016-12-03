using System.Collections.Generic;
using System.Linq;
using WcfBankingService.account.number;

namespace WcfBankingService.user
{
    public class UserManager : IUserManager
    {
        private List<IUser> _users;

        public UserManager()
        {
            _users = new List<IUser>();
        }

        public UserManager(List<IUser> users)
        {
            _users = users;
        }

        public string SignIn(string login, string password)
        {
            return GetUser(login)?.GenerateAccessToken(password);
        }

        public bool SignUp(string login, string password)
        {
            if (ContainsUser(login))
                return false;

            _users.Add(new User(login, password));
            return true;
        }

        public bool AddAccountNumber(string accessToken, AccountNumber accountNumber)
        {
            throw new System.NotImplementedException();
        }

        public bool ContainsUser(string login)
        {
            return _users.Any(user => user.Login.Equals(login));
        }

        public IEnumerable<AccountNumber> GetAccountNumbers(string login, string accessToken)
        {
            var user = GetUser(login);
            if(user!=null && user.ContainsAccessToken(accessToken))
                return user.AccoutNumber;
            return null;
        }

        private IUser GetUser(string login)
        {
            var searchedUsers = _users?.Where(user => user.Login.Equals(login));
            return searchedUsers?.FirstOrDefault();
        }
    }
}