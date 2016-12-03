using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfBankingService.account.number;

namespace WcfBankingService
{
    public class User
    {
        private readonly string _login;
        private readonly string _password;
        private string _accessToken; //generated after sign in
        private IEnumerable<AccountNumber> _accoutNumber;

        public User(string login, string password)
        {
            _login = login;
            _password = password;
        }
    }
}