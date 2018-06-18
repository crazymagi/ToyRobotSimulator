using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Commands
{
    public class PlaceCommand : IRequest
    {
        public Position Position { get; set; }

        public Facing Facing { get; set; }

    }
}
