using System.Numerics;

namespace WcfBankingService.Accounts.Balance
{
    public interface IBalance
    {
        IBalance AddToBalance(BigInteger amount);
        IBalance SubstractFromBalance(BigInteger amount);
        BigInteger GetValue();
    }
}
