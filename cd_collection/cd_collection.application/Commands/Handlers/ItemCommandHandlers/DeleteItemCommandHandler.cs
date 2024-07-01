using cd_collection.application.Abstractions;
using cd_collection.core.Contracts;
using cd_collection.core.Exceptions.ItemServiceExceptions;

namespace cd_collection.application.Commands.Handlers.ItemCommandHandlers;

public class DeleteItemCommandHandler : ICommandHandler<DeleteItem>
{
    private readonly IItemsRepository _itemsRepository;

    public DeleteItemCommandHandler(IItemsRepository itemsRepository)
    {
        _itemsRepository = itemsRepository;
    }

    public async Task HandleAsync(DeleteItem command)
    {
        var item = _itemsRepository.GetItem(command.ItemId);
        if (item == null)
        {
            throw new ItemDontExistsException(command.ItemId);
        }

        await _itemsRepository.DeleteItem(command.ItemId);
    }
}