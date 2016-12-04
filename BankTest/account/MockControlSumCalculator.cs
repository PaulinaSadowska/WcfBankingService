using WcfBankingService.Accounts.Number;

namespace BankTest.Account
{
    class MockControlSumCalculator : IControlSumCalculator
    {
        public string Calculate(string bankId, string number)
        {
            return "00";
        }
    }
}
