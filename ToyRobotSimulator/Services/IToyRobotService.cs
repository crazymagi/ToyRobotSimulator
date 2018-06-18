﻿using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Services
{
    public interface IToyRobotService
    {
        Position GetPosition();
        Facing? GetFacing();
        void SetPlace(Position position, Facing facing);
        bool IsOnTable();
        void Move();
        void Turn(Direction direction);
    }
}