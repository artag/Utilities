using System;
using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTest
{
    [TestClass]
    public class TimeSlotTest
    {
        [TestMethod]
        [DataRow("0:0")]
        [DataRow("10:10")]
        [DataRow("9:30 - 12:00")]
        [DataRow("12 : 30 - 15 : 00")]
        [DataRow("16:00 - 17:00")]
        [DataRow("17:30-18:30")]
        public void Create_IsSuccess(string input)
        {
            var result = TimeSlot.Create(input);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        [DataRow("00 :00")]
        [DataRow("12:30")]
        [DataRow("18: 15")]
        public void Create_From_OneTimeValue_StartTime_IsEqual_EndTime(string input)
        {
            var result = TimeSlot.Create(input);

            if (result.IsFailure)
                Assert.Fail(result.Error);

            var timeSlot = result.Value;
            Assert.AreEqual(timeSlot.StartTime, timeSlot.EndTime);
        }

        [TestMethod]
        [DataRow("18:00 - 18:30")]
        [DataRow("09 : 00-13 : 00")]
        public void Create_From_TwoTimeValues_StartTime_IsNotEqual_EndTime(string input)
        {
            var result = TimeSlot.Create(input);

            if (result.IsFailure)
                Assert.Fail(result.Error);

            var timeSlot = result.Value;
            Assert.AreNotEqual(timeSlot.StartTime, timeSlot.EndTime);
        }

        [TestMethod]
        [DataRow("00:00")]
        [DataRow("09 : 30")]
        [DataRow("18:00 - 18:00")]
        public void Create_StartTime_IsEqual_EndTime_Duration_Is_Zero(string input)
        {
            var result = TimeSlot.Create(input);

            if (result.IsFailure)
                Assert.Fail(result.Error);

            var timeSlot = result.Value;
            Assert.AreEqual(TimeSpan.Zero, timeSlot.Duration);
        }
    }
}
