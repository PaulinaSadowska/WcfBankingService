namespace WcfBankingService.Accounts.Number
{
    public interface IAccountNumberFactory
    {
        AccountNumber GetAccountNumberFromInner(string innerNumber);
        AccountNumber GetBankAccountNumber(string accountNumber);
    }
}
