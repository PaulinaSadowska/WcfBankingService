using System.Collections.Generic;
using WcfBankingService.operation;

namespace WcfBankingService.Accounts
{
    /// <summary>
    /// Interface used to perform actions on the account (including actions which require authorization)
    /// </summary>
    public interface IAccount : IPublicAccount
    {
        /// <summary>
        /// Substract amount from account balance value 
        /// </summary>
        /// <param name="amount">amount to substract</param>
        void SubstractFromBalance(decimal amount);

        /// <summary>
        /// Get history of all operations
        /// </summary>
        /// <returns>list of all operations performed on this account</returns>
        IEnumerable<OperationRecord> GetOperationHistory();
    }
}