using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NightLight.Tests
{
    [TestClass]
    public class ZoneStateTests
    {
        [TestMethod]
        public void GetCurrentTime_Tests()
        {
            //arrange
            var state = new ZoneState()
            {
                DayOfWeek = DayOfWeek.Monday,
                Time = new TimeSpan(5, 0, 0)
            };
            var now = Convert.ToDateTime("1/27/2015 4:00PM");

            //act
            var currentTimeBefore =state.GetCurrentTime(ZoneDirection.Before,now);
            var currentTimeAfter =state.GetCurrentTime(ZoneDirection.After,now);

            //assert
            Assert.AreEqual(Convert.ToDateTime("1/26/2015 5:00"), currentTimeBefore);
            Assert.AreEqual(Convert.ToDateTime("2/2/2015 5:00"), currentTimeAfter);
        }
    }
}
