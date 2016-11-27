using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfBankingService.accunt;

namespace WcfBankingService.account
{
    public class AccountNumberFactory : IAccountNumberFactory
    {
        public readonly string BankId;
        private IControlSumCalculator ControlSumCalculator;

        public AccountNumberFactory(string bankId, IControlSumCalculator controlSumCalculator)
        {
            BankId = bankId;
            ControlSumCalculator = controlSumCalculator;
        }

        public AccountNumber createAccountNumber(string number)
        {
            //create account number from number
            throw new NotImplementedException();
        }

        public AccountNumber getAccountNumber(string accountNumber)
        {
            //create account number object from account Number (check if is valid!)
            throw new NotImplementedException();
        }

        public bool isAccountNumberValid(string accountNumber)
        {
            throw new NotImplementedException();
        }
    }
}