using System.Collections.Generic;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.Users
{
    /// <summary>
    /// Contains operations on the user and its accounts
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Try to sign in using given login and password
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="password">password</param>
        /// <returns>access token</returns>
        string SignIn(string login, string password);

        /// <summary>
        /// Create account with given login and password
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="password">password</param>
        /// <returns>if operation succeeded</returns>
        bool SignUp(string login, string password);

        /// <summary>
        /// Check if there is user in the manager with given login
        /// </summary>
        /// <param name="login">user login</param>
        /// <returns>if user exists</returns>
        bool ContainsUser(string login);

        /// <summary>
        /// Add accoount to user
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="accessToken">access token used to verify access permission</param>
        /// <param name="account">account to add</param>
        /// <returns>if operation succeeded</returns>
        bool AddAccount(string login, string accessToken, IAccount account);

        /// <summary>
        /// Get all acounts of the user
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="accessToken">access token used to verify access permission</param>
        /// <returns>user accounts list</returns>
        ICollection<IAccount> GetAllAccounts(string login, string accessToken);

        /// <summary>
        /// Get all acount numbers of the user
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="accessToken">access token used to verify access permission</param>
        /// <returns>user account numbers list</returns>
        ICollection<string> GetAllAccountNumbers(string login, string accessToken);

        /// <summary>
        /// Get account interface for given account number
        /// </summary>
        /// <param name="accessToken">access token used to verify access permission</param>
        /// <param name="accoutNumber">account number</param>
        /// <returns>account object</returns>
        IAccount GetAccount(string accessToken, AccountNumber accoutNumber);

        /// <summary>
        /// Get account interface which doesn't require authorization for given account number 
        /// </summary>
        /// <param name="accoutNumber">account number</param>
        /// <returns>public account object</returns>
        IPublicAccount GetAccount(AccountNumber accoutNumber);

    }
}