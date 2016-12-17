namespace WcfBankingService.Accounts.Number.ControlSum
{
    public interface IControlSumCalculator
    {
        string Calculate(string bankId, string innerAccountNumber);
    }
}
