using cd_collection.DTO;
using cd_collection.Models;
using cd_collection.Repositories;
using cd_collection.Repositories.Contracts;
using cd_collection.Services.Contracts;

namespace cd_collection.Services;

public class CollectionsService : ICollectionsService
{
    private readonly IInMemoryCollectionRepository _collectionsRepository;
    private readonly IInMemoryItemsRepository _itemsRepository;

    public CollectionsService(
        IInMemoryCollectionRepository collectionsRepository,
        IInMemoryItemsRepository itemsRepository)
    {
        _collectionsRepository = collectionsRepository;
        _itemsRepository = itemsRepository;
    }

    public CollectionDto? GetCollection(Guid guid) => _collectionsRepository.GetCollection(guid)?.ConvertToDto();

    public CollectionDto CreateCollection(string name)
    {
        var collection = new Collection(name: name);

        _collectionsRepository.AddCollection(collection: collection);

        return _collectionsRepository
            .GetCollection(collection.Id)
            .ConvertToDto();
    }

    public IEnumerable<CollectionDto?> GetCollections()
    {
        return _collectionsRepository
            .GetCollections()
            .Select(x => x.ConvertToDto());
    }

    public CollectionDto? UpdateCollection(Guid guid, string? collectionName, List<Guid> items)
    {
        var collection = _collectionsRepository.GetCollection(guid);

        if (collection == null)
        {
            return null;
            //TODO: throw exception
        }

        if (!string.IsNullOrEmpty(collectionName))
        {
            collection.ChangeName(collectionName);
        }

        //User should pass updated list of items
        // not only items that should be added
        
        if (items.Count > 0)
        {
            collection.SetAllItems(items);
        }
        
        return collection.ConvertToDto();
    }

    public bool DeleteCollection(Guid guid)
    {
        //return bool
        var collection = _collectionsRepository.GetCollection(guid);
        if (collection == null)
        {
            //throw exception
            Console.WriteLine($"No collection found ad id: {guid}");
            return false;
        }

        _collectionsRepository.DeleteCollection(collection);
        return true;
    }

    public CollectionDto? AddItemToCollection(Guid itemId, Guid collectionId)
    {
        //Handle on the items side
        var collection = _collectionsRepository.GetCollection(collectionId);
        var item = _itemsRepository.GetItem(itemId);

        if (collection == null || item == null)
        {
            //throw exception and get rid of optionals
            return null;
        }

        collection.ItemsIds.Add(itemId);
        return collection.ConvertToDto();
    }

    public CollectionDto? RemoveItemFromCollection(Guid itemId, Guid collectionId)
    {
        var collection = _collectionsRepository.GetCollection(collectionId);
        var item = _itemsRepository.GetItem(itemId);

        if (item == null)
        {
            return null;
        }

        if (collection == null)
        {
            //throw exception
            return null;
        }

        if (!_itemsRepository.DeleteItem(item.Id))
        {
            //throw exception
            return null;
        }


        collection.ItemsIds.Remove(itemId);
        return collection.ConvertToDto();
    }
}