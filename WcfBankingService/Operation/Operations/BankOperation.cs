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

        public void RecordBalanceAfterOperation(decimal balance)
        {
            if (Executed)
                OperationRecord.BalanceAfterOperation = balance;
            else
                throw new Exception("Balance should be saved after executing the operation");
        }

        public void CheckExecuted()
        {
            if(Executed)
                throw new Exception("Operation can't be executed more than once");
        }

        public abstract void Execute();
    }
}