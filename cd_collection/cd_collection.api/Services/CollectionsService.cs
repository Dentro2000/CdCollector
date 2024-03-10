using cd_collection.DTO;
using cd_collection.Models;
using cd_collection.Repositories.Contracts;

namespace cd_collection.Repository;

public class CollectionsService : ICollectionsService
{
    private IInMemoryCollectionRepository _repository;

    public CollectionsService(IInMemoryCollectionRepository repository)
    {
        _repository = repository;
    }

    public CollectionDto? GetCollection(Guid guid) => _repository.GetCollection(guid)?.ConvertToDto();

    public CollectionDto CreateCollection(string name)
    {
        var collection = new Collection(name: name);
        
        _repository.AddCollection(collection: collection);
        
        return _repository
            .GetCollection(collection.Id)
            .ConvertToDto();
    }

    public IEnumerable<CollectionDto?> GetCollections()
    {
        return _repository
            .GetCollections()
            .Select(x => x.ConvertToDto());
    }

    public CollectionDto? UpdateCollection(Guid guid, string? collectionName, Guid? itemId)
    {
        var collection = _repository.GetCollection(guid);
        
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
        var collection = _repository.GetCollection(guid);
        if (collection == null)
        {
            //throw exception
            Console.WriteLine($"No collection found ad id: {guid}");
            return false;
        }

        _repository.DeleteCollection(collection);
        return true;
    }

    public CollectionDto? AddItemToCollection(Guid itemId, Guid collectionId)
    {
        var collection = _repository.GetCollection(collectionId);
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
        var collection = _repository.GetCollection(collectionId);
        if (collection == null)
        {
            //throw exception
            return null;
        }

        collection.ItemsIds.Remove(itemId);
        return collection.ConvertToDto();
    }
}