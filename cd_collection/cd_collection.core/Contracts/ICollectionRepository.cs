using cd_collection.core.Entities;

namespace cd_collection.core.Contracts;

public interface ICollectionRepository
{
    void AddCollection(Collection collection);

    void DeleteCollection(Collection collection);

    IEnumerable<Collection?> GetCollections();

    Collection? GetCollection(Guid id);
}