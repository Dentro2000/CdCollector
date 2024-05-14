using cd_collection.core.Contracts;
using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;

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
    

    public void UpdateCollection(Collection collection)
    {
        var z = _collections.Single(x => x.Id == collection.Id);
        _collections.Remove(z);
        _collections.Add(collection);
    }

    public Collection? GetCollection(ColectionIdentfier id)
    {
        return _collections.SingleOrDefault(x => x.Id == id);
    }
}