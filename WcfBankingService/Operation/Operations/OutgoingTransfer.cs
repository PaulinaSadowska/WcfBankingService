using WcfBankingService.Accounts;

namespace WcfBankingService.operation.operations
{
    public class OutgoingTransfer : Withdraw
    {
        public OutgoingTransfer(IAccount account, decimal amount, string title, string receiverAccountNumber)
            : base(account, amount, title, $"{receiverAccountNumber}")
        {
            
        }
    }
}