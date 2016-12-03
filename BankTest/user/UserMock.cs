using WcfBankingService.user;

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

        public bool ContainsAccessToken(string accessToken)
        {
            return _accessToken.Equals(accessToken);
        }

        public string GetLogin()
        {
            return _login;
        }
    }
}
