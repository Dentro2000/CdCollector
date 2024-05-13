using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;

namespace cd_collection.core.Contracts;

public interface ICollectionRepository
{
    void AddCollection(Collection collection);

    void DeleteCollection(Collection collection);

    IEnumerable<Collection?> GetCollections();

    Collection? GetCollection(ColectionIdentfier id);

    void UpdateCollection(Collection collection);
}