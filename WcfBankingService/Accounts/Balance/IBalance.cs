namespace WcfBankingService.Accounts.Balance
{
    public interface IBalance
    {
        /// <summary>
        /// Add amount to balance value
        /// </summary>
        /// <param name="amount">amount to add</param>
        void AddToBalance(decimal amount);

        /// <summary>
        /// Substract amount from balance value
        /// </summary>
        /// <param name="amount">amount to substract</param>
        void SubstractFromBalance(decimal amount);

        /// <summary>
        /// Get balance value
        /// </summary>
        /// <returns>balance value</returns>
        decimal GetValue();
    }
}
