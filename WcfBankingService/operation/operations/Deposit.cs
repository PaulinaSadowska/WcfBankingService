using WcfBankingService.Accounts;

namespace WcfBankingService.Operation.Operations
{
    public class Deposit : BankOperation
    {
        private readonly IPublicAccount _targetAccount;
        private readonly decimal _amount;

        public Deposit(IPublicAccount targetAccount, decimal amount, string operationTitle) : base(operationTitle, amount, "Deposit")
        {
            _targetAccount = targetAccount;
            _amount = amount;
        }

        public override void Execute()
        {
            if(Executed)
                return;

            _targetAccount.AddToBalance(_amount);
            Executed = true;

            RecordBalanceAfterOperation(_targetAccount.GetBalanceValue());
            _targetAccount.AddOperationToHistory(OperationRecord);
        }
    }
}