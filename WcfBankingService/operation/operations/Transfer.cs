using WcfBankingService.Operation.Operations;

namespace WcfBankingService.operation.operations
{
    public class Transfer : BankOperation, IBankCommand
    {
        public Transfer(string operationTitle, decimal amount, string source) : base(operationTitle, amount, source)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}