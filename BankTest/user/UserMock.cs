using System.Collections.Generic;
using WcfBankingService.account.number;
using WcfBankingService.User;

namespace BankTest.user
{
    internal class UserMock : IUser
    {
        private readonly string _accessToken;

        public UserMock(string login, string accessToken)
        {
            this.Login = login;
            this._accessToken = accessToken;
        }

        public string GenerateAccessToken(string password)
        {
            return _accessToken;
        }

        public IEnumerable<AccountNumber> GetAccountNumbers(string accessToken)
        {
            return _accessToken == accessToken ? new List<AccountNumber>{new AccountNumber("", "", "")} : null;
        }


        public string Login { get; }

        public bool AddAccountNumber(string accessToken, AccountNumber accountNumber)
        {
            if (_accessToken == accessToken)
                return false;
            return true;
        }
    }
}