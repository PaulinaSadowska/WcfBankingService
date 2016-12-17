using WcfBankingService.Accounts.Number.ControlSum;

namespace BankTest.Account
{
    class MockControlSumCalculator : IControlSumCalculator
    {
        public string Calculate(string bankId, string innerAccountNumber)
        {
            return "00";
        }

        public bool IsValid(string accountNumber)
        {
            throw new System.NotImplementedException();
        }
    }
}
