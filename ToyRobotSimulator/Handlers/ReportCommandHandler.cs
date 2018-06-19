using ToyRobotSimulator.Commands;
using ToyRobotSimulator.Services;

namespace ToyRobotSimulator.Handlers
{
    public class ReportCommandHandler : ICommandHandler<ReportCommand>
    {
        private readonly IToyRobotService _toyRobotService;
        private readonly IWriter _writer;

        public ReportCommandHandler(IToyRobotService toyRobotService, IWriter writer)
        {
            _toyRobotService = toyRobotService;
            _writer = writer;
        }

        public void Handle(ReportCommand command)
        {
            if (_toyRobotService.IsOnTable())
            {
                var position = _toyRobotService.GetPosition();
                var facing = _toyRobotService.GetFacing();
                var reportContents = $"Position ({position.X}, {position.Y}), Facing {facing.ToString()}";
                _writer.Write(reportContents);

            }
        }
    }
}
