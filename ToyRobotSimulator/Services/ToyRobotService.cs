using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotSimulator.Configuration;
using ToyRobotSimulator.Helpers;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Services
{
    public class ToyRobotService : IToyRobotService
    {
        private readonly IToyRobotContext _toyRobotContext;
        private readonly ITableTopSettings _tableTopSettings;

        public ToyRobotService(IToyRobotContext toyRobotContext, ITableTopSettings tableTopSettings)
        {
            _toyRobotContext = toyRobotContext;
            _tableTopSettings = tableTopSettings;
        }

        public Position GetPosition()
        {
            return _toyRobotContext?.Position;
        }

        public Facing? GetFacing()
        {
            return _toyRobotContext?.Facing;
        }

        public void SetPlace(Position position, Facing facing)
        {
            _toyRobotContext.Position = position;
            _toyRobotContext.Facing = facing;
        }

        public bool IsOnTable()
        {
            return _toyRobotContext.Position != null
                   && _toyRobotContext.Facing != null
                   && _toyRobotContext.Position.Y >= _tableTopSettings.MinY
                   && _toyRobotContext.Position.Y <= _tableTopSettings.MaxY
                   && _toyRobotContext.Position.X >= _tableTopSettings.MinX
                   && _toyRobotContext.Position.X <= _tableTopSettings.MaxX;
        }

        public void Move()
        {
            if (!IsOnTable())
            {
                return;
            }

            if (_toyRobotContext.Facing != null)
                switch (_toyRobotContext.Facing.Value)
                {
                    case Facing.North:
                        _toyRobotContext.Position.Y = _toyRobotContext.Position.Y == _tableTopSettings.MaxY
                            ? _toyRobotContext.Position.Y
                            : _toyRobotContext.Position.Y + 1;
                        break;
                    case Facing.South:
                        _toyRobotContext.Position.Y = _toyRobotContext.Position.Y == _tableTopSettings.MinY
                            ? _toyRobotContext.Position.Y
                            : _toyRobotContext.Position.Y - 1;
                        break;
                    case Facing.East:
                        _toyRobotContext.Position.X = _toyRobotContext.Position.X == _tableTopSettings.MaxX
                            ? _toyRobotContext.Position.X
                            : _toyRobotContext.Position.X + 1;
                        break;
                    case Facing.West:
                        _toyRobotContext.Position.X = _toyRobotContext.Position.X == _tableTopSettings.MinX
                            ? _toyRobotContext.Position.X
                            : _toyRobotContext.Position.X - 1;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
        }

        public void Turn(Direction direction)
        {
            if (!IsOnTable())
            {
                return;
            }

            switch (direction)
            {
                case Direction.Left:
                    if (_toyRobotContext.Facing != null)
                        _toyRobotContext.Facing = _toyRobotContext.Facing.Value.TurnLeft();
                    break;
                case Direction.Right:
                    if (_toyRobotContext.Facing != null)
                        _toyRobotContext.Facing = _toyRobotContext.Facing.Value.TurnRight();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

        }
    }
}
