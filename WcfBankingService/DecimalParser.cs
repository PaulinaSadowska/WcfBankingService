namespace WcfBankingService
{
    /// <summary>
    /// Parses string to decimal in allowed formats
    /// </summary>
    public class DecimalParser
    {
        /// <summary>
        /// parses string to decimal. Accepts xx.xx and xx,xx format
        /// </summary>
        /// <param name="value">value to parse</param>
        /// <returns>string value parsed to decimal</returns>
        public static decimal Parse(string value)
        {
            return decimal.Parse(value.Replace(',', '.'));
        }
    }
}