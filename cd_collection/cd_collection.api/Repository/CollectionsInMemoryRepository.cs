using cd_collection.Models;

namespace cd_collection.Repository;

public class CollectionsInMemoryRepository
{
    private List<CdCollection?> _collections;

    public CollectionsInMemoryRepository()
    {
        _collections = new List<CdCollection?>
        {
            new CdCollection(Guid.NewGuid(), "OneTwoThree", new List<Guid>() { Guid.NewGuid() }, DateTime.Now),
            new CdCollection(Guid.NewGuid(), "DupaDupa", new List<Guid>() { Guid.NewGuid() }, DateTime.Now)
        };
    }


    public CdCollection? AddCollection(string name)
    {
        var collectionId = Guid.NewGuid();
        _collections.Add(new CdCollection(collectionId, name, new List<Guid>() { Guid.NewGuid() }, DateTime.Now));
        return _collections.SingleOrDefault(x => x.CollectionId == collectionId);
    }

    public List<CdCollection?> GetCollections()
    {
        return _collections;
    }

    public CdCollection? UpdateCollection(Guid guid, string collectionName)
    {
        var oldCollection = _collections.SingleOrDefault(x => x.CollectionId == guid);
        if (oldCollection == null)
        {
            return null;
            //throw exception
        }

        var newCollection =
            new CdCollection(oldCollection.CollectionId, collectionName, oldCollection.CdIds, DateTime.Now);

        _collections.Remove(oldCollection);
        _collections.Add(newCollection);

        return newCollection;
    }

    public Guid? DeleteCollection(Guid guid)
    {
        var collection = _collections.SingleOrDefault(x => x.CollectionId == guid);
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