using cd_collection.application.Abstractions;
using cd_collection.core.Contracts;
using cd_collection.core.Entities;
using cd_collection.core.Exceptions.ItemServiceExceptions;

namespace cd_collection.application.Commands.Handlers;

public class CreateItemCommandHandler : ICommandHandler<CreateItem>
{
    private readonly IItemsRepository _itemsRepository;

    public CreateItemCommandHandler(IItemsRepository itemsRepository)
    {
        _itemsRepository = itemsRepository;
    }

    public async Task HandleAsync(CreateItem command)
    {
        var item = new CdItem(command.ItemId, command.Artist, command.Title, command.Label, command.ReleaseDate);
        
        
        var ifItemAlreadyExists = _itemsRepository.GetItems().FirstOrDefault(x => x.IsSameItem(item));

        if (ifItemAlreadyExists != null)
        {
            throw new ItemAlreadyExistsException(item);
        }

        await _itemsRepository.AddItemAsync(item);
    }
}