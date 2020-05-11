using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math = Core.Operations.Math;

namespace CoreTest
{
    [TestClass]
    public class MathTest
    {
        [TestMethod]
        [DataRow(1, 8, 0)]
        [DataRow(1, 9, 0)]
        [DataRow(1, 10, 0)]
        [DataRow(1, 11, 0)]
        [DataRow(1, 12, 0)]
        [DataRow(1, 13, 0)]
        [DataRow(1, 14, 0)]
        [DataRow(1, 15, 0)]
        [DataRow(1, 16, 0)]
        [DataRow(1, 17, 0)]
        [DataRow(1, 18, 0)]
        [DataRow(1, 19, 0)]
        [DataRow(1, 20, 0)]
        [DataRow(1, 21, 0)]
        [DataRow(1, 22, 0)]
        [DataRow(1, 22, 59)]
        public void RoundTimeSpanToNearestMinutes_Round_To_15(int hours, int minutes, int seconds)
        {
            var time = new TimeSpan(hours, minutes, seconds);
            var roundedTime = Math.RoundTimeSpanToNearestMinutes(time, 15);

            Assert.AreEqual(
                new TimeSpan(1, 15, 0),
                roundedTime,
                $"Error on {time.Hours}hours {time.Minutes}minutes {time.Seconds}seconds");
        }

        [TestMethod]
        [DataRow(1, 23, 0)]
        [DataRow(1, 24, 0)]
        [DataRow(1, 25, 0)]
        [DataRow(1, 26, 0)]
        [DataRow(1, 27, 0)]
        [DataRow(1, 28, 0)]
        [DataRow(1, 29, 0)]
        [DataRow(1, 30, 0)]
        [DataRow(1, 31, 0)]
        [DataRow(1, 32, 0)]
        [DataRow(1, 33, 0)]
        [DataRow(1, 34, 0)]
        [DataRow(1, 35, 0)]
        [DataRow(1, 36, 0)]
        [DataRow(1, 37, 0)]
        [DataRow(1, 37, 59)]
        public void RoundTimeSpanToNearestMinutes_Round_To_30(int hours, int minutes, int seconds)
        {
            var time = new TimeSpan(hours, minutes, seconds);
            var roundedTime = Math.RoundTimeSpanToNearestMinutes(time, 15);

            Assert.AreEqual(
                new TimeSpan(1, 30, 0),
                roundedTime,
                $"Error on {time.Hours}hours {time.Minutes}minutes {time.Seconds}seconds");
        }

        [TestMethod]
        [DataRow(1, 38, 0)]
        [DataRow(1, 39, 0)]
        [DataRow(1, 40, 0)]
        [DataRow(1, 41, 0)]
        [DataRow(1, 42, 0)]
        [DataRow(1, 43, 0)]
        [DataRow(1, 44, 0)]
        [DataRow(1, 45, 0)]
        [DataRow(1, 46, 0)]
        [DataRow(1, 47, 0)]
        [DataRow(1, 48, 0)]
        [DataRow(1, 49, 0)]
        [DataRow(1, 50, 0)]
        [DataRow(1, 51, 0)]
        [DataRow(1, 52, 0)]
        [DataRow(1, 52, 59)]
        public void RoundTimeSpanToNearestMinutes_Round_To_45(int hours, int minutes, int seconds)
        {
            var time = new TimeSpan(hours, minutes, seconds);
            var roundedTime = Math.RoundTimeSpanToNearestMinutes(time, 15);

            Assert.AreEqual(
                new TimeSpan(1, 45, 0),
                roundedTime,
                $"Error on {time.Hours}hours {time.Minutes}minutes {time.Seconds}seconds");
        }

        [TestMethod]
        [DataRow(1, 53, 0)]
        [DataRow(1, 54, 0)]
        [DataRow(1, 55, 0)]
        [DataRow(1, 56, 0)]
        [DataRow(1, 57, 0)]
        [DataRow(1, 58, 0)]
        [DataRow(1, 59, 0)]
        [DataRow(2, 0, 0)]
        [DataRow(2, 1, 0)]
        [DataRow(2, 2, 0)]
        [DataRow(2, 3, 0)]
        [DataRow(2, 4, 0)]
        [DataRow(2, 5, 0)]
        [DataRow(2, 6, 0)]
        [DataRow(2, 7, 0)]
        [DataRow(2, 7, 59)]
        public void RoundTimeSpanToNearestMinutes_Round_To_0(int hours, int minutes, int seconds)
        {
            var time = new TimeSpan(hours, minutes, seconds);
            var roundedTime = Math.RoundTimeSpanToNearestMinutes(time, 15);

            Assert.AreEqual(
                new TimeSpan(2, 0, 0),
                roundedTime,
                $"Error on {time.Hours}hours {time.Minutes}minutes {time.Seconds}seconds");
        }

        [TestMethod]
        [DataRow(1, 23, 53, 0)]
        [DataRow(1, 23, 54, 0)]
        [DataRow(1, 23, 55, 0)]
        [DataRow(1, 23, 56, 0)]
        [DataRow(1, 23, 57, 0)]
        [DataRow(1, 23, 58, 0)]
        [DataRow(1, 23, 59, 0)]
        [DataRow(2, 0, 0, 0)]
        [DataRow(2, 0, 1, 0)]
        [DataRow(2, 0, 2, 0)]
        [DataRow(2, 0, 3, 0)]
        [DataRow(2, 0, 4, 0)]
        [DataRow(2, 0, 5, 0)]
        [DataRow(2, 0, 6, 0)]
        [DataRow(2, 0, 7, 0)]
        [DataRow(2, 0, 7, 59)]
        public void RoundTimeSpanToNearestMinutes_Round_To_0_CheckDays(int days, int hours, int minutes, int seconds)
        {
            var time = new TimeSpan(days, hours, minutes, seconds);
            var roundedTime = Math.RoundTimeSpanToNearestMinutes(time, 15);

            Assert.AreEqual(
                new TimeSpan(2, 0, 0, 0),
                roundedTime,
                $"Error on {time.Hours}hours {time.Minutes}minutes {time.Seconds}seconds");
        }
    }
}
