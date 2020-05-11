using System;
using System.Linq;
using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTest
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        [DataRow("")]
        [DataRow("00 : 00")]
        [DataRow("0:0")]
        [DataRow("0:0:0")]
        public void ParseTimeSpan(string value)
        {
            var result = Parser.ParseTimeSpan(value);

            Assert.IsTrue(result.IsSuccess, $"Error on input {value}.");
            Assert.AreEqual(result.Value.First(), TimeSpan.Zero);
        }
    }
}
