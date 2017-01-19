using WcfBankingService.Accounts.Number;

namespace BankTest.account
{
    public class MockAccountNumberFactory : IAccountNumberFactory
    {
        public AccountNumber GetAccountNumberFromInner(string innerNumber)
        {
            return new AccountNumber("", "", "");
        }

        public AccountNumber GetBankAccountNumber(string accountNumber)
        {
            return new AccountNumber("", "", "");
        }
    }
}