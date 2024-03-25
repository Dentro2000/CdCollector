using cd_collection.core.Contracts;
using cd_collection.Entities;
using cd_collection.Models;

namespace cd_collection.tests.Unit;

class MockCollectionRepository : ICollectionRepository
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