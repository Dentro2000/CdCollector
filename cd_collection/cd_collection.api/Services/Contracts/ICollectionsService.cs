using cd_collection.DTO;
using cd_collection.Models;

namespace cd_collection.Repository;

public interface ICollectionsService
{
    
    IEnumerable<CollectionDto?> GetCollections();

    CollectionDto? GetCollection(Guid guid);
    
    CollectionDto? CreateCollection(string name);

    public CollectionDto? UpdateCollection(Guid guid, string? collectionName, List<Guid> items);

    bool DeleteCollection(Guid guid);

    CollectionDto? RemoveItemFromCollection(Guid itemId, Guid collectionId);

    CollectionDto? AddItemToCollection(Guid itemId, Guid collectionId);
}