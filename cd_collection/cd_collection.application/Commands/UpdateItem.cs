using cd_collection.application.Abstractions;

namespace cd_collection.application.Commands;

public record UpdateItem(
    Guid ItemIdentifier,
    string Artist,
    string Title,
    string Label,
    DateOnly ReleaseDate) : ICommand;