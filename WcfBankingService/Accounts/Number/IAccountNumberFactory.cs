using System;

namespace WcfBankingService.Accounts.Number
{
    public interface IAccountNumberFactory
    {
        AccountNumber CreateAccountNumber(string innerNumber);
        AccountNumber GetAccountNumber(string accountNumber);
    }
}
