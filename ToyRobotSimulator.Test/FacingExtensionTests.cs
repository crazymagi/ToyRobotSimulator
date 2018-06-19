using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyRobotSimulator.Helpers;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Test
{
    [TestClass]
    public class FacingExtensionTests
    {
        [TestMethod]
        public void TurnLeftFromNorthShouldBeWest()
        {
            var input = Facing.North;
            var actual = input.TurnLeft();
            var expect = Facing.West;
            Assert.AreEqual(expect,actual);
        }

        [TestMethod]
        public void TurnLeftFromWestShouldBeSouth()
        {
            var input = Facing.West;
            var actual = input.TurnLeft();
            var expect = Facing.South;
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void TurnLeftFromSouthShouldBeEast()
        {
            var input = Facing.South;
            var actual = input.TurnLeft();
            var expect = Facing.East;
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void TurnLeftFromEastShouldBeNorth()
        {
            var input = Facing.East;
            var actual = input.TurnLeft();
            var expect = Facing.North;
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void TurnRightFromNorthShouldBeEast()
        {
            var input = Facing.North;
            var actual = input.TurnRight();
            var expect = Facing.East;
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void TurnRightFromEastShouldBeSouth()
        {
            var input = Facing.East;
            var actual = input.TurnRight();
            var expect = Facing.South;
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void TurnRightFromSouthShouldBeWest()
        {
            var input = Facing.South;
            var actual = input.TurnRight();
            var expect = Facing.West;
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void TurnRightFromWestShouldBeNorth()
        {
            var input = Facing.West;
            var actual = input.TurnRight();
            var expect = Facing.North;
            Assert.AreEqual(expect, actual);
        }
    }
}
