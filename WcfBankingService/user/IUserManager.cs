using System.Collections.Generic;
using WcfBankingService.account;
using WcfBankingService.account.number;

namespace WcfBankingService.User
{
    public interface IUserManager
    {
        /// <summary>
        /// If login and password is correct - returns access token which can be used to obtain account numbers. 
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="password">user password</param>
        /// <returns>access token, needed to obtain accout number</returns>
        string SignIn(string login, string password);

        /// <summary>
        /// Creates account with given login and password
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="password">user password</param>
        /// <returns>if user account was created</returns>
        bool SignUp(string login, string password);

        /// <summary>
        /// Checks if there is user with given login
        /// </summary>
        /// <param name="login">login</param>
        /// <returns>if there is user with given login</returns>
        bool ContainsUser(string login);

        /// <summary>
        /// Add account to user. 
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="accessToken">access token received after sign in</param>
        /// <param name="account">account</param>
        /// <returns>if account number was created</returns>
        bool AddAccount(string login, string accessToken, Account account);

        /// <summary>
        /// Returns users accounts list
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="accessToken">access token received after sign in</param>
        /// <returns>Account list</returns>
        IEnumerable<Account> GetAllAccounts(string login, string accessToken);

        /// <summary>
        /// Returns user account
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="accessToken">access token received after sign in</param>
        /// <param name="accoutNumber">account number</param>
        /// <returns>Account </returns>
        Account GetAccount(string login, string accessToken, AccountNumber accoutNumber);



    }
}