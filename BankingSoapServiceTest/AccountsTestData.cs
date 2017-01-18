namespace BankingSoapServiceTest
{
    public class AccountsTestData
    {
        public readonly string ValidSenderAccountNumber = "46001121691234567890987654";
        public readonly string ValidReceiverAccountNumber = "93001121691234567898765432";

        public readonly string NotExistingAccountNumber = "19001121691234567890987655";
        public readonly string InvalidAccountNumber = "12001121691234567891234567";

        public readonly string ListedBankAccountNumber = "89001122411234567890987654";
        public readonly string NotListedBankAccountNumber = "83001122491234567890987654";

        public readonly string ValidAccessToken = "876123456433";
    }
}
