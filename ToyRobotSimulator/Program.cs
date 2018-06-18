using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using ToyRobotSimulator.Commands;

namespace ToyRobotSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = Config();
            Console.WriteLine("Hello World!");
        }

        static AutofacServiceProvider Config()
        {
            var containerBuilder = new ContainerBuilder();

            
            containerBuilder.RegisterType<PlaceCommand>().As<IRequest>();

            
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}
