using System;
using System.Numerics;

namespace WcfBankingService.Accounts.Balance
{
    public class Balance : IBalance
    {
        private decimal _balanceValue;

        public Balance(decimal balanceValue)
        {
            _balanceValue = balanceValue;
        }

        public IBalance AddToBalance(decimal amount)
        {
            throw new NotImplementedException();
        }

        public IBalance SubstractFromBalance(decimal amount)
        {
            throw new NotImplementedException();
        }

        public decimal GetValue()
        {
            return _balanceValue;
        }
    }
}
