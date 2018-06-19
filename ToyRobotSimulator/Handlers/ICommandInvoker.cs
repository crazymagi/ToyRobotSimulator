using ToyRobotSimulator.Commands;

namespace ToyRobotSimulator.Handlers
{
    public interface ICommandInvoker
    {
        void Execute<TCommand>(TCommand command) where TCommand : ICommand;
    }
}