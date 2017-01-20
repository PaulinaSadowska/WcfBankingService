using System;
using WcfBankingService.Accounts.Number;
using WcfBankingService.operation;

namespace WcfBankingService.Operation.Operations
{
    /// <summary>
    /// abstract class used to share common functionalities between withdraw and deposit classes
    /// </summary>
    public abstract class BankOperation : IBankCommand
    {
        /// <summary>
        /// operation record
        /// </summary>
        public OperationRecord OperationRecord { get; }

        /// <summary>
        /// account number of account on which this operation is performed
        /// </summary>
        public AccountNumber AccountNumber;

        /// <summary>
        /// if command was executed
        /// </summary>
        public bool Executed { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="accountNumber">accout number on which operation will be performed</param>
        /// <param name="record">operation record</param>
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