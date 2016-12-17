using System;
using WcfBankingService.Accounts.Number.ControlSum;

namespace WcfBankingService.Accounts.Number
{
    public class AccountNumberFactory : IAccountNumberFactory
    {
        private readonly string _bankId;
        private IControlSumCalculator _controlSumCalculator;

        public AccountNumberFactory(string bankId, IControlSumCalculator controlSumCalculator)
        {
            _bankId = bankId;
            _controlSumCalculator = controlSumCalculator;
        }

        public AccountNumber CreateAccountNumber(string number)
        {
            //create account number from number
            throw new NotImplementedException();
        }

        public AccountNumber GetAccountNumber(string accountNumber)
        {
            //create account number object from account Number (check if is valid!)
            throw new NotImplementedException();
        }

        public bool IsAccountNumberValid(string accountNumber)
        {
            throw new NotImplementedException();
        }
    }
}