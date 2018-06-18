using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Services
{
    public class ToyRobotService
    {
        private readonly IToyRobotContext _toyRobotContext;

        public ToyRobotService(IToyRobotContext toyRobotContext)
        {
            _toyRobotContext = toyRobotContext;
        }

        public Position GetPosition()
        {
            return _toyRobotContext?.Position;
        }

        public Facing? GetFacing()
        {
            return _toyRobotContext?.Facing;
        }

        public void Move()
        {
            if (_toyRobotContext == null || _toyRobotContext.Position==null || _toyRobotContext.Facing == null)
            {
                return;
            }

            switch (_toyRobotContext.Facing.Value)
            {
                case Facing.North:
                    _toyRobotContext.Position.Y = _toyRobotContext.Position.Y + 1;
                    break;
                case Facing.South:
                    break;
                case Facing.East:
                    break;
                case Facing.West:
                    break;
            }

        }
    }
}
