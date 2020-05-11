using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTest
{
    [TestClass]
    public class DateTest
    {
        [TestMethod]
        [DataRow("01.05.1999")]
        [DataRow("14/09/2020")]
        [DataRow("06 . 08 . 2011")]
        [DataRow("25,01,1972")]
        public void Create_IsSuccess(string input)
        {
            var result = Date.Create(input);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
