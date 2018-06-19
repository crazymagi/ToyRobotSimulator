using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ToyRobotSimulator.Configuration;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Services;

namespace ToyRobotSimulator.Test
{
    [TestClass]
    public class ToyRobotServiceTests
    {
        #region Test GetPosition Method
        [TestMethod]
        public void GetPositionShouldBeSameWithContext()
        {
            var mockContext = new Mock<IToyRobotContext>();
            var mockSettings = new Mock<ITableTopSettings>();
            var position = new Position(){X=1, Y=1};
            mockContext.Setup(mc => mc.Position).Returns(position);
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.GetPosition();
            Assert.AreEqual(position, actual);
        }
        #endregion

        #region Test GetFacing Method
        [TestMethod]
        public void GetFacingShouldBeSameWithContext()
        {
            var mockContext = new Mock<IToyRobotContext>();
            var mockSettings = new Mock<ITableTopSettings>();
            var facing = Facing.South;
            mockContext.Setup(mc => mc.Facing).Returns(facing);
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.GetFacing();
            Assert.AreEqual(facing, actual);
        }
        #endregion

        #region Test SetPlace Method
        [TestMethod]
        public void SetPlacePositionShouldSaveToContext()
        {
            var context = new ToyRobotContext();
            var mockSettings = new Mock<ITableTopSettings>();
            var service = new ToyRobotService(context, mockSettings.Object);
            var position = new Position() { X = 2, Y = 3 };
            service.SetPlace(position, Facing.North);
            var actual = context.Position;
            Assert.AreEqual(position, actual);
        }

        [TestMethod]
        public void SetPlaceFacingShouldSaveToContext()
        {
            var context = new ToyRobotContext();
            var mockSettings = new Mock<ITableTopSettings>();
            var service = new ToyRobotService(context, mockSettings.Object);
            var position = new Position() { X = 2, Y = 3 };
            var facing = Facing.North;
            service.SetPlace(position, facing);
            var actual = context.Facing;
            Assert.AreEqual(facing, actual);
        }
        #endregion

        #region Test IsOnTable Method
        [TestMethod]
        public void ValidPositionAndFacingIsOnTable()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void NullPositionIsNotOnTable()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Setup(mc => mc.Position).Returns((Position)null);
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void NullFacingIsNotOnTable()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Setup(mc => mc.Facing).Returns((Facing?)null);
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void InvalidPositionIsNotOnTable_SmallerThanMinX()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var position = mockContext.Object.Position;
            position.X = mockSettings.Object.MinX - 1;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void InvalidPositionIsNotOnTable_SmallerThanMinY()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var position = mockContext.Object.Position;
            position.Y = mockSettings.Object.MinY - 1;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void InvalidPositionIsNotOnTable_BiggerThanMaxX()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var position = mockContext.Object.Position;
            position.X = mockSettings.Object.MaxX + 1;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void InvalidPositionIsNotOnTable_BiggerThanMaxY()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var position = mockContext.Object.Position;
            position.Y = mockSettings.Object.MaxY + 1;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ValidPositionIsOnTable_SameAsMinX()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var position = mockContext.Object.Position;
            position.X = mockSettings.Object.MinX;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ValidPositionIsOnTable_SameAsMaxX()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var position = mockContext.Object.Position;
            position.X = mockSettings.Object.MaxX;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void ValidPositionIsOnTable_SameAsMinY()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var position = mockContext.Object.Position;
            position.Y = mockSettings.Object.MinY;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsTrue(actual);
        }


        [TestMethod]
        public void ValidPositionIsOnTable_SameAsMaxY()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var position = mockContext.Object.Position;
            position.Y = mockSettings.Object.MaxY;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsTrue(actual);
        }
        #endregion

        #region Test CanMove Method
        [TestMethod]
        public void NotInTableCannotMove()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Object.Position.X = mockSettings.Object.MinX - 1;  //Make IsOnTable return false
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.CanMove();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CannotMoveWhenFacingNorthAtNorthEdge()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Object.Position.Y = mockSettings.Object.MaxY;
            mockContext.Setup(mc => mc.Facing).Returns(Facing.North);
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.CanMove();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CannotMoveWhenFacingSouthAtSouthEdge()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Object.Position.Y = mockSettings.Object.MinY;
            mockContext.Setup(mc => mc.Facing).Returns(Facing.South);
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.CanMove();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CannotMoveWhenFacingEastAtEastEdge()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Object.Position.X = mockSettings.Object.MaxX;
            mockContext.Setup(mc => mc.Facing).Returns(Facing.East);
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.CanMove();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CannotMoveWhenFacingWestAtWestEdge()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Object.Position.X = mockSettings.Object.MinX;
            mockContext.Setup(mc => mc.Facing).Returns(Facing.West);
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.CanMove();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CanMoveWhenFacingNorthButNotAtNorthEdge()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Object.Position.Y = mockSettings.Object.MaxY - 1;
            mockContext.Setup(mc => mc.Facing).Returns(Facing.North);
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.CanMove();
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CanMoveWhenFacingSouthButNotAtSouthEdge()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Object.Position.Y = mockSettings.Object.MinY + 1;
            mockContext.Setup(mc => mc.Facing).Returns(Facing.South);
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.CanMove();
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CanMoveWhenFacingEastButNotAtEastEdge()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Object.Position.X = mockSettings.Object.MaxX - 1;
            mockContext.Setup(mc => mc.Facing).Returns(Facing.East);
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.CanMove();
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CanMoveWhenFacingWestButNotAtWestEdge()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Object.Position.X = mockSettings.Object.MinX + 1;
            mockContext.Setup(mc => mc.Facing).Returns(Facing.West);
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.CanMove();
            Assert.IsTrue(actual);
        }
        #endregion

        #region Test Move Method
        [TestMethod]
        public void MoveNorthIncreaseOneOfY()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Setup(mc => mc.Facing).Returns(Facing.North);
            var expect = mockContext.Object.Position.Y + 1;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            service.Move();
            var actual = mockContext.Object.Position.Y;
            Assert.AreEqual(expect,actual);
        }

        [TestMethod]
        public void MoveSouthDecreaseOneOfY()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Setup(mc => mc.Facing).Returns(Facing.South);
            var expect = mockContext.Object.Position.Y - 1;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            service.Move();
            var actual = mockContext.Object.Position.Y;
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void MoveEastIncreaseOneOfX()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Setup(mc => mc.Facing).Returns(Facing.East);
            var expect = mockContext.Object.Position.X + 1;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            service.Move();
            var actual = mockContext.Object.Position.X;
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void MoveWestDecreaseOneOfX()
        {
            GetValidMockContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Setup(mc => mc.Facing).Returns(Facing.West);
            var expect = mockContext.Object.Position.X - 1;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            service.Move();
            var actual = mockContext.Object.Position.X;
            Assert.AreEqual(expect, actual);
        }
        #endregion

        #region Test Turn Method
        [TestMethod]
        public void FromNorthTurnLeftShouldBeWest()
        {
            GetValidContextAndSettingWhichOnTable(out var context, out var settings);
            context.Facing = Facing.North;
            var expect = Facing.West;
            var service = new ToyRobotService(context, settings);
            service.Turn(Direction.Left);
            var actual = context.Facing;
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void FromWestTurnLeftShouldBeSouth()
        {
            GetValidContextAndSettingWhichOnTable(out var context, out var settings);
            context.Facing = Facing.West;
            var expect = Facing.South;
            var service = new ToyRobotService(context, settings);
            service.Turn(Direction.Left);
            var actual = context.Facing;
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void FromSouthTurnLeftShouldBeEast()
        {
            GetValidContextAndSettingWhichOnTable(out var context, out var settings);
            context.Facing = Facing.South;
            var expect = Facing.East;
            var service = new ToyRobotService(context, settings);
            service.Turn(Direction.Left);
            var actual = context.Facing;
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void FromEastTurnLeftShouldBeNorth()
        {
            GetValidContextAndSettingWhichOnTable(out var context, out var settings);
            context.Facing = Facing.East;
            var expect = Facing.North;
            var service = new ToyRobotService(context, settings);
            service.Turn(Direction.Left);
            var actual = context.Facing;
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void FromNorthTurnRightShouldBeEast()
        {
            GetValidContextAndSettingWhichOnTable(out var context, out var settings);
            context.Facing = Facing.North;
            var expect = Facing.East;
            var service = new ToyRobotService(context, settings);
            service.Turn(Direction.Right);
            var actual = context.Facing;
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void FromEastTurnRightShouldBeSouth()
        {
            GetValidContextAndSettingWhichOnTable(out var context, out var settings);
            context.Facing = Facing.East;
            var expect = Facing.South;
            var service = new ToyRobotService(context, settings);
            service.Turn(Direction.Right);
            var actual = context.Facing;
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void FromSouthTurnRightShouldBeWest()
        {
            GetValidContextAndSettingWhichOnTable(out var context, out var settings);
            context.Facing = Facing.South;
            var expect = Facing.West;
            var service = new ToyRobotService(context, settings);
            service.Turn(Direction.Right);
            var actual = context.Facing;
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void FromWestTurnRightShouldBeNorth()
        {
            GetValidContextAndSettingWhichOnTable(out var context, out var settings);
            context.Facing = Facing.West;
            var expect = Facing.North;
            var service = new ToyRobotService(context, settings);
            service.Turn(Direction.Right);
            var actual = context.Facing;
            Assert.AreEqual(expect, actual);
        }

        #endregion

        #region Helper Methods
        private void GetValidMockContextAndSettingWhichOnTable(out Mock<IToyRobotContext> mockContext,
            out Mock<ITableTopSettings> mockSettings)
        {
            mockContext = new Mock<IToyRobotContext>();
            mockSettings = new Mock<ITableTopSettings>();
            var position = new Position() { X = 10, Y = 10 };
            mockContext.Setup(mc => mc.Position).Returns(position);
            mockContext.Setup(mc => mc.Facing).Returns(Facing.North);
            mockSettings.Setup(ms => ms.MaxX).Returns(15);
            mockSettings.Setup(ms => ms.MinX).Returns(5);
            mockSettings.Setup(ms => ms.MaxY).Returns(15);
            mockSettings.Setup(ms => ms.MinY).Returns(5);
        }

        private void GetValidContextAndSettingWhichOnTable(out ToyRobotContext context,
            out TableTopSettings settings)
        {
            context = new ToyRobotContext();
            settings = new TableTopSettings();
            var position = new Position() { X = 10, Y = 10 };
            context.Position = position;
            context.Facing = Facing.North;
            settings.MaxX = 15;
            settings.MinX = 5;
            settings.MaxY = 15;
            settings.MinY = 5;
        }


        #endregion
    }
}
