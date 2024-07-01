using cd_collection.application.Abstractions;
using cd_collection.core.ValueObjects;

namespace cd_collection.application.Commands;

public record RemoveItemFromCollection(Guid ItemId, Guid collectionId) : ICommand;