namespace WcfBankingService
{
    public class DecimalParser
    {
        public static decimal Parse(string value)
        {
            return decimal.Parse(value.Replace(',', '.'));
        }
    }
}