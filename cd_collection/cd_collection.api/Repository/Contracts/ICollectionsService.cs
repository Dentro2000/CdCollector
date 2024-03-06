using cd_collection.DTO;
using cd_collection.Models;

namespace cd_collection.Repository;

public interface ICollectionsService
{
    
    IEnumerable<CollectionDto?> GetCollections();

    Collection? GetCollection(Guid guid);
    Collection? AddCollection(string name);

    public Collection? UpdateCollection(Guid guid, string? collectionName, Guid? itemId);

    bool DeleteCollection(Guid guid);
}