using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyRobotSimulator.Commands;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Services;

namespace ToyRobotSimulator.Test
{
    [TestClass]
    public class CommandParserTests
    {
        [TestMethod]
        public void ShouldGetMoveCommand()
        {
            var commandString = "Move";
            var parser = new CommandParser();
            var command = parser.GetCommand(commandString);
            Assert.IsTrue(command is MoveCommand);
        }

        [TestMethod]
        public void ShouldGetReportCommand()
        {
            var commandString = "Report";
            var parser = new CommandParser();
            var command = parser.GetCommand(commandString);
            Assert.IsTrue(command is ReportCommand);
        }

        [TestMethod]
        public void ShouldGetTurnLeftCommand()
        {
            var commandString = "Left";
            var parser = new CommandParser();
            var command = parser.GetCommand(commandString);
            Assert.IsTrue(command is TurnCommand);
            Assert.IsTrue((command as TurnCommand).Direction == Direction.Left);
        }

        [TestMethod]
        public void ShouldGetTurnRightCommand()
        {
            var commandString = "Right";
            var parser = new CommandParser();
            var command = parser.GetCommand(commandString);
            Assert.IsTrue(command is TurnCommand);
            Assert.IsTrue((command as TurnCommand).Direction == Direction.Right);
        }

        [TestMethod]
        public void ShouldGetPlaceCommand()
        {
            var commandString = "Place 1,2,west";
            var parser = new CommandParser();
            var command = parser.GetCommand(commandString);
            Assert.IsTrue(command is PlaceCommand);
            Assert.IsTrue((command as PlaceCommand).Position.X == 1);
            Assert.IsTrue((command as PlaceCommand).Position.Y == 2);
            Assert.IsTrue((command as PlaceCommand).Facing == Facing.West);
        }

        [TestMethod]
        public void ShouldIgnoreInvalidCommand()
        {
            var commandString = "Plafce 1,2,west";
            var parser = new CommandParser();
            var command = parser.GetCommand(commandString);
            Assert.IsFalse(command is PlaceCommand);
            Assert.IsNull(command);
        }
    }
}
