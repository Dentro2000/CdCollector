using cd_collection.application.Abstractions;
using cd_collection.application.Commands.CollectionCommands;
using cd_collection.core.Contracts;
using cd_collection.core.Exceptions.Collection;
using cd_collection.core.Exceptions.ItemServiceExceptions;

namespace cd_collection.application.Commands.Handlers.CollectionCommandHandlers;

public class RemoveItemFromCollectionCommandHandler : ICommandHandler<RemoveItemFromCollection>
{
    private readonly ICollectionRepository _collectionsRepository;
    private readonly IItemsRepository _itemsRepository;

    public RemoveItemFromCollectionCommandHandler(ICollectionRepository collectionsRepository,
        IItemsRepository itemsRepository)
    {
        _collectionsRepository = collectionsRepository;
        _itemsRepository = itemsRepository;
    }

    public async Task HandleAsync(RemoveItemFromCollection command)
    {
        var collection = _collectionsRepository.GetCollection(command.collectionId);
        var item = _itemsRepository.GetItem(command.ItemId);

        if (collection == null)
        {
            throw new CannotUpdateException(command.collectionId);
        }

        if (item == null)
        {
            throw new ItemDontExistsException(command.ItemId);
        }

        collection.RemoveItem(item);
        await _collectionsRepository.UpdateCollection(collection);
    }
}