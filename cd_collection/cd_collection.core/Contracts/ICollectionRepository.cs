using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;

namespace cd_collection.core.Contracts;

public interface ICollectionRepository
{
    Task AddCollection(Collection collection);

    Task DeleteCollection(Collection collection);

    IEnumerable<Collection?> GetCollections();

    Collection? GetCollection(ColectionIdentfier id);

    Task UpdateCollection(Collection collection);
}