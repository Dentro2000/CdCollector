using cd_collection.core.Contracts;
using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;

namespace cd_collection.infrastructure.DataAccessLayer.Repositories;

internal class InMemoryCollectionRepository : ICollectionRepository
{
    private readonly List<Collection?> _collections = new List<Collection?>
    {
        new(name: "OneTwoThree", Guid.NewGuid()),
        new(name: "FourFiveSix", Guid.NewGuid()),
    };

    public async Task AddCollection(Collection collection) => _collections.Add(collection);

    public void DeleteCollection(Collection collection) => _collections.Remove(collection);

    public IEnumerable<Collection> GetCollections() => _collections;

    public Collection? GetCollection(ColectionIdentfier id) => _collections.SingleOrDefault(x => x.Id == id);

    public void UpdateCollection(Collection collection)
    {
        throw new NotImplementedException();
    }
}