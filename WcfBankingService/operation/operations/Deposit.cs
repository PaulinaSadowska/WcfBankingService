using WcfBankingService.Accounts;

namespace WcfBankingService.Operation.Operations
{
    public class Deposit : BankOperation, IBankCommand
    {
        private readonly IAccount _targetAccount;
        private readonly decimal _amount;

        public Deposit(IAccount targetAccount, decimal amount, string operationTitle) : base(operationTitle, amount, "Deposit")
        {
            _targetAccount = targetAccount;
            _amount = amount;
        }

        public void Execute()
        {
            if(Executed)
                return;

            _targetAccount.AddToBalance(_amount);
            SetBalanceAfterOperation(_targetAccount.GetBalanceValue());

            Executed = true;
        }
    }
}