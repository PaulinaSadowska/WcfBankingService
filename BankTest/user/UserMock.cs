using System.Collections.Generic;
using WcfBankingService.account;
using WcfBankingService.account.number;
using WcfBankingService.User;

namespace BankTest.User
{
    internal class UserMock : IUser
    {
        private readonly string _accessToken;
        public string Login { get; }

        public UserMock(string login, string accessToken)
        {
            this.Login = login;
            this._accessToken = accessToken;
        }

        public string GenerateAccessToken(string password)
        {
            return _accessToken;
        }

        public IEnumerable<Account> GetAllAccounts(string accessToken)
        {
            return _accessToken == accessToken ? new List<Account>() : null;
        }

        public Account GetAccount(string accessToken, AccountNumber accountNumber)
        {
            throw new System.NotImplementedException();
        }

        public bool AddAccount(string accessToken, Account account)
        {
            return _accessToken != accessToken;
        }

    }
}