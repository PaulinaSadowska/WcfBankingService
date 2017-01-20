using System.Collections.Generic;
using WcfBankingService.Accounts.Balance;
using WcfBankingService.Accounts.Number;
using WcfBankingService.operation;

namespace WcfBankingService.Accounts
{
    /// <summary>
    /// stores account informations
    /// </summary>
    public class Account : IAccount
    {
        /// <summary>
        /// account number
        /// </summary>
        public AccountNumber AccountNumber { get; }

        /// <summary>
        /// balance
        /// </summary>
        public IBalance Balance { get; }

        /// <summary>
        /// records of operations performed on this account
        /// </summary>
        private readonly IList<OperationRecord> _operationHistory;

        public Account(AccountNumber accountNumber, IBalance balance)
            : this(accountNumber, balance, new List<OperationRecord>())
        {
        }

        public Account(AccountNumber accountNumber, IBalance balance, IList<OperationRecord> operationRecord)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            _operationHistory = operationRecord;
        }

        /// <summary>
        /// <see cref="IPublicAccount.AddToBalance"/>
        /// </summary>
        public void AddToBalance(decimal amount)
        {
            Balance.AddToBalance(amount);
        }

        /// <summary>
        /// <see cref="IAccount.SubstractFromBalance"/>
        /// </summary>
        public void SubstractFromBalance(decimal amount)
        {
            Balance.SubstractFromBalance(amount);
        }

        /// <summary>
        /// <see cref="IPublicAccount.AddOperationToHistory"/>
        /// </summary>
        public void AddOperationToHistory(OperationRecord operationRecord)
        {
            _operationHistory.Add(operationRecord);
        }

        /// <summary>
        /// <see cref="IAccount.GetBalanceValue"/>
        /// </summary>
        public decimal GetBalanceValue()
        {
            return Balance.GetValue();
        }

        /// <summary>
        /// <see cref="IAccount.GetOperationHistory"/>
        /// </summary>
        public IEnumerable<OperationRecord> GetOperationHistory()
        {
            return _operationHistory;
        }
    }
}