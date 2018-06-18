using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
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
            var mediator = container.Resolve<IMediator>();
            await mediator.Send(new PlaceCommand() {Facing = Facing.East, Position = new Position() {X = 1, Y = 2}});
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
            BuildMediator(builder);

            var container = builder.Build();
            return container;
        }

        private static void BuildMediator(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();


            //register all handlers
            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>)
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(typeof(PlaceCommand).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}
