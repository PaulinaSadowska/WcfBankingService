using WcfBankingService.Accounts;

namespace WcfBankingService.operation.operations
{
    /// <summary>
    /// Transfer performed from this account (money are withdrawed)
    /// </summary>
    public class OutgoingTransfer : Withdraw
    {
        /// <param name="account">sender account number</param>
        /// <param name="amount">amount to transfer</param>
        /// <param name="title">operation title</param>
        /// <param name="receiverAccountNumber">receiver account number</param>
        public OutgoingTransfer(IAccount account, decimal amount, string title, string receiverAccountNumber)
            : base(account, amount, title, $"{receiverAccountNumber}")
        {
            
        }
    }
}