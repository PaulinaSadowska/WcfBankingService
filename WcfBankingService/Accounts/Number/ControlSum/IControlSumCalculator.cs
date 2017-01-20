namespace WcfBankingService.Accounts.Number.ControlSum
{
    /// <summary>
    /// Interface of control sum calculator used to calculate and validate control sum
    /// </summary>
    public interface IControlSumCalculator
    {
        /// <summary>
        /// Calculates control sum for account number
        /// </summary>
        /// <param name="bankId">identifier of the bank</param>
        /// <param name="innerAccountNumber">client's inner account number in this bank</param>
        /// <returns></returns>
        string Calculate(string bankId, string innerAccountNumber);

        /// <summary>
        /// Check if given account number corresponds with given format and has valid control sum
        /// </summary>
        /// <param name="accountNumber">account number (with control sum and bankId)</param>
        /// <returns>whether account number format is correct</returns>
        bool IsValid(string accountNumber);
    }
}
