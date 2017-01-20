using System.Collections.Generic;
using WcfBankingService.Accounts;
using WcfBankingService.Operation.Operations;

namespace WcfBankingService.Database.SavingData
{
    /// <summary>
    /// inserts data into bank database
    /// </summary>
    public interface IBankDataInserter
    {
        /// <summary>
        /// Saves access token for given login
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="accessToken">access token to save</param>
        void SaveAccessToken(string login, string accessToken);

        /// <summary>
        /// saves operation info into database
        /// </summary>
        /// <param name="operation">operation which record should be saved</param>
        void SaveOperation(BankOperation operation);

        /// <summary>
        /// saves operation list into database
        /// </summary>
        /// <param name="operations">operations which record should be saved</param>
        void SaveOperations(List<BankOperation> operations);

        /// <summary>
        /// Saves current account balance
        /// </summary>
        /// <param name="account">account which balance should be saved</param>
        void SaveAccountBalance(IPublicAccount account);
    }
}
