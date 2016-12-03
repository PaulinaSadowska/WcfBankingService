using System;
using WcfBankingService.account;
using WcfBankingService.account.number;

namespace BankTest.account
{
    class MockControlSumCalculator : IControlSumCalculator
    {
        public string Calculate(string bankId, string number)
        {
            return "00";
        }
    }
}
