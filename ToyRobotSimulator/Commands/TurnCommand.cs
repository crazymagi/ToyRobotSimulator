using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Commands
{
    public class TurnCommand : ICommand
    {
        public Direction Direction { get; set; }
        public CommandType CommandType => CommandType.Turn;
    }
}
