using ToyRobotSimulator.Commands;
using ToyRobotSimulator.Services;

namespace ToyRobotSimulator.Handlers
{
    public class ReportCommandHandler : ICommandHandler<ReportCommand>
    {
        private readonly IToyRobotService _toyRobotService;

        public ReportCommandHandler(IToyRobotService toyRobotService)
        {
            _toyRobotService = toyRobotService;
        }

        public void Handle(ReportCommand command)
        {
            var position = _toyRobotService.GetPosition();
            var facing = _toyRobotService.GetFacing();
        }
    }
}
