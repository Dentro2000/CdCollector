using cd_collection.application.Abstractions;
using cd_collection.core.Contracts;
using cd_collection.core.Entities;

namespace cd_collection.application.Commands.Handlers;

public class CreateCollectionCommandHandler : ICommandHandler<CreateCollection>
{
    private readonly ICollectionRepository _collectionsRepository;

    public CreateCollectionCommandHandler(ICollectionRepository collectionsRepository)
    {
        _collectionsRepository = collectionsRepository;
    }

    // TODO: CHANGE DATE TIME TO DATE ONLY IN DB -> CDITEM RELEASE DATE
    public async Task HandleAsync(CreateCollection command)
    {
        var collection = new Collection(name: command.Name);
        await _collectionsRepository.AddCollection(collection: collection);
    }
}