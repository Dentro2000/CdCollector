using cd_collection.application.Abstractions;
using cd_collection.application.Commands.ItemsCommands;
using cd_collection.core.Contracts;
using cd_collection.core.Exceptions.ItemServiceExceptions;

namespace cd_collection.application.Commands.Handlers.ItemCommandHandlers;

public class UpdateItemCommandHandler: ICommandHandler<UpdateItem>
{
    private readonly IItemsRepository _itemsRepository;

    public UpdateItemCommandHandler(IItemsRepository itemsRepository)
    {
        _itemsRepository = itemsRepository;
    }

    public async Task HandleAsync(UpdateItem command)
    {
        var itemToUpdate = _itemsRepository.GetItem(command.ItemIdentifier);
        
        if (itemToUpdate == null)
        {
            throw new ItemDontExistsException(command.ItemIdentifier);
        }

        if (!string.IsNullOrEmpty(command.Artist))
        {
            itemToUpdate.ChangeArtist(command.Artist);
        }

        if (!string.IsNullOrEmpty(command.Title))
        {
            itemToUpdate.ChangeTitle(command.Title);
        }

        if (!string.IsNullOrEmpty(command.Label))
        {
            itemToUpdate.ChangeLabel(command.Label);
        }

        itemToUpdate.ChangeReleaseDate(command.ReleaseDate);

        await _itemsRepository.UpdateItemAsync(itemToUpdate);
    }
}