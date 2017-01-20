namespace WcfBankingService.Accounts.Balance
{
    /// <summary>
    /// Balance of the account
    /// </summary>
    public class Balance : IBalance
    {
        private decimal _balanceValue;

        public Balance(decimal balanceValue)
        {
            _balanceValue = balanceValue;
        }

        /// <summary>
        /// <see cref="IBalance.AddToBalance"/>
        /// </summary>
        /// <param name="amount">amount to add</param>
        public void AddToBalance(decimal amount)
        {
            _balanceValue += amount;
        }

        /// <summary>
        /// <see cref="IBalance.SubstractFromBalance"/>
        /// </summary>
        /// <param name="amount">amount to substract</param>
        public void SubstractFromBalance(decimal amount)
        {
            _balanceValue -= amount;
        }

        /// <summary>
        /// <see cref="IBalance.GetValue"/>
        /// </summary>
        /// <returns>balance decimal value</returns>
        public decimal GetValue()
        {
            return _balanceValue;
        }
    }
}
