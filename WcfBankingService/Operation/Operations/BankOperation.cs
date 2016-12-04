using System;
using System.Numerics;
using WcfBankingService.operation;

namespace WcfBankingService.Operation.Operations
{
    public class BankOperation
    {
        public OperationRecord OperationRecord { get; }

        public bool Executed { get; set; }

        protected BankOperation(string operationTitle, BigInteger amount, string source)
        {
            OperationRecord = new OperationRecord
            {
                Title = operationTitle,
                Amount = amount,
                Source = source
            };
        }

        public void SetBalanceAfterOperation(BigInteger balance)
        {
            if (Executed)
                OperationRecord.BalanceAfterOperation = balance;
        }

        public void CheckExecuted()
        {
            if(Executed)
                throw new Exception("Operation can't be executed more than once");
        }
    }
}