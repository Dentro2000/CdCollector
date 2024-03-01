using cd_collection.Models;

namespace cd_collection.Repository;

public interface ICollectionsRepository
{
    
    List<Collection?> GetCollections();

    Collection? GetCollection(Guid guid);
    Collection? AddCollection(string name);
    
    Collection? UpdateCollection(Guid guid, string collectionName);

    Guid? DeleteCollection(Guid guid);
}