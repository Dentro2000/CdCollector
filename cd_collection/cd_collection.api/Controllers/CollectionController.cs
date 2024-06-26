using cd_collection.application.Abstractions;
using cd_collection.application.Commands.CollectionCommands;
using cd_collection.application.DTO;
using cd_collection.application.Queries;
using cd_collection.Models;
using Microsoft.AspNetCore.Mvc;

namespace cd_collection.Controllers;

[ApiController]
[Route("collections")]
public class CdCollectionController : ControllerBase
{
    private readonly ICommandHandler<CreateCollection> _createCollectionCommandHandler;
    private readonly IQueryHandler<GetCollections, IEnumerable<CollectionDto>> _getCollectionsQuery;
    private readonly IQueryHandler<GetCollection, CollectionDto> _getCollectionQuery;
    private readonly ICommandHandler<UpdateCollection> _updateCollectionCommandHandler;
    private readonly ICommandHandler<DeleteCollection> _deleteCollectionCommandHandler;
    private readonly ICommandHandler<AddItemToCollection> _addItemToCollectionCommandHandler;
    private readonly ICommandHandler<RemoveItemFromCollection> _removeItemFromCollectionCommandHandler;

    public CdCollectionController(
        ICommandHandler<CreateCollection> createCollectionCommandHandler,
        IQueryHandler<GetCollections, IEnumerable<CollectionDto>> getCollectionsQuery,
        IQueryHandler<GetCollection, CollectionDto> getCollectionQuery,
        ICommandHandler<UpdateCollection> updateCollectionCommandHandler,
        ICommandHandler<DeleteCollection> deleteCollectionCommandHandler,
        ICommandHandler<AddItemToCollection> addItemToCollectionCommandHandler,
        ICommandHandler<RemoveItemFromCollection> removeItemFromCollectionCommandHandler)
    {
        _createCollectionCommandHandler = createCollectionCommandHandler;
        _getCollectionsQuery = getCollectionsQuery;
        _getCollectionQuery = getCollectionQuery;
        _updateCollectionCommandHandler = updateCollectionCommandHandler;
        _deleteCollectionCommandHandler = deleteCollectionCommandHandler;
        _addItemToCollectionCommandHandler = addItemToCollectionCommandHandler;
        _removeItemFromCollectionCommandHandler = removeItemFromCollectionCommandHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CollectionDto>>> GetAllCollections()
    {
        return Ok(await _getCollectionsQuery.HandleAsync(new GetCollections()));
    }

    [HttpPost]
    public async Task<ActionResult<CollectionDto>> CreateCollection(CreateCollectionRequest request)
    {
        var command = new CreateCollection(request.CollectionName, Guid.NewGuid());
        await _createCollectionCommandHandler.HandleAsync(command);

        var newCollection = await _getCollectionQuery.HandleAsync(new GetCollection(command.CollectionId));
        return Ok(newCollection);
    }


    [HttpGet("{collectionId:guid}")]
    public async Task<ActionResult<List<CollectionDto>>> GetCollection(Guid collectionId) =>
        Ok(await _getCollectionQuery.HandleAsync(new GetCollection(collectionId)));


    [HttpPut("{collectionId:guid}")]
    public async Task<ActionResult<CollectionDto>> UpdateCollection(Guid collectionId, UpdateCollection command)
    {
        await _updateCollectionCommandHandler.HandleAsync(command with { CollectionId = collectionId });
        var updated = await _getCollectionQuery.HandleAsync(new GetCollection(collectionId));
        return Ok(updated);
    }


    [HttpDelete("{collectionId:guid}")]
    public async Task<ActionResult> DeleteCollection(Guid collectionId)
    {
        await _deleteCollectionCommandHandler.HandleAsync(new DeleteCollection(collectionId));

        return NoContent();
    }

    [HttpPut("items/{collectionId:guid}/add")]
    public async Task<ActionResult<CollectionDto>> AddItemToCollection(Guid itemId, Guid collectionId)
    {
        await _addItemToCollectionCommandHandler.HandleAsync(new AddItemToCollection(itemId, collectionId));

        var collection = await _getCollectionQuery.HandleAsync(new GetCollection(collectionId));

        return Ok(collection);
    }

    [HttpDelete("items/{collectionId:guid}/remove")]
    public async Task<ActionResult<CollectionDto>> RemoveItemFromCollection(Guid itemId, Guid collectionId)
    {
        await _removeItemFromCollectionCommandHandler.HandleAsync(new RemoveItemFromCollection(itemId, collectionId));
        var collection = await _getCollectionQuery.HandleAsync(new GetCollection(collectionId));
        return Ok(collection);
    }
}