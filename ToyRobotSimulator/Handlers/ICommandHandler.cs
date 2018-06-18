using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotSimulator.Commands;

namespace ToyRobotSimulator.Handlers
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        void Handle(T command);
    }
}
