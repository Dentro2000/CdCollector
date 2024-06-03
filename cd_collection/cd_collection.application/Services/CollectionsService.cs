using cd_collection.application.DTO;
using cd_collection.application.Extensions;
using cd_collection.application.Services.Contracts;
using cd_collection.core.Contracts;
using cd_collection.core.Entities;
using cd_collection.core.Exceptions.Collection;
using cd_collection.core.ValueObjects;

namespace cd_collection.application.Services;

public class CollectionsService : ICollectionsService
{
    private readonly ICollectionRepository _collectionsRepository;
    private readonly IItemsRepository _itemsRepository;

    public CollectionsService(
        ICollectionRepository collectionsRepository,
        IItemsRepository itemsRepository)
    {
        _collectionsRepository = collectionsRepository;
        _itemsRepository = itemsRepository;
    }

    public CollectionDto? GetCollection(Guid guid) => _collectionsRepository.GetCollection(guid)?.ConvertToDto();

    // public CollectionDto CreateCollection(string name)
    // {
    //     var collection = new Collection(name: name);
    //
    //     _collectionsRepository.AddCollection(collection: collection);
    //
    //     return _collectionsRepository
    //         .GetCollection(collection.Id)
    //         .ConvertToDto();
    // }

    public IList<CollectionDto> GetCollections()
    {
        var collections = 
            _collectionsRepository
            .GetCollections()
            .ToList();

        if (!collections.Any())
        {
            return new List<CollectionDto>() { };
        }

        return collections.Select(x => x.ConvertToDto())
            .ToList();
    }

    public CollectionDto? UpdateCollection(Guid guid, string? collectionName, List<Guid> itemIds)
    {
        var collection = _collectionsRepository.GetCollection(guid);
        var items = _itemsRepository.GetItems().Where(x => itemIds.Contains(x.Id)).ToList();

        if (collection == null)
        {
            throw new CannotUpdateException(guid);
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
        
        _collectionsRepository.UpdateCollection(collection);

        var z = _collectionsRepository.GetCollection(collection.Id);

        return z.ConvertToDto();
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
        var collection = _collectionsRepository.GetCollection(collectionId);
        var item = _itemsRepository.GetItem(itemId);

        if (collection == null || item == null)
        {
            throw new CantAddItemToCollectionException();
        }

        collection.AddItem(item);
        _collectionsRepository.UpdateCollection(collection);

        return _collectionsRepository.GetCollection(collectionId).ConvertToDto();
    }

    public CollectionDto? RemoveItemFromCollection(Guid itemId, Guid collectionId)
    {
        var collection = _collectionsRepository.GetCollection(collectionId);
        var item = _itemsRepository.GetItem(itemId);

        if (item == null || collection == null)
        {
            return null;
        }

        collection.RemoveItem(item);
        _collectionsRepository.UpdateCollection(collection);
        return _collectionsRepository.GetCollection(collectionId).ConvertToDto();
    }
}