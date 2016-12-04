using System.Numerics;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.Accounts
{
    public interface IAccount
    {
        AccountNumber AccountNumber { get; }
        void AddToBalance(BigInteger amount);
    }
}