using System;

namespace WcfBankingService.Accounts.Number
{
    public interface IAccountNumberFactory
    {
        bool IsAccountNumberValid(String accountNumber);
        AccountNumber CreateAccountNumber(String number);
        AccountNumber GetAccountNumber(String accountNumber);
    }
}
