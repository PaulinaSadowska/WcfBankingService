using System.Collections.Generic;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.Users
{
    /// <summary>
    /// User containing operation record and it's accounts
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// login of the user
        /// </summary>
        string Login { get; }

        /// <summary>
        /// Generate access token if password mathes hashed password of the user
        /// </summary>
        /// <param name="password">given password</param>
        /// <returns>access token</returns>
        string GenerateAccessToken(string password);

        /// <summary>
        /// Returns all accounts of the user
        /// </summary>
        /// <param name="accessToken">access token used to verify access permission</param>
        /// <returns>user accounts</returns>
        ICollection<IAccount> GetAllAccounts(string accessToken);

        /// <summary>
        /// Returns all account numbers of the user
        /// </summary>
        /// <param name="accessToken">access token used to verify access permission</param>
        /// <returns>user account numbers</returns>
        ICollection<string> GetAllAccountNumbers(string accessToken);

        /// <summary>
        /// Get account interface for given account number
        /// </summary>
        /// <param name="accessToken">access token used to verify access permission</param>
        /// <param name="accountNumber">account number</param>
        /// <returns>account object</returns>
        IAccount GetAccount(string accessToken, AccountNumber accountNumber);

        /// <summary>
        /// Get account interface which doesn't require authorization for given account number 
        /// </summary>
        /// <param name="accountNumber">account number</param>
        /// <returns>public account object</returns>
        IPublicAccount GetAccount(AccountNumber accountNumber);

        /// <summary>
        /// Check if user owns account with given account number
        /// </summary>
        /// <param name="accountNumber">account number</param>
        /// <returns>if user owns account with given account number</returns>
        bool ContainsAccount(AccountNumber accountNumber);

        /// <summary>
        /// Adds account to the user
        /// </summary>
        /// <param name="accessToken">access token used to verify access permission</param>
        /// <param name="account">account to add</param>
        /// <returns>if account was added successfuly</returns>
        bool AddAccount(string accessToken, IAccount account);
    }
}