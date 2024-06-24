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

        var ifItemAlreadyExist =
            _itemsRepository
                .GetItems()
                .SingleOrDefault(x => x.IsSameItem(item));

        if (ifItemAlreadyExist != null)
        {
            throw new ItemAlreadyExistsException(item);
        }

        await _itemsRepository.AddItem(item);
    }
}