using cd_collection.Models;

namespace cd_collection.Repository;

public class CollectionsInMemoryRepository : ICollectionsRepository
{
    private List<Collection?> _collections;


    public CollectionsInMemoryRepository()
    {
        _collections = new List<Collection?>
        {
            new Collection(name: "OneTwoThree", itemsIds: new List<Guid>() { Guid.NewGuid(), }),
            new Collection(name: "FourFiveSix", itemsIds: new List<Guid>() { Guid.NewGuid(), }),
        };
    }

    public Collection? GetCollection(Guid guid)
    {
        return _collections.SingleOrDefault(x => x.Id == guid);
    }

    public Collection? AddCollection(string name)
    {
        var collection = new Collection(name: name, new List<Guid>() { Guid.NewGuid() });
        _collections.Add(collection);
        return _collections.SingleOrDefault(x => x.Id == collection.Id);
    }

    public List<Collection?> GetCollections()
    {
        return _collections;
    }

    public Collection? UpdateCollection(Guid guid, string? collectionName, Guid? itemId)
    {
        var collection = _collections.SingleOrDefault(x => x.Id == guid);
        if (collection == null)
        {
            return null;
            //TODO: throw exception
        }

        if (!string.IsNullOrEmpty(collectionName))
        {
            collection.ChangeName(collectionName);
        }

        if (itemId != null && !collection.ItemsIds.Contains(itemId.Value))
        {
            collection.AddItem(itemId.Value);
        }
        
        return collection;
    }

    public bool DeleteCollection(Guid guid)
    {
        //return bool
        var collection = _collections.SingleOrDefault(x => x.Id == guid);
        if (collection == null)
        {
            //throw exception
            Console.WriteLine($"No collection found ad id: {guid}");
            return false;
        }

        _collections.Remove(collection);
        return true;
    }

    public Collection? AddItemToCollection(Guid itemId, Guid collectionId)
    {
        var collection = _collections.SingleOrDefault(x => x.Id == collectionId);
        if (collection == null)
        {
            //throw exception and get rid of optionals
            return null;
        }

        collection.ItemsIds.Add(itemId);
        return collection;
    }

    public Collection? RemoveItemFromCollection(Guid itemId, Guid collectionId)
    {
        var collection = _collections.SingleOrDefault(x => x.Id == collectionId);
        if (collection == null)
        {
            //throw exception
            return null;
        }

        collection.ItemsIds.Remove(itemId);
        return collection;
    }
}