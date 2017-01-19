using System.Collections.Generic;
using System.Linq;
using WcfBankingService.Operation.Operations;
using WcfBankingService.Service.DataContract.Response;

namespace WcfBankingService.operation.Complex
{
    /// <summary>
    /// Creates and executes sequence of regular bank operations
    /// </summary>
    public abstract class ComplexCommand : IBankCommand
    {
        protected abstract List<BankOperation> GetOperations();
        protected abstract void Rollback();

        public ResponseStatus ResponseStatus { get; protected set; }

        /// <summary>
        /// If all operations was already executed
        /// </summary>
        public bool Executed
        {
            get { return GetOperations().All(operation => operation.Executed); }
        }

        /// <summary>
        /// Execute sequence of operations. If any of them throws exception - start Rollback sequence
        /// </summary>
        public void Execute()
        {
            try
            {
                foreach (var operation in GetOperations())
                {
                    operation.Execute();
                }
            }
            catch (BankException e)
            {
                Rollback();
                ResponseStatus = e.ResponseStatus;
            }
        }

        /// <summary>
        /// Get list of executed operations in sequence
        /// </summary>
        /// <returns>list of executed operations</returns>
        public List<BankOperation> GetExecutedOperations()
        {
            return GetOperations().Where(operation => operation.Executed).ToList();
        }
    }
}
