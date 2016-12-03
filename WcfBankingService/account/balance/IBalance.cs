using System.Numerics;

namespace WcfBankingService.account.balance
{
    public interface IBalance
    {
        IBalance AddToBalance(BigInteger amount);
        IBalance SubstractFromBalance(BigInteger amount);
        BigInteger GetValue();
    }
}
