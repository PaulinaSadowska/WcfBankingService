using System.Collections.Generic;
using WcfBankingService.Accounts;
using WcfBankingService.operation.operations;
using WcfBankingService.Operation.Operations;
using WcfBankingService.Service.DataContract.Response;

namespace WcfBankingService.operation.Complex
{
    public class InnerBankTransfer : ComplexCommand
    {
        private readonly List<BankOperation> _operations;


        public InnerBankTransfer(IAccount sender , IPublicAccount receiver , 
            int amountInCents, string operationTitle)
        {
            var amount = amountInCents / 100m;
            ResponseStatus = ResponseStatus.Success;
            _operations = new List<BankOperation>
            {
                new OutgoingTransfer(sender, amount, operationTitle, receiver.AccountNumber.ToString()),
                new IncomingTransfer(receiver, amount, operationTitle, sender.AccountNumber.ToString())
            };
        }


        protected override List<BankOperation> GetOperations()
        {
            return _operations;
        }

        protected override void Rollback()
        {
            // do nothing
        }
    }
}