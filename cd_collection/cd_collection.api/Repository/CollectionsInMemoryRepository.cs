using cd_collection.Models;

namespace cd_collection.Repository;

public class CollectionsInMemoryRepository: ICollectionsRepository
{
    private List<Collection?> _collections;

    public CollectionsInMemoryRepository()
    {
        _collections = new List<Collection?>
        {
            new Collection(Guid.NewGuid(), "OneTwoThree", new List<Guid>() { Guid.NewGuid() }, DateTime.Now),
            new Collection(Guid.NewGuid(), "FourFiveSix", new List<Guid>() { Guid.NewGuid() }, DateTime.Now)
        };
    }

    public Collection? GetCollection(Guid guid)
    {
        return _collections.SingleOrDefault(x => x.Id == guid);
    }

    public Collection? AddCollection(string name)
    {
        var collectionId = Guid.NewGuid();
        _collections.Add(new Collection(collectionId, name, new List<Guid>() { Guid.NewGuid() }, DateTime.Now));
        return _collections.SingleOrDefault(x => x.Id == collectionId);
    }

    public List<Collection?> GetCollections()
    {
        return _collections;
    }

    public Collection? UpdateCollection(Guid guid, string collectionName)
    {
        var oldCollection = _collections.SingleOrDefault(x => x.Id == guid);
        if (oldCollection == null)
        {
            return null;
            //throw exception
        }

        var newCollection =
            new Collection(oldCollection.Id, collectionName, oldCollection.ItemsIds, DateTime.Now);

        _collections.Remove(oldCollection);
        _collections.Add(newCollection);

        return newCollection;
    }

    public Guid? DeleteCollection(Guid guid)
    {
        var collection = _collections.SingleOrDefault(x => x.Id == guid);
        if (collection == null)
        {
            //throw exception
            Console.WriteLine($"No collection found ad id: {guid}");
            return guid;
        }

        _collections.Remove(collection);
        return null;
    }
}