using WcfBankingService.Accounts;
using WcfBankingService.Operation.Operations;
using WcfBankingService.Service.DataContract.Response;

namespace WcfBankingService.operation.operations
{
    /// <summary>
    /// withdrawal operation
    /// </summary>
    public class Withdraw : BankOperation
    {
        private readonly IAccount _targetAccount;
        private readonly decimal _amount;

        public Withdraw(IAccount targetAccount, decimal amount, string operationTitle)
            : this(targetAccount, amount, operationTitle, "Withdraw")
        {
        }


        public Withdraw(IAccount targetAccount, decimal amount, string operationTitle, string source)
            : base(targetAccount.AccountNumber, new OperationRecord
            {
                Title = operationTitle,
                Debet = amount,
                Source = source
            })
        {
            _targetAccount = targetAccount;
            _amount = amount;
        }

        /// <summary>
        /// execute withdraw operation. 
        /// Throws exception when amount is higher than balance value
        /// records balance after operation into operation record
        /// </summary>
        public override void Execute()
        {
            if (Executed)
                return;

            if (_targetAccount.GetBalanceValue() < _amount)
                throw new BankException(ResponseStatus.InsufficientFunds);

            _targetAccount.SubstractFromBalance(_amount);
            Executed = true;

            RecordBalanceAfterOperation(_targetAccount.GetBalanceValue());
            _targetAccount.AddOperationToHistory(OperationRecord);
        }
    }
}