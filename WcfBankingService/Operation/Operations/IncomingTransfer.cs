using WcfBankingService.Accounts;
using WcfBankingService.Operation.Operations;

namespace WcfBankingService.operation.operations
{
    /// <summary>
    /// Transfer performed to this account (money are deposited)
    /// </summary>
    public class IncomingTransfer : Deposit
    {
        /// <param name="account">receiver account number</param>
        /// <param name="amount">amount to transfer</param>
        /// <param name="title">operation title</param>
        /// <param name="senderAccountNumber">sendar account number</param>
        public IncomingTransfer(IPublicAccount account, decimal amount, string title, string senderAccountNumber)
            : base(account, amount, title, $"{senderAccountNumber}")
        {
        }
    }
}