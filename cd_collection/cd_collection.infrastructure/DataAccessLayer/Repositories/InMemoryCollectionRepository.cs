using cd_collection.core.Contracts;
using cd_collection.core.Entities;

namespace cd_collection.infrastructure.DataAccessLayer.Repositories;

internal class InMemoryCollectionRepository : ICollectionRepository
{
    private readonly List<Collection?> _collections = new List<Collection?>
    {
        new(name: "OneTwoThree"),
        new(name: "FourFiveSix"),
    };

    public void AddCollection(Collection collection) => _collections.Add(collection);

    public void DeleteCollection(Collection collection) => _collections.Remove(collection);

    public IEnumerable<Collection> GetCollections() => _collections;

    public Collection? GetCollection(Guid id) => _collections.SingleOrDefault(x => x.Identifier.Value == id);
}