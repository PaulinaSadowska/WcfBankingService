using System;

namespace WcfBankingService.Accounts.Number
{
    public interface IAccountNumberFactory
    {
        AccountNumber CreateAccountNumber(String innerNumber);
        AccountNumber GetAccountNumber(String accountNumber);
    }
}
