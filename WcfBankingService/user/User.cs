using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfBankingService.account.number;

namespace WcfBankingService
{
    public class User
    {
        public string Login { get; }
        private string _password;
        private IEnumerable<AccountNumber> _accoutNumber;

        public User(string login, string password)
        {
            Login = login;
            _password = password;
        }

        public string GetAccessToken(string password)
        {
            return password == _password ? "some access token" : null;
        }
    }
}