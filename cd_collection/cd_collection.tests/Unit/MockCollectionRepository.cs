using cd_collection.core.Contracts;
using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;

namespace cd_collection.tests.Unit;

class MockCollectionRepository : ICollectionRepository
{
    private List<Collection> _collections = new List<Collection> { };


    public Task AddCollection(Collection collection)
    {
        _collections.Add(collection);
        return Task.CompletedTask;
    }

    public async Task DeleteCollection(Collection collection)
    {
        _collections.Remove(collection);
    }

    public IEnumerable<Collection?> GetCollections()
    {
        return _collections;
    }


    public async Task UpdateCollection(Collection collection)
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