namespace WcfBankingService.Accounts.Number
{
    /// <summary>
    /// stores account number informations, simplify comparing account numbers
    /// </summary>
    public class AccountNumber
    {
        /// <summary>
        /// Bank identifier
        /// </summary>
        public readonly string BankId;

        /// <summary>
        /// Client's account number (unique within this bank)
        /// </summary>
        public readonly string InnerNumber;

        /// <summary>
        /// control sum
        /// </summary>
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