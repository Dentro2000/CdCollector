using cd_collection.application.Abstractions;

namespace cd_collection.application.Commands;

public record UpdateCollection(Guid CollectionId, string? CollectionName, List<Guid> Items): ICommand;