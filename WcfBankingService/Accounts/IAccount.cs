using System.Collections.Generic;
using WcfBankingService.Accounts.Number;
using WcfBankingService.operation;

namespace WcfBankingService.Accounts
{
    public interface IAccount
    {
        AccountNumber AccountNumber { get; }
        void AddToBalance(decimal amount);
        void SubstractFromBalance(decimal amount);
        void AddOperationToHistory(OperationRecord operationRecord);
        decimal GetBalanceValue();
        IEnumerable<OperationRecord> GetOperationHistory();
    }
}