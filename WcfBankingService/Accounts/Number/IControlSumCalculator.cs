namespace WcfBankingService.Accounts.Number
{
    public interface IControlSumCalculator
    {
        string Calculate(string bankId, string number);
    }
}
