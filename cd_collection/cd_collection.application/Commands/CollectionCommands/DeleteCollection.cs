using cd_collection.application.Abstractions;

namespace cd_collection.application.Commands.CollectionCommands;

public record DeleteCollection(Guid Id): ICommand;