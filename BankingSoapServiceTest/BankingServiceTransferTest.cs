using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Database.SavingData;
using WcfBankingService.Service.Soap;

namespace BankingSoapServiceTest

{
    [TestClass]
    public class BankingServiceTransferTest
    {
        private const string ValidSenderAccountNumber = "39112169001234567890987654";
        private const string ValidReceiverAccountNumber = "86112169001234567898765432";

        private const string NotExistingAccountNumber = "04112169001234567891234567";
        private const string InvalidAccountNumber = "12112169001234567891234567";

        private const string ListedBankAccountNumber = "66112241001122334455667788";
        private const string NotListedBankAccountNumber = "38333241001122334455667788";

        private readonly IBankingService _service;

        public BankingServiceTransferTest()
        {
            _service = new BankingService(new MockDataInserter());
        }


        [TestMethod]
        public void Transfer_BothAccountsFromThisBank_ReturnsSuccess()
        {

        }


    }
}