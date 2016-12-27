using System;

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
            _balanceValue += amount;
            return this;
        }

        public IBalance SubstractFromBalance(decimal amount)
        {
            _balanceValue -= amount;
            return this;
        }

        public decimal GetValue()
        {
            return _balanceValue;
        }
    }
}
