using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;
using WcfBankingService.Users.Access;

namespace WcfBankingService.Users
{
    /// <summary>
    /// <see cref="IUser"/>
    /// </summary>
    public class User : IUser
    {
        /// <summary>
        /// Length of the access token
        /// </summary>
        public static readonly int AccessTokenLength = 12;
        private readonly string _hashedPassword;
        private readonly List<IAccount> _accouts;
        private readonly List<string> _accessTokens;

        /// <summary>
        /// user login
        /// </summary>
        public string Login { get; }

        private readonly PasswordComparator _passwordComparator;

        /// <summary>
        /// creates user with given data
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="hashedPassword">user hashed password</param>
        public User(string login, string hashedPassword) 
            : this(login, hashedPassword, new List<IAccount>(), new List<string>())
        {
            
        }

        /// <summary>
        /// creates user with given data
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="hashedPassword">user hashed password</param>
        /// <param name="accounts">account list</param>
        public User(string login, string hashedPassword, List<IAccount> accounts) 
            : this(login, hashedPassword, accounts, new List<string>())
        {

        }

        /// <summary>
        /// creates user with given data
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="hashedPassword">user hashed password</param>
        /// <param name="accounts">account list</param>
        /// <param name="accessTokens">access tokens</param>
        public User(string login, string hashedPassword, List<IAccount> accounts, List<string> accessTokens )
        {
            Login = login;
            _hashedPassword = hashedPassword;
            _accouts = accounts;
            _accessTokens = accessTokens;

            _passwordComparator = new PasswordComparator();
        }

        /// <summary>
        /// Generates access token if password matches. If not - returns null
        /// </summary>
        /// <param name="password">password</param>
        /// <returns>new access token</returns>
        public string GenerateAccessToken(string password)
        {
            if (!_passwordComparator.ArePasswordsSame(_hashedPassword, password))
                return null;

            var accessToken = AccessTokenGenerator.Generate(AccessTokenLength);
            _accessTokens.Add(accessToken);
            return accessToken;
        }

        /// <summary>
        /// <see cref="IUser.GetAllAccounts"/>
        /// </summary>
        public ICollection<IAccount> GetAllAccounts(string accessToken)
        {
            Authorize(accessToken);
            return _accouts;
        }

        /// <summary>
        /// <see cref="IUser.GetAllAccountNumbers"/>
        /// </summary>
        public ICollection<string> GetAllAccountNumbers(string accessToken)
        {
            Authorize(accessToken);
            return _accouts.Select(account => account.AccountNumber.ToString()).ToList();
        }

        /// <summary>
        /// <see cref="IUser.GetAccount"/>
        /// </summary>
        public IAccount GetAccount(string accessToken, AccountNumber accountNumber)
        {
            Authorize(accessToken);
            return _accouts?.FirstOrDefault(account => account.AccountNumber.Equals(accountNumber));
        }

        /// <summary>
        /// <see cref="IUser.GetAccount"/>
        /// </summary>
        public IPublicAccount GetAccount(AccountNumber accountNumber)
        {
            return _accouts?.FirstOrDefault(account => account.AccountNumber.Equals(accountNumber));
        }

        /// <summary>
        /// <see cref="IUser.ContainsAccount"/>
        /// </summary>
        public bool ContainsAccount(AccountNumber accountNumber)
        {
            return _accouts?.FirstOrDefault(account => account.AccountNumber.Equals(accountNumber)) != null;
        }

        /// <summary>
        /// <see cref="IUser.AddAccount"/>
        /// </summary>
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