using cd_collection.DTO;
using cd_collection.Models;

namespace cd_collection.Repository;

public class CollectionsService : ICollectionsService
{
    private List<Collection?> _collections;


    public CollectionsService()
    {
        _collections = new List<Collection?>
        {
            new Collection(name: "OneTwoThree"),
            new Collection(name: "FourFiveSix"),
        };
    }

    public CollectionDto? GetCollection(Guid guid)
    {
        return _collections
            .SingleOrDefault(x => x.Id == guid)
            .ConvertToDto();
    }

    public CollectionDto? CreateCollection(string name)
    {
        var collection = new Collection(name: name);
        _collections.Add(collection);
        
        return _collections
            .SingleOrDefault(x => x.Id == collection.Id)
            .ConvertToDto();
    }

    public IEnumerable<CollectionDto?> GetCollections()
    {
        return _collections.Select(x => x.ConvertToDto());
    }

    public CollectionDto? UpdateCollection(Guid guid, string? collectionName, Guid? itemId)
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
        
        return collection.ConvertToDto();
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

    public CollectionDto? AddItemToCollection(Guid itemId, Guid collectionId)
    {
        var collection = _collections.SingleOrDefault(x => x.Id == collectionId);
        if (collection == null)
        {
            //throw exception and get rid of optionals
            return null;
        }

        collection.ItemsIds.Add(itemId);
        return collection.ConvertToDto();
    }

    public CollectionDto? RemoveItemFromCollection(Guid itemId, Guid collectionId)
    {
        var collection = _collections.SingleOrDefault(x => x.Id == collectionId);
        if (collection == null)
        {
            //throw exception
            return null;
        }

        collection.ItemsIds.Remove(itemId);
        return collection.ConvertToDto();
    }
}