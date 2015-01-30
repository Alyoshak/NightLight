using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NightLight.Tests
{
    [TestClass]
    public class ZoneStateRunnerTests
    {
        [TestMethod]
        public void GenerateTransitionState_Tests()
        {
            //arrange
            var controller = new ZoneController(1,"192.168.0.7");
            var runner = new ZoneStateRunner(controller,new List<ZoneState>());
            var now =Convert.ToDateTime("1/29/2015 12:00");
            var startState= new ZoneState(){
                Brightness=100,
                Color=0,
                DayOfWeek=DayOfWeek.Thursday,
                Time=new TimeSpan(0)
            };
            var endState= new ZoneState(){
                Brightness=0,
                Color=100,
                DayOfWeek=DayOfWeek.Friday,
                Time=new TimeSpan(0)
            };


            //act
            var transitionState = runner.GenerateTransitionState(startState, endState, now);

            //assert
            Assert.AreEqual(transitionState.Color, 50);
            Assert.AreEqual(transitionState.Brightness, 50);
        }
    }
}
