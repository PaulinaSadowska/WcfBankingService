using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Web;
using WcfBankingService.account.balance;

namespace WcfBankingService.operation.operations
{
    public class Deposit : IOperation
    {
        private readonly IBalance _balance;
        private readonly BigInteger _amount;
        public Deposit(IBalance balance, BigInteger amount)
        {
            _balance = balance;
            _amount = amount;
        }

        public IBalance Execute()
        {
            return _balance.AddToBalance(_amount);
        }

        public string GetInfo()
        {
            return $"Deposit:\n balance after operation: {_balance.GetValue()}\n amount: {_amount}\n";
        }
    }
}