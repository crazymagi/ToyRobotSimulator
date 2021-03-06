﻿using ToyRobotSimulator.Commands;
using ToyRobotSimulator.Services;

namespace ToyRobotSimulator.Handlers
{
    public class PlaceCommandHandler : ICommandHandler<PlaceCommand>
    {
        private readonly IToyRobotService _toyRobotService;

        public PlaceCommandHandler(IToyRobotService toyRobotService)
        {
            _toyRobotService = toyRobotService;
        }

        public void Handle(PlaceCommand command)
        {
            _toyRobotService.SetPlace(command.Position, command.Facing);
        }
    }
}
