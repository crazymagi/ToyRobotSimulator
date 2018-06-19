using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotSimulator.Services
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string contents)
        {
            Console.WriteLine(contents);
        }
    }
}
