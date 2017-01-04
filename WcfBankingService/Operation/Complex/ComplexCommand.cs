using System.Collections.Generic;
using System.Linq;
using WcfBankingService.Operation.Operations;

namespace WcfBankingService.operation.Complex
{
    public abstract class ComplexCommand : IBankCommand
    {
        protected abstract List<BankOperation> GetOperations();

        public void Execute()
        {
            foreach (var operation in GetOperations())
            {
                operation.Execute();
            }
        }

        public List<BankOperation> GetExecutedOperations()
        {
            return GetOperations().Where(operation => operation.Executed).ToList();
        }
    }
}
