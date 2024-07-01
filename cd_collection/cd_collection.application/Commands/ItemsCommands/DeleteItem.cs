using cd_collection.application.Abstractions;

namespace cd_collection.application.Commands.ItemsCommands;

public record DeleteItem(Guid ItemId): ICommand;