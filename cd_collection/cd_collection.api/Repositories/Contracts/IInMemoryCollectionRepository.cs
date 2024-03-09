using cd_collection.Models;

namespace cd_collection.Repositories.Contracts;

public interface IInMemoryCollectionRepository
{
    void AddCollection(Collection collection);

    void DeleteCollection(Collection collection);

    IEnumerable<Collection?> GetCollections();
    
    Collection? GetCollection(Guid id);
}