using System.Collections.Generic;
using System.Web.UI;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;
using WcfBankingService.operation.Complex;
using WcfBankingService.operation.operations;
using WcfBankingService.Operation.Operations;
using WcfBankingService.Service.DataContract.Response;
using WcfBankingService.SoapService.DataContract.Response;

namespace WcfBankingService.Operation.Complex
{
    public class InterBankTransfer : ComplexCommand
    {
        private readonly List<BankOperation> _operations;

        private readonly IAccount _sender;
        private readonly AccountNumber _receiverAccountNumber;
        private readonly decimal _amount;
        private readonly string _operationTitle;
        private ResponseStatus _responseStatus;

        public InterBankTransfer(IAccount sender, AccountNumber receiverAccountNumber,
            int amountInCents, string operationTitle)
        {
            var amount = amountInCents / 100m;
            _operations = new List<BankOperation>
            {
                new OutgoingTransfer(sender, amount, operationTitle, receiverAccountNumber.ToString()),
                new RestTransfer(sender, amount, operationTitle, receiverAccountNumber)
            };
            _sender = sender;
            _receiverAccountNumber = receiverAccountNumber;
            _operationTitle = operationTitle;
            _amount = amount;
            _responseStatus = ResponseStatus.Success;
        }

        public new void Execute()
        {
            try
            {
                base.Execute();
            }
            catch (BankException e)
            {
                Rollback();
                _responseStatus = e.ResponseStatus;
            }
            if (_responseStatus!=ResponseStatus.Success)
                throw new BankException(_responseStatus);
        }

        private void Rollback()
        {
            if (!_operations[0].Executed) return;

            var rollback = new RollbackTransfer(_sender, _amount, _operationTitle, _receiverAccountNumber);
            _operations.Add(rollback);
            rollback.Execute();
        }


        protected override List<BankOperation> GetOperations()
        {
            return _operations;
        }
    }
}