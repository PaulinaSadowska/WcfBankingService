using System;
using WcfBankingService.Accounts.Number;
using WcfBankingService.operation;

namespace WcfBankingService.Operation.Operations
{
    public abstract class BankOperation : IBankCommand
    {
        public OperationRecord OperationRecord { get; }

        public AccountNumber AccountNumber;

        public bool Executed { get; set; }

        protected BankOperation(AccountNumber accountNumber, OperationRecord record)
        {
            OperationRecord = record;
            AccountNumber = accountNumber;
        }

        /// <summary>
        /// Saves balance after operation into operation record. If operation was not already executed - throw exception
        /// </summary>
        /// <param name="balance">balance value</param>
        public void RecordBalanceAfterOperation(decimal balance)
        {
            if (Executed)
                OperationRecord.BalanceAfterOperation = balance;
            else
                throw new Exception("Balance should be saved after executing the operation");
        }

        /// <summary>
        /// Checks is operation was executed. If otherwise - throw exeption
        /// </summary>
        public void CheckExecuted()
        {
            if(Executed)
                throw new Exception("Operation can't be executed more than once");
        }

        public abstract void Execute();
    }
}