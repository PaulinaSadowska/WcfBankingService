using System;
using WcfBankingService.Accounts.Number;

namespace WcfBankingService.Accounts
{
    public interface IAccount
    {
        AccountNumber AccountNumber { get; }
        void AddToBalance(Decimal amount);
    }
}