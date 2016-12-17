using System.Collections.Generic;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;
using WcfBankingService.User;

namespace BankTest.User
{
    internal class UserMock : IUser
    {
        private readonly string _accessToken;
        public string Login { get; }

        public UserMock(string login, string accessToken)
        {
            Login = login;
            _accessToken = accessToken;
        }

        public string GenerateAccessToken(string password)
        {
            return _accessToken;
        }

        public IEnumerable<IAccount> GetAllAccounts(string accessToken)
        {
            return _accessToken == accessToken ? new List<IAccount>() : null;
        }

        public IAccount GetAccount(string accessToken, AccountNumber accountNumber)
        {
            throw new System.NotImplementedException();
        }

        public bool AddAccount(string accessToken, IAccount account)
        {
            return _accessToken != accessToken;
        }

    }
}