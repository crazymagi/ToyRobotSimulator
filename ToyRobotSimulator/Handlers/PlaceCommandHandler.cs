using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToyRobotSimulator.Commands;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Services;

namespace ToyRobotSimulator.Handlers
{
    public class PlaceCommandHandler : IRequestHandler<PlaceCommand>
    {
        private readonly IToyRobotService _toyRobotService;

        public PlaceCommandHandler(IToyRobotService toyRobotService)
        {
            _toyRobotService = toyRobotService;
        }

        public async Task<Unit> Handle(PlaceCommand request, CancellationToken cancellationToken)
        {
            Thread.Yield();
            _toyRobotService.SetPlace(request.Position, request.Facing);
            return new Unit();
        }
    }
}
