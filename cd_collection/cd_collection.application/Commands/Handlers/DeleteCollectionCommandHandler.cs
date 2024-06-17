using cd_collection.application.Abstractions;
using cd_collection.core.Contracts;
using cd_collection.core.Exceptions;

namespace cd_collection.application.Commands.Handlers;

public class DeleteCollectionCommandHandler : ICommandHandler<DeleteCollection>
{
    private readonly ICollectionRepository _collectionsRepository;

    public DeleteCollectionCommandHandler(ICollectionRepository collectionsRepository)
    {
        _collectionsRepository = collectionsRepository;
    }

    public async Task HandleAsync(DeleteCollection command)
    {
        var collection = _collectionsRepository.GetCollection(command.Id);
        if (collection == null)
        {
            throw new CannotDeleteException(collection.Id);
        }

        await _collectionsRepository.DeleteCollection(collection);
    }
}