using WcfBankingService.Accounts;
using WcfBankingService.Operation.Operations;

namespace WcfBankingService.operation.operations
{

    public class IncomingTransfer : Deposit
    {

        public IncomingTransfer(IPublicAccount account, decimal amount, string title, string senderAccountNumber)
            : base(account, amount, title, $"Incoming transfer from: {senderAccountNumber}")
        {
        }
    }
}