using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;
using WcfBankingService.Users.Access;

namespace WcfBankingService.Users
{
    public class User : IUser
    {
        public static readonly int AccessTokenLength = 12;
        private readonly string _hashedPassword;
        private readonly List<IAccount> _accouts;
        private readonly List<string> _accessTokens;
        public string Login { get; }

        private readonly PasswordComparator _passwordComparator;

        public User(string login, string hashedPassword) 
            : this(login, hashedPassword, new List<IAccount>(), new List<string>())
        {
            
        }

        public User(string login, string hashedPassword, List<IAccount> accounts) 
            : this(login, hashedPassword, accounts, new List<string>())
        {

        }

        public User(string login, string hashedPassword, List<IAccount> accounts, List<string> accessTokens )
        {
            Login = login;
            _hashedPassword = hashedPassword;
            _accouts = accounts;
            _accessTokens = accessTokens;

            _passwordComparator = new PasswordComparator();
        }

        public string GenerateAccessToken(string password)
        {
            if (!_passwordComparator.ArePasswordsSame(_hashedPassword, password))
                return null;

            var accessToken = AccessTokenGenerator.Generate(AccessTokenLength);
            _accessTokens.Add(accessToken);
            return accessToken;
        }

        public ICollection<IAccount> GetAllAccounts(string accessToken)
        {
            Authorize(accessToken);
            return _accouts;
        }

        public ICollection<string> GetAllAccountNumbers(string accessToken)
        {
            Authorize(accessToken);
            return _accouts.Select(account => account.AccountNumber.ToString()).ToList();
        }

        public IAccount GetAccount(string accessToken, AccountNumber accountNumber)
        {
            Authorize(accessToken);
            return _accouts?.FirstOrDefault(account => account.AccountNumber.Equals(accountNumber));
        }

        public IPublicAccount GetAccount(AccountNumber accountNumber)
        {
            return _accouts?.FirstOrDefault(account => account.AccountNumber.Equals(accountNumber));
        }

        public bool ContainsAccount(AccountNumber accountNumber)
        {
            return _accouts?.FirstOrDefault(account => account.AccountNumber.Equals(accountNumber)) != null;
        }

        public bool AddAccount(string accessToken, IAccount account)
        {
            Authorize(accessToken);
            if (_accouts.Contains(account))
            {
                return false;
            }
            _accouts.Add(account);
            return true;
        }

        private void Authorize(string accessToken)
        {
            if(!_accessTokens.Contains(accessToken))
                throw new AuthenticationException("Wrong access token");
        }
    }
}