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

        private readonly IList<OperationRecord> _operationHistory;

        public Account(AccountNumber accountNumber, IBalance balance)
            : this(accountNumber, balance, new List<OperationRecord>())
        {
        }

        public Account(AccountNumber accountNumber, IBalance balance, List<OperationRecord> operationRecord)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            _operationHistory = operationRecord;
        }

        public void AddToBalance(decimal amount)
        {
            Balance.AddToBalance(amount);
        }

        public void SubstractFromBalance(decimal amount)
        {
            Balance.SubstractFromBalance(amount);
        }

        public void AddOperationToHistory(OperationRecord operationRecord)
        {
            _operationHistory.Add(operationRecord);
        }

        public decimal GetBalanceValue()
        {
            return Balance.GetValue();
        }

        public IEnumerable<OperationRecord> GetOperationHistory()
        {
            return _operationHistory;
        }
    }
}