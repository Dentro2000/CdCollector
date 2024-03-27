using cd_collection.application.DTO;
using cd_collection.application.Extensions;
using cd_collection.application.Services.Contracts;
using cd_collection.core.Contracts;
using cd_collection.core.Entities;
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

    public CollectionDto CreateCollection(string name)
    {
        var collection = new Collection(name: name);

        _collectionsRepository.AddCollection(collection: collection);

        return _collectionsRepository
            .GetCollection(collection.Identifier)
            .ConvertToDto();
    }

    public IList<CollectionDto?> GetCollections()
    {
        return _collectionsRepository
            .GetCollections()
            .Select(x => x.ConvertToDto())
            .ToList();
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
            collection.SetAllItems(items.ToIdentifiers());
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

        return collection.AddItem(itemId).ConvertToDto();
    }

    public CollectionDto? RemoveItemFromCollection(Guid itemId, Guid collectionId)
    {
        var collection = _collectionsRepository.GetCollection(collectionId);
        var item = _itemsRepository.GetItem(itemId);

        if (item == null || collection == null)
        {
            return null;
        }

        return collection.RemoveItem(itemId).ConvertToDto();
    }
}