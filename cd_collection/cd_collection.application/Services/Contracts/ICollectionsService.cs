using cd_collection.application.DTO;

namespace cd_collection.application.Services.Contracts;

public interface ICollectionsService
{
    IList<CollectionDto?> GetCollections();

    CollectionDto? GetCollection(Guid guid);

    CollectionDto? CreateCollection(string name);
    CollectionDto? UpdateCollection(Guid guid, string? collectionName, List<Guid> items);

    bool DeleteCollection(Guid guid);

    CollectionDto? RemoveItemFromCollection(Guid itemId, Guid collectionId);

    CollectionDto? AddItemToCollection(Guid itemId, Guid collectionId);
}