using cd_collection.DTO;

namespace cd_collection.Services.Contracts;

public interface ICollectionsService
{
    
    IEnumerable<CollectionDto?> GetCollections();

    CollectionDto? GetCollection(Guid guid);
    
    CollectionDto? CreateCollection(string name);
    CollectionDto? UpdateCollection(Guid guid, string? collectionName, List<Guid> items);

    bool DeleteCollection(Guid guid);

    CollectionDto? RemoveItemFromCollection(Guid itemId, Guid collectionId);

    CollectionDto? AddItemToCollection(Guid itemId, Guid collectionId);
}