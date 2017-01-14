using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfBankingService;

namespace BankTest
{
    [TestClass]
    public class DecimalParserTest
    {

        [TestMethod]
        public void ValidDecimal_returnsDecimal()
        {
            Assert.AreEqual(100.2m, DecimalParser.Parse("100.2"));
        }

        [TestMethod]
        public void ValidDecimalWithComma_returnsDecimal()
        {
            Assert.AreEqual(100.2m, DecimalParser.Parse("100,2"));
        }

        [TestMethod]
        public void ValidIntValue_returnsDecimal()
        {
            Assert.AreEqual(100m, DecimalParser.Parse("100"));
        }
    }
}
