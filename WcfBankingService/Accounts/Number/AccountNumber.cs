namespace WcfBankingService.Accounts.Number
{
    public class AccountNumber
    {
        public readonly string BankId;
        public readonly string Number;
        public readonly string ControlSum;

        public AccountNumber(string bankId, string number, string controlSum)
        {
            BankId = bankId;
            Number = number;
            ControlSum = controlSum;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return $"{ControlSum}{BankId}{Number}";
        }
    }
}