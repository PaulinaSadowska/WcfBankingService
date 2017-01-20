using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.Operation.Operations
{
    /// <summary>
    /// Refund money withdrawed from account during unsuccessfull transfer
    /// </summary>
    public class RollbackTransfer : Deposit
    {
        /// <param name="account">sender account number</param>
        /// <param name="amount">amount to transfer</param>
        /// <param name="title">operation title</param>
        /// <param name="receiverAccountNumber">receiver  account number</param>
        public RollbackTransfer(IPublicAccount account, decimal amount, string title, AccountNumber receiverAccountNumber)
            : base(account, amount, title, $"{receiverAccountNumber}")
        {
        }
    }
}