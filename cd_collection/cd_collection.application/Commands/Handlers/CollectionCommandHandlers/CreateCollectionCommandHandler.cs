using cd_collection.application.Abstractions;
using cd_collection.core.Contracts;
using cd_collection.core.Entities;

namespace cd_collection.application.Commands.Handlers.CollectionCommandHandlers;

public class CreateCollectionCommandHandler : ICommandHandler<CreateCollection>
{
    private readonly ICollectionRepository _collectionsRepository;

    public CreateCollectionCommandHandler(ICollectionRepository collectionsRepository)
    {
        _collectionsRepository = collectionsRepository;
    }
    
    public async Task HandleAsync(CreateCollection command)
    {
        var collection = new Collection(name: command.Name, command.CollectionId);
        await _collectionsRepository.AddCollection(collection: collection);
    }
}