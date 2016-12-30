using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Database.SavingData.Helper;

namespace BankingSoapServiceTest.Database

{
    [TestClass]
    public class IndexesFinderTest
    {
        private const string ExistingLogin = "Purun";
        private const int ExistingLoginId = 2;

        private const string ExistingInnerAccountNumber = "1234567890987654";
        private const int ExistingAccountId = 1;


        private readonly IndexesFinder _indexesFinder;

        public IndexesFinderTest()
        {
            _indexesFinder = new IndexesFinder();
        }

        [TestMethod]
        public void getUserId_existingLogin_returnsId()
        {
            var id = _indexesFinder.GetUserId(ExistingLogin);
            Assert.AreEqual(ExistingLoginId, id);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void getUserId_notExistingLogin_throwsBankException()
        {
            _indexesFinder.GetUserId("Some non existing login nie wow");
        }

        [TestMethod]
        public void getAccountId_existingAccount_returnsId()
        {
            var id = _indexesFinder.GetAccountId(ExistingInnerAccountNumber);
            Assert.AreEqual(ExistingAccountId, id);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void getAccountId_notExistingAccount_returnsId()
        {
            _indexesFinder.GetAccountId("6666666666666666");
        }
    }
}