namespace WcfBankingService.Accounts.Number
{
    /// <summary>
    /// Creates account number with proper control sum
    /// </summary>
    public interface IAccountNumberFactory
    {
        /// <summary>
        /// Creates account number object from client's inner account number in given bank
        /// </summary>
        /// <param name="innerNumber">client's inner account number in this bank</param>
        /// <returns>full account number with bank id and calculated control sum</returns>
        AccountNumber GetAccountNumberFromInner(string innerNumber);

        /// <summary>
        /// Creates account number object from full account number (string). Account must belong to this bank
        /// </summary>
        /// <param name="accountNumber">full account number (with control sum and bankId)</param>
        /// <returns></returns>
        AccountNumber GetBankAccountNumber(string accountNumber);

        /// <summary>
        /// Creates account number object from full account number (string). Account dont have to belong to this bank
        /// </summary>
        /// <param name="accountNumber">full account number (with control sum and bankId)</param>
        /// <returns></returns>
        AccountNumber GetAccountNumber(string accountNumber);
    }
}
