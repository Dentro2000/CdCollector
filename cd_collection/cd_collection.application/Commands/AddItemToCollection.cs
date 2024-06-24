using cd_collection.application.Abstractions;

namespace cd_collection.application.Commands;

public record AddItemToCollection(Guid ItemId, Guid CollectionId): ICommand;