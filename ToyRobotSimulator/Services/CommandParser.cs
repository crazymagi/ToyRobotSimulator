using System;
using System.Runtime.InteropServices.WindowsRuntime;
using ToyRobotSimulator.Commands;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Services
{
    public class CommandParser
    {
        private const string MoveString = "MOVE"; 
        private const string ReportString = "REPORT"; 
        private const string LeftString = "LEFT"; 
        private const string RightString = "RIGHT"; 
        private const string PlaceString = "PLACE"; 
        private const string EastString = "EAST"; 
        private const string WestString = "WEST"; 
        private const string SouthString = "SOUTH";
        private const string NorthString = "NORTH";
        
        public ICommand GetCommand(string input)
        {
            if (input == null)
                return null;

            var formatedInput = input.Trim().ToUpper();

            switch (formatedInput)
            {
                case MoveString:
                    return new MoveCommand();
                case ReportString:
                    return new ReportCommand();
                case LeftString:
                    return new TurnCommand {Direction = Direction.Left};
                case RightString:
                    return new TurnCommand {Direction = Direction.Right};
            }

            if (formatedInput.StartsWith(PlaceString))
            {
                var parameters = formatedInput
                    .Substring(PlaceString.Length, formatedInput.Length - PlaceString.Length)
                    .Split(',');
                if (parameters.Length == 3)
                {
                    var parameterX = parameters[0].Trim();
                    var parameterY = parameters[1].Trim();
                    var parameterFacing = parameters[2].Trim();
                    if (int.TryParse(parameterX, out var positionX) 
                        && int.TryParse(parameterY, out var positionY) 
                        && TryParseFacing(parameterFacing, out var facing))
                    {
                        return new PlaceCommand
                        {
                            Position = new Position { X = positionX, Y=positionY},
                            Facing = facing
                        };
                    }
                }
            }

            return null;


        }

        private bool TryParseFacing(string facingString, out Facing facing)
        {
            switch ((facingString??string.Empty).ToUpper())
            {
                case EastString:
                    facing = Facing.East;
                    return true;
                case WestString:
                    facing = Facing.West;
                    return true;
                case SouthString:
                    facing = Facing.South;
                    return true;
                case NorthString:
                    facing = Facing.North;
                    return true;
                default:
                    facing = default;
                    return false;
            }
        }
    }
}
