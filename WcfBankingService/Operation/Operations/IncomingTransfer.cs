using WcfBankingService.Accounts;
using WcfBankingService.Operation.Operations;

namespace WcfBankingService.operation.operations
{
    /// <summary>
    /// Transfer performed to this account (money are deposited)
    /// </summary>
    public class IncomingTransfer : Deposit
    {

        public IncomingTransfer(IPublicAccount account, decimal amount, string title, string senderAccountNumber)
            : base(account, amount, title, $"{senderAccountNumber}")
        {
        }
    }
}