using ToyRobotSimulator.Commands;
using ToyRobotSimulator.Services;

namespace ToyRobotSimulator.Handlers
{
    public class MoveCommandHandler : ICommandHandler<MoveCommand>
    {
        private readonly IToyRobotService _toyRobotService;

        public MoveCommandHandler(IToyRobotService toyRobotService)
        {
            _toyRobotService = toyRobotService;
        }

        public void Handle(MoveCommand command)
        {
            _toyRobotService.Move();
        }
    }
}
