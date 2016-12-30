﻿using System;
using WcfBankingService.operation;

namespace WcfBankingService.Operation.Operations
{
    public abstract class BankOperation : IBankCommand
    {
        public OperationRecord OperationRecord { get; }

        public bool Executed { get; set; }

        protected BankOperation(string operationTitle, decimal amount, string source)
        {
            OperationRecord = new OperationRecord
            {
                Title = operationTitle,
                Amount = amount,
                Source = source
            };
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