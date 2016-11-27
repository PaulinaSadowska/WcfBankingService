using System;
using WcfBankingService.account;

namespace BankTest.account
{
    class MockControlSumCalculator : IControlSumCalculator
    {
        public string calculate(string bankId, string number)
        {
            return "00";
        }
    }
}
