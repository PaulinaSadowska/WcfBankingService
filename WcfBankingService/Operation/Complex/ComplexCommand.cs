using System.Collections.Generic;
using System.Linq;
using WcfBankingService.Operation.Operations;
using WcfBankingService.Service.DataContract.Response;

namespace WcfBankingService.operation.Complex
{
    public abstract class ComplexCommand : IBankCommand
    {
        protected abstract List<BankOperation> GetOperations();
        protected abstract void Rollback();

        public ResponseStatus ResponseStatus { get; protected set; }
        public bool Executed
        {
            get { return GetOperations().All(operation => operation.Executed); }
        }

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

        public List<BankOperation> GetExecutedOperations()
        {
            return GetOperations().Where(operation => operation.Executed).ToList();
        }
    }
}
