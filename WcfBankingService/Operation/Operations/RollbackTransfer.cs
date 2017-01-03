using WcfBankingService.Accounts;

namespace WcfBankingService.Operation.Operations
{
    public class RollbackTransfer : Deposit
    {
        public RollbackTransfer(IPublicAccount account, decimal amount, string title, string receiverAccountNumber)
            : base(account, amount, title, $"Rollback transfer to: {receiverAccountNumber}")
        {
        }
    }
}