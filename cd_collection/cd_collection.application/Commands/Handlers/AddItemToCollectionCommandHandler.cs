using cd_collection.application.Abstractions;
using cd_collection.core.Contracts;
using cd_collection.core.Exceptions.Collection;

namespace cd_collection.application.Commands.Handlers;

public class AddItemToCollectionCommandHandler: ICommandHandler<AddItemToCollection>
{
    
    private readonly ICollectionRepository _collectionsRepository;
    private readonly IItemsRepository _itemsRepository;

    public AddItemToCollectionCommandHandler(ICollectionRepository collectionsRepository, IItemsRepository itemsRepository)
    {
        _collectionsRepository = collectionsRepository;
        _itemsRepository = itemsRepository;
    }

    public async Task HandleAsync(AddItemToCollection command)
    {
        var collection = _collectionsRepository.GetCollection(command.CollectionId);
        var item = _itemsRepository.GetItem(command.ItemId);
        
        if (collection == null || item == null)
        {
            throw new CantAddItemToCollectionException();
        }

        collection.AddItem(item);
        await _collectionsRepository.UpdateCollection(collection);
    }
}