using System;

namespace WcfBankingService.Accounts.Number
{
    public interface IAccountNumberFactory
    {
        AccountNumber CreateAccountNumber(string innerNumber);
        AccountNumber GetBankAccountNumber(string accountNumber);
    }
}
