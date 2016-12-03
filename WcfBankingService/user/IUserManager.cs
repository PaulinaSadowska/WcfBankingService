using System.Collections.Generic;
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
        /// Add account number to user. 
        /// </summary>
        /// <param name="accessToken">access token received after sign in</param>
        /// <param name="accountNumber">account number</param>
        /// <returns>if account number was created</returns>
        bool AddAccountNumber(string accessToken, AccountNumber accountNumber);

        /// <summary>
        /// Checks if there is user with given login
        /// </summary>
        /// <param name="login">login</param>
        /// <returns>if there is user with given login</returns>
        bool ContainsUser(string login);

        /// <summary>
        /// Returns users account number list
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="accessToken">access token received after sign in</param>
        /// <returns>Account number list</returns>
        IEnumerable<AccountNumber> GetAccountNumbers(string login, string accessToken);
    }
}