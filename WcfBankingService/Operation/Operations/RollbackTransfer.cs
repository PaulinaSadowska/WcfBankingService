using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.Operation.Operations
{
    public class RollbackTransfer : Deposit
    {
        public RollbackTransfer(IPublicAccount account, decimal amount, string title, AccountNumber receiverAccountNumber)
            : base(account, amount, title, $"{receiverAccountNumber}")
        {
        }
    }
}