using System;
using System.Numerics;
using WcfBankingService.account.balance;

namespace WcfBankingService.operation.operations
{
    public class Withdraw : IOperation
    {
        private IBalance _balance;
        private BigInteger _amount;
        public Withdraw(IBalance balance, BigInteger amount)
        {
            _balance = balance;
            _amount = amount;
        }

        public IBalance Execute()
        {
            //get money from balance
            //return new balance
            return _balance.SubstractFromBalance(_amount);
        }

        public string GetInfo()
        {
            return $"Withdraw:\n balance after operation: {_balance.GetValue()}\n amount: {_amount}\n";
        }
    }
}