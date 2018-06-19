using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using ToyRobotSimulator.Commands;

namespace ToyRobotSimulator.Handlers
{
    public class CommandInvoker : ICommandInvoker
    {
        private readonly IComponentContext _context;


        public CommandInvoker(IComponentContext context)
        {
            _context = context;
        }

        public void Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandHandler = _context.Resolve<ICommandHandler<TCommand>>();
            commandHandler.Handle(command);
        }
    }
}
