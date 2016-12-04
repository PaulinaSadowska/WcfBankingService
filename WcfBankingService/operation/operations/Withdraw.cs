using System.Numerics;
using WcfBankingService.Operation.Operations;

namespace WcfBankingService.operation.operations
{
    public class Withdraw : BankOperation, IBankCommand
    {
        public Withdraw(string operationTitle, BigInteger amount, string source) : base(operationTitle, amount, source)
        {
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}