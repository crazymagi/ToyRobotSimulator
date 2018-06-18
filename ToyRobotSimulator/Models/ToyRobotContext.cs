using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotSimulator.Models
{
    public class ToyRobotContext : IToyRobotContext
    {
        public Position Position { get; set; }
        public Facing? Facing { get; set; }

        public bool IsValid => Position != null && Facing != null;

    }
}
