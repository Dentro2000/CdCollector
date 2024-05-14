using cd_collection.application.DTO;
using cd_collection.core.Entities;

namespace cd_collection.application.Services.Contracts;

public interface ICollectionsService
{
    List<CollectionDto>? GetCollections();

    CollectionDto? GetCollection(Guid guid);

    CollectionDto? CreateCollection(string name);
    CollectionDto? UpdateCollection(Guid guid, string? collectionName, List<CdItem> items);

    bool DeleteCollection(Guid guid);

    CollectionDto? RemoveItemFromCollection(Guid itemId, Guid collectionId);

    CollectionDto? AddItemToCollection(Guid itemId, Guid collectionId);
}