using System.Numerics;
using WcfBankingService.Accounts;
using WcfBankingService.Operation.Operations;

namespace WcfBankingService.operation.operations
{
    public class Withdraw : BankOperation, IBankCommand
    {
        private readonly IAccount _targetAccount;
        private readonly decimal _amount;

        public Withdraw(IAccount targetAccount, decimal amount, string operationTitle) : base(operationTitle, amount, "Deposit")
        {
            _targetAccount = targetAccount;
            _amount = amount;
        }

        public void Execute()
        {
            if (Executed)
                return;

            _targetAccount.SubstractFromBalance(_amount);
            SetBalanceAfterOperation(_targetAccount.GetBalanceValue());

            Executed = true;
        }
    }
}