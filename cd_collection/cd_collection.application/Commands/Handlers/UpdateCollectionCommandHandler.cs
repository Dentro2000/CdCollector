using cd_collection.application.Abstractions;
using cd_collection.core.Contracts;
using cd_collection.core.Exceptions.Collection;

namespace cd_collection.application.Commands.Handlers;

public class UpdateCollectionCommandHandler : ICommandHandler<UpdateCollection>
{
    private readonly ICollectionRepository _collectionsRepository;
    private readonly IItemsRepository _itemsRepository;

    public UpdateCollectionCommandHandler(
        ICollectionRepository collectionsRepository,
        IItemsRepository itemsRepository)
    {
        _collectionsRepository = collectionsRepository;
        _itemsRepository = itemsRepository;
    }

    public async Task HandleAsync(UpdateCollection command)
    {
        var collectionToUpdate = _collectionsRepository.GetCollection(command.collectionId);
        var items = _itemsRepository.GetItems().Where(x => command.Items.Contains(x.Id)).ToList();
        
        if (collectionToUpdate == null)
        {
            throw new CannotUpdateException(command.collectionId);
        }
        
        if (!string.IsNullOrEmpty(command.CollectionName))
        {
            collectionToUpdate.ChangeName(command.CollectionName);
        }
        
        //User should pass updated list of items
        // not only items that should be added

        if (items.Count > 0)
        {
            collectionToUpdate.SetAllItems(items);
        }
        
        await _collectionsRepository.UpdateCollection(collectionToUpdate);
        
        
    }
}