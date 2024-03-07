using cd_collection.DTO;
using cd_collection.Models;

namespace cd_collection.Repository;

public interface ICollectionsService
{
    
    IEnumerable<CollectionDto?> GetCollections();

    CollectionDto? GetCollection(Guid guid);
    CollectionDto? CreateCollection(string name);

    public Collection? UpdateCollection(Guid guid, string? collectionName, Guid? itemId);

    bool DeleteCollection(Guid guid);
}