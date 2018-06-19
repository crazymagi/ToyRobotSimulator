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
            GetContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void NullPositionIsNotOnTable()
        {
            GetContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Setup(mc => mc.Position).Returns((Position)null);
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void NullFacingIsNotOnTable()
        {
            GetContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            mockContext.Setup(mc => mc.Facing).Returns((Facing?)null);
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void InvalidPositionIsNotOnTable_SmallerThanMinX()
        {
            GetContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var position = mockContext.Object.Position;
            position.X = mockSettings.Object.MinX - 1;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void InvalidPositionIsNotOnTable_SmallerThanMinY()
        {
            GetContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var position = mockContext.Object.Position;
            position.Y = mockSettings.Object.MinY - 1;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void InvalidPositionIsNotOnTable_BiggerThanMaxX()
        {
            GetContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var position = mockContext.Object.Position;
            position.X = mockSettings.Object.MaxX + 1;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void InvalidPositionIsNotOnTable_BiggerThanMaxY()
        {
            GetContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var position = mockContext.Object.Position;
            position.Y = mockSettings.Object.MaxY + 1;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ValidPositionIsOnTable_SameAsMinX()
        {
            GetContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var position = mockContext.Object.Position;
            position.X = mockSettings.Object.MinX;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ValidPositionIsOnTable_SameAsMaxX()
        {
            GetContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var position = mockContext.Object.Position;
            position.X = mockSettings.Object.MaxX;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void ValidPositionIsOnTable_SameAsMinY()
        {
            GetContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var position = mockContext.Object.Position;
            position.Y = mockSettings.Object.MinY;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsTrue(actual);
        }


        [TestMethod]
        public void ValidPositionIsOnTable_SameAsMaxY()
        {
            GetContextAndSettingWhichOnTable(out var mockContext, out var mockSettings);
            var position = mockContext.Object.Position;
            position.Y = mockSettings.Object.MaxY;
            var service = new ToyRobotService(mockContext.Object, mockSettings.Object);
            var actual = service.IsOnTable();
            Assert.IsTrue(actual);
        }
        #endregion



        #region Helper Methods
        private void GetContextAndSettingWhichOnTable(out Mock<IToyRobotContext> mockContext,
            out Mock<ITableTopSettings> mockSettings)
        {
            mockContext = new Mock<IToyRobotContext>();
            mockSettings = new Mock<ITableTopSettings>();
            var position = new Position() { X = 10, Y = 10 };
            mockContext.Setup(mc => mc.Position).Returns(position);
            mockContext.Setup(mc => mc.Facing).Returns(Facing.North);
            mockSettings.Setup(ms => ms.MaxX).Returns(11);
            mockSettings.Setup(ms => ms.MinX).Returns(9);
            mockSettings.Setup(ms => ms.MaxY).Returns(11);
            mockSettings.Setup(ms => ms.MinY).Returns(9);
        }


        #endregion
    }
}
