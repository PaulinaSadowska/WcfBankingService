using System;
using WcfBankingService.Accounts.Number.ControlSum;

namespace WcfBankingService.Accounts.Number
{
    public class AccountNumberFactory : IAccountNumberFactory
    {
        private readonly string _bankId;
        private readonly IControlSumCalculator _controlSumCalculator;

        public AccountNumberFactory(string bankId, IControlSumCalculator controlSumCalculator)
        {
            _bankId = bankId;
            _controlSumCalculator = controlSumCalculator;
        }

        public AccountNumber CreateAccountNumber(string innerNumber)
        {
            if (innerNumber == null)
                return null;
            var checksum = _controlSumCalculator.Calculate(_bankId, innerNumber);
            return new AccountNumber(_bankId, innerNumber, checksum);
        }

        public AccountNumber GetAccountNumber(string accountNumber)
        {
            if (accountNumber == null || !_controlSumCalculator.IsValid(accountNumber))
                return null;
            var checksum = accountNumber.Substring(0, 2);
            var bankId = accountNumber.Substring(2, 8);
            var innerNumber = accountNumber.Substring(10);
            return (_controlSumCalculator.IsValid(accountNumber) && bankId == _bankId) ?
                new AccountNumber(_bankId, innerNumber, checksum) : null;
        }
    }
}