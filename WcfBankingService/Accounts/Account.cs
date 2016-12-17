using System.Collections.Generic;
using System.Numerics;
using WcfBankingService.Accounts.Balance;
using WcfBankingService.Accounts.Number;
using WcfBankingService.operation;
using WcfBankingService.Operation.Operations;

namespace WcfBankingService.Accounts
{
    public class Account : IAccount
    { 
        public AccountNumber AccountNumber { get; }

        public IBalance Balance { get; }

        private IEnumerable<OperationRecord> _operationHistory;

        public Account(AccountNumber accountNumber, IBalance balance)
        {
            this.AccountNumber = accountNumber;
            this.Balance = balance;
        }

        public void AddToBalance(BigInteger amount)
        {
            throw new System.NotImplementedException();
        }
    }
}