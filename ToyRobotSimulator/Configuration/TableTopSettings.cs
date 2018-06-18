using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotSimulator.Configuration
{
    public class TableTopSettings : ITableTopSettings
    {
        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int MinY { get; set; }
        public int MaxY { get; set; }

    }
}
