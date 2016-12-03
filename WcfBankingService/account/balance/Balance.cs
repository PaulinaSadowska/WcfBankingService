using System;
using System.Numerics;

namespace WcfBankingService.account.balance
{
    public class Balance : IBalance
    {
        private BigInteger _balanceValue;

        public Balance(BigInteger balanceValue)
        {
            _balanceValue = balanceValue;
        }

        public IBalance AddToBalance(BigInteger amount)
        {
            throw new NotImplementedException();
        }

        public IBalance SubstractFromBalance(BigInteger amount)
        {
            throw new NotImplementedException();
        }

        public BigInteger GetValue()
        {
            return _balanceValue;
        }
    }
}
