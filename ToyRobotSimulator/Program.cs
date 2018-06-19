using System;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using ToyRobotSimulator.Commands;
using ToyRobotSimulator.Configuration;
using ToyRobotSimulator.Handlers;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Services;

namespace ToyRobotSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = DependencyInjectionConfig();
            var invoker = container.Resolve<ICommandInvoker>();
            var commandParser = new CommandParser();

            while (true)
            {
                var commandString = Console.ReadLine();
                var command = commandParser.GetCommand(commandString);
                if (command != null)
                {
                    if (command is PlaceCommand placeCommand)
                    {
                        invoker.Execute(placeCommand);
                    }
                    else if (command is MoveCommand moveCommand)
                    {
                        invoker.Execute(moveCommand);
                    }
                    else if (command is ReportCommand reportCommand)
                    {
                        invoker.Execute(reportCommand);
                    }
                    else if (command is TurnCommand turnCommand)
                    {
                        invoker.Execute(turnCommand);
                    }
                }

            }
        }

        private static IContainer DependencyInjectionConfig()
        {
            var builder = new ContainerBuilder();
            var settings = new TableTopSettings()
            {
                MaxX = 4,
                MinX = 0,
                MaxY = 4,
                MinY = 0
            };
            builder
                .RegisterInstance<TableTopSettings>(settings)
                .As<ITableTopSettings>();
            builder
                .RegisterType<ToyRobotContext>()
                .As<IToyRobotContext>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ToyRobotService>()
                .As<IToyRobotService>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ConsoleWriter>()
                .As<IWriter>()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(typeof(ICommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder
                .RegisterType<CommandInvoker>()
                .As<ICommandInvoker>()
                .InstancePerLifetimeScope();

            var container = builder.Build();
            return container;
        }



        
    }
}
