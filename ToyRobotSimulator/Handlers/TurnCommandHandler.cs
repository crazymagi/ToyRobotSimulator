using ToyRobotSimulator.Commands;
using ToyRobotSimulator.Services;

namespace ToyRobotSimulator.Handlers
{
    public class TurnCommandHandler : ICommandHandler<TurnCommand>
    {
        private readonly IToyRobotService _toyRobotService;

        public TurnCommandHandler(IToyRobotService toyRobotService)
        {
            _toyRobotService = toyRobotService;
        }

        public void Handle(TurnCommand command)
        {
            _toyRobotService.Turn(command.Direction);
        }
    }
}
