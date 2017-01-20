using WcfBankingService.Accounts.Number.ControlSum;

namespace WcfBankingService.Accounts.Number
{
    /// <summary>
    /// <see cref="IAccountNumberFactory"/>
    /// </summary>
    public class AccountNumberFactory : IAccountNumberFactory
    {
        private readonly string _bankId;
        private readonly IControlSumCalculator _controlSumCalculator;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="bankId">bank identifier</param>
        /// <param name="controlSumCalculator">control sum calculator which calculates control sum in given format</param>
        public AccountNumberFactory(string bankId, IControlSumCalculator controlSumCalculator)
        {
            _bankId = bankId;
            _controlSumCalculator = controlSumCalculator;
        }

        /// <summary>
        /// <see cref="IAccountNumberFactory.GetAccountNumberFromInner"/>
        /// </summary>
        /// <param name="innerNumber">clients inner account number (unique within bank)</param>
        /// <returns>account number object</returns>
        public AccountNumber GetAccountNumberFromInner(string innerNumber)
        {
            if (innerNumber == null)
                return null;
            var checksum = _controlSumCalculator.Calculate(_bankId, innerNumber);
            return new AccountNumber(_bankId, innerNumber, checksum);
        }

        /// <summary>
        /// <see cref="IAccountNumberFactory.GetBankAccountNumber"/>
        /// </summary>
        /// <param name="accountNumber">full account number</param>
        /// <returns>account number object</returns>
        public AccountNumber GetBankAccountNumber(string accountNumber)
        {
            var number = GetAccountNumber(accountNumber);
            return (number != null && number.BankId == _bankId) ? number : null;
        }

        /// <summary>
        /// <see cref="IAccountNumberFactory.GetAccountNumber"/>
        /// </summary>
        /// <param name="accountNumber">sull account number</param>
        /// <returns>account number object</returns>
        public AccountNumber GetAccountNumber(string accountNumber)
        {
            if (accountNumber == null || !_controlSumCalculator.IsValid(accountNumber))
                return null;
            var checksum = accountNumber.Substring(0, 2);
            var bankId = accountNumber.Substring(2, 8);
            var innerNumber = accountNumber.Substring(10);
            return (_controlSumCalculator.IsValid(accountNumber)) ?
                new AccountNumber(bankId, innerNumber, checksum) : null;
        }
    }
}