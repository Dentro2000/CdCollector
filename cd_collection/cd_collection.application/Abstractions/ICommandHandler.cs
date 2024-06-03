namespace cd_collection.application.Abstractions;

public interface ICommandHandler<in TCommand> where TCommand: class, ICommand
{
    Task HandleAsync(TCommand command);
}