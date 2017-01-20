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

        /// <summary>
        /// constructor of account number
        /// </summary>
        /// <param name="bankId">bank id (8 digits)</param>
        /// <param name="innerNumber">inner account number (16 digits)</param>
        /// <param name="controlSum">control sum (2 digits)</param>
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