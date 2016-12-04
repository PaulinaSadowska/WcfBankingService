using System.Numerics;
using WcfBankingService.Operation.Operations;

namespace WcfBankingService.operation.operations
{
    public class Transfer : BankOperation, IBankCommand
    {
        public Transfer(string operationTitle, BigInteger amount, string source) : base(operationTitle, amount, source)
        {
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}