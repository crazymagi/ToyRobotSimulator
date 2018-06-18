using System;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using ToyRobotSimulator.Commands;
using ToyRobotSimulator.Configuration;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Services;

namespace ToyRobotSimulator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var container = DependencyInjectionConfig();
            Console.WriteLine("Hello World!");
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

            var container = builder.Build();
            return container;
        }

        
    }
}
