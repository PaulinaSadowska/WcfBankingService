using WcfBankingService.Accounts.Number;
using WcfBankingService.operation;

namespace WcfBankingService.Accounts
{
    public interface IPublicAccount
    {
            AccountNumber AccountNumber { get; }
            void AddToBalance(decimal amount);
            void AddOperationToHistory(OperationRecord operationRecord);
            decimal GetBalanceValue();
        }
}
