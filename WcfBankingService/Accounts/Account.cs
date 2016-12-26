using System.Collections.Generic;
using WcfBankingService.Accounts.Balance;
using WcfBankingService.Accounts.Number;
using WcfBankingService.operation;

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

        public void AddToBalance(decimal amount)
        {
            Balance.AddToBalance(amount);
        }

        public void SubstractFromBalance(decimal amount)
        {
            Balance.SubstractFromBalance(amount);
        }

        public decimal GetBalanceValue()
        {
            return Balance.GetValue();
        }
    }
}