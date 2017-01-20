using WcfBankingService.Accounts;

namespace WcfBankingService.operation.operations
{
    /// <summary>
    /// Transfer performed from this account (money are withdrawed)
    /// </summary>
    public class OutgoingTransfer : Withdraw
    {
        public OutgoingTransfer(IAccount account, decimal amount, string title, string receiverAccountNumber)
            : base(account, amount, title, $"{receiverAccountNumber}")
        {
            
        }
    }
}