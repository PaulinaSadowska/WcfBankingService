using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfBankingService.accunt;

namespace WcfBankingService
{
    public class User
    {
        public string login;
        public string password;
        public IEnumerable<AccountNumber> accoutNumber;
    }
}