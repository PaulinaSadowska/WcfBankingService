using System;

namespace WcfBankingService.Accounts.Number
{
    public interface IAccountNumberFactory
    {
        AccountNumber CreateAccountNumber(String number);
        AccountNumber GetAccountNumber(String accountNumber);
    }
}
