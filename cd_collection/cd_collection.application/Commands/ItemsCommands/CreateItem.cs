using cd_collection.application.Abstractions;

namespace cd_collection.application.Commands.ItemsCommands;

public record CreateItem(Guid ItemId, string Artist, string Title, string Label, DateOnly ReleaseDate): ICommand;