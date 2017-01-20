using System.Collections.Generic;
using WcfBankingService.Accounts;
using WcfBankingService.Accounts.Number;
using WcfBankingService.operation.Complex;
using WcfBankingService.operation.operations;
using WcfBankingService.Operation.Operations;
using WcfBankingService.Service.DataContract.Response;

namespace WcfBankingService.Operation.Complex
{
    /// <summary>
    /// transfer between account from this and other bank
    /// </summary>
    public class InterBankTransfer : ComplexCommand
    {
        private const decimal TransferFeeValue = 0.1m;

        private readonly List<BankOperation> _operations;

        private readonly IAccount _sender;
        private readonly AccountNumber _receiverAccountNumber;
        private readonly decimal _amount;
        private readonly string _operationTitle;

        public InterBankTransfer(IAccount sender, AccountNumber receiverAccountNumber,
            decimal amount, string operationTitle)
        {
            _operations = new List<BankOperation>
            {
                new OutgoingTransfer(sender, amount, operationTitle, receiverAccountNumber.ToString()),
                new TransferFee(sender, TransferFeeValue, receiverAccountNumber.ToString()),
                new RestTransfer(sender.AccountNumber, amount, operationTitle, receiverAccountNumber)
            };
            _sender = sender;
            _receiverAccountNumber = receiverAccountNumber;
            _operationTitle = operationTitle;
            _amount = amount;
            ResponseStatus = ResponseStatus.Success;
        }

        public InterBankTransfer(IAccount sender, AccountNumber receiverAccountNumber,
            int amountInCents, string operationTitle)
            : this(sender, receiverAccountNumber, amountInCents/100m, operationTitle)
        {
        }

        protected override void Rollback()
        {
            if (!_operations[0].Executed) return;

            var rollback = new RollbackTransfer(_sender, _amount, _operationTitle, _receiverAccountNumber);
            _operations.Add(rollback);
            rollback.Execute();

            if (!_operations[1].Executed) return;

            var feeRollback = new RollbackTransferFee(_sender, TransferFeeValue, _receiverAccountNumber.ToString());
            _operations.Add(feeRollback);
            feeRollback.Execute();
        }


        protected override List<BankOperation> GetOperations()
        {
            return _operations;
        }
    }
}