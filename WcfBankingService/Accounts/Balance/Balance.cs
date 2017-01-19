namespace WcfBankingService.Accounts.Balance
{
    public class Balance : IBalance
    {
        private decimal _balanceValue;

        public Balance(decimal balanceValue)
        {
            _balanceValue = balanceValue;
        }

        public void AddToBalance(decimal amount)
        {
            _balanceValue += amount;
        }

        public void SubstractFromBalance(decimal amount)
        {
            _balanceValue -= amount;
        }

        public decimal GetValue()
        {
            return _balanceValue;
        }
    }
}
