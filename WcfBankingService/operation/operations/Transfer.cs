using WcfBankingService.Accounts;
using WcfBankingService.Operation.Operations;
using WcfBankingService.SoapService.DataContract.Response;

namespace WcfBankingService.operation.operations
{

    public class Transfer : BankOperation, IBankCommand
    {
        private readonly IPublicAccount _targetAccount;
        private readonly decimal _amount;

        public Transfer(IPublicAccount account, decimal amount, string title, string senderAccountNumber)
            : base(title, amount, $"Transfer from: {senderAccountNumber}")
        {
            this._targetAccount = account;
            this._amount = amount;
        }

        public override void Execute()
        {
            if (Executed)
                return;

            _targetAccount.AddToBalance(_amount);
            Executed = true;

            RecordBalanceAfterOperation(_targetAccount.GetBalanceValue());
            _targetAccount.AddOperationToHistory(OperationRecord);
        }
    }
}