using System.Collections.Generic;
using WcfBankingService.operation;

namespace WcfBankingService.Accounts
{
    public interface IAccount : IPublicAccount
    {
        void SubstractFromBalance(decimal amount);
        IEnumerable<OperationRecord> GetOperationHistory();
    }
}