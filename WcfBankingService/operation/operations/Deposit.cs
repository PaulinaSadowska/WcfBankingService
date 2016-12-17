using System.Numerics;
using WcfBankingService.Accounts;

namespace WcfBankingService.Operation.Operations
{
    public class Deposit : BankOperation, IBankCommand
    {
        private readonly IAccount _targetAccount;
        private readonly BigInteger _amount;
        public Deposit(IAccount targetAccount, BigInteger amount, string operationTitle) : base(operationTitle, amount, "Deposit")
        {
            _targetAccount = targetAccount;
            _amount = amount;
        }

        public void Execute()
        {
            if(Executed)
                return;

            _targetAccount.AddToBalance(_amount);
            //TODO
            //SetBalanceAfterOperation(_targetAccount.getBalanceValue());

            Executed = true;
        }
    }
}