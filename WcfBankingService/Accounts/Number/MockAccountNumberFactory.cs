namespace WcfBankingService.Accounts.Number
{
    public class MockAccountNumberFactory : IAccountNumberFactory
    {
        public AccountNumber CreateAccountNumber(string innerNumber)
        {
            return new AccountNumber("", "", "");
        }

        public AccountNumber GetAccountNumber(string accountNumber)
        {
            return new AccountNumber("", "", "");
        }
    }
}