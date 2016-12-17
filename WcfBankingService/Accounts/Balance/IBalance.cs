using System;

namespace WcfBankingService.Accounts.Balance
{
    public interface IBalance
    {
        IBalance AddToBalance(decimal amount);
        IBalance SubstractFromBalance(decimal amount);
        decimal GetValue();
    }
}
