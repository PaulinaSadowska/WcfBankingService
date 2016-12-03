namespace WcfBankingService.account.number
{
    public interface IControlSumCalculator
    {
        string Calculate(string bankId, string number);
    }
}
