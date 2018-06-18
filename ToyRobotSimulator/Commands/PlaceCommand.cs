using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Commands
{
    public class PlaceCommand : ICommand
    {
        public Position Position { get; set; }

        public Facing Facing { get; set; }

        public CommandType CommandType => CommandType.Place;
    }
}
