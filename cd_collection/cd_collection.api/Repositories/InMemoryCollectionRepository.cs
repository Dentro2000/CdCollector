using cd_collection.Entities;
using cd_collection.Models;
using cd_collection.Repositories.Contracts;

namespace cd_collection.Repositories;

public class InMemoryCollectionRepository : IInMemoryCollectionRepository
{
    private readonly List<Collection?> _collections = new List<Collection?>
    {
        new (name: "OneTwoThree"),
        new (name: "FourFiveSix"),
    };
    
    public void AddCollection(Collection collection)
    {
       _collections.Add(collection);
    }

    public void DeleteCollection(Collection collection)
    {
        _collections.Remove(collection);
    }

    public IEnumerable<Collection> GetCollections()
    {
        return _collections;
    }

    public Collection? GetCollection(Guid id)
    {
        return _collections.SingleOrDefault(x => x.Id == id);
    }
}