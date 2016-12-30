using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService.Database.SavingData.Helper;

namespace BankTest.Database
{
    [TestClass]
    public class IndexesFinderTest
    {
        private const string ExistingLogin = "Purun";
        private const int ExistingLoginId = 2;


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

    }
}
