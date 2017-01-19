using WcfBankingService.Accounts.Number;
using WcfBankingService.operation;

namespace WcfBankingService.Accounts
{
    /// <summary>
    /// Interface used to perform actions on the account which don't require authorization
    /// </summary>
    public interface IPublicAccount
    {
        AccountNumber AccountNumber { get; }

        /// <summary>
        /// Add amount to balance value
        /// </summary>
        /// <param name="amount">amount to add</param>
        void AddToBalance(decimal amount);

        /// <summary>
        /// Add operation record to account operation history
        /// </summary>
        /// <param name="operationRecord">operation record</param>
        void AddOperationToHistory(OperationRecord operationRecord);

        /// <summary>
        /// Get current balance value
        /// </summary>
        /// <returns>balance value</returns>
        decimal GetBalanceValue();
    }
}