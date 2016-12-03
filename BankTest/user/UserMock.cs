using System.Collections.Generic;
using WcfBankingService.account.number;
using WcfBankingService.User;

namespace BankTest.user
{
    internal class UserMock : IUser
    {
        private readonly string _login;
        private readonly string _accessToken;

        public UserMock(string login, string accessToken)
        {
            this._login = login;
            this._accessToken = accessToken;
        }

        public string GenerateAccessToken(string password)
        {
            return _accessToken;
        }

        public IEnumerable<AccountNumber> GetAccountNumbers(string accessToken)
        {
            if (_accessToken == accessToken)
                return new List<AccountNumber>{new AccountNumber("", "", "")};
            return null;
        }


        public string GetLogin()
        {
            return _login;
        }
    }
}