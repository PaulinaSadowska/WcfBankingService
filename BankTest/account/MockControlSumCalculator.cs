using WcfBankingService.Accounts.Number.ControlSum;

namespace BankTest.Account
{
    internal class MockControlSumCalculator : IControlSumCalculator
    {
        public string Calculate(string bankId, string innerAccountNumber)
        {
            return "00";
        }

        public bool IsValid(string accountNumber)
        {
            return accountNumber.Length == 26;
        }
    }
}
