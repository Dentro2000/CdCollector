using cd_collection.Models;
using cd_collection.Repositories.Contracts;

namespace cd_collection.tests.Unit;

class MockCollectionRepository : IInMemoryCollectionRepository
{
    private List<Collection> _collections = new List<Collection> { };


    public void AddCollection(Collection collection)
    {
        _collections.Add(collection);
    }

    public void DeleteCollection(Collection collection)
    {
        _collections.Remove(collection);
    }

    public IEnumerable<Collection?> GetCollections()
    {
        return _collections;
    }

    public Collection? GetCollection(Guid id)
    {
        return _collections.SingleOrDefault(x => x.Id == id);
    }
}