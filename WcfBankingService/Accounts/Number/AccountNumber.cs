using System;

namespace WcfBankingService.Accounts.Number
{
    public class AccountNumber
    {
        public readonly string BankId;
        public readonly string InnerNumber;
        public readonly string ControlSum;

        public AccountNumber(string bankId, string innerNumber, string controlSum)
        {
            BankId = bankId;
            InnerNumber = innerNumber;
            ControlSum = controlSum;
        }

        public override bool Equals(object obj)
        {
            var number = obj as AccountNumber;
            return number != null && number.ToString().Equals(ToString());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return $"{ControlSum}{BankId}{InnerNumber}";
        }
    }
}