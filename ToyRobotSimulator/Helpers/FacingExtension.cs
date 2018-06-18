using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Helpers
{
    public static class FacingExtension
    {
        public static Facing TurnRight(this Facing originFacing)
        {
            switch (originFacing)
            {
                case Facing.North:
                    return Facing.West;
                case Facing.South:
                    return Facing.East;
                case Facing.East:
                    return Facing.North;
                case Facing.West:
                    return Facing.South;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static Facing TurnLeft(this Facing originFacing)
        {
            switch (originFacing)
            {
                case Facing.North:
                    return Facing.East;
                case Facing.South:
                    return Facing.West;
                case Facing.East:
                    return Facing.South;
                case Facing.West:
                    return Facing.North;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
