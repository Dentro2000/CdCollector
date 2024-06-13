using cd_collection.application.Abstractions;
using cd_collection.application.Commands;
using cd_collection.application.DTO;
using cd_collection.application.Queries;
using cd_collection.application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace cd_collection.Controllers;

[ApiController]
[Route("collections")]
public class CdCollectionController : ControllerBase
{
    private readonly ICollectionsService _collectionsService;
    private readonly ICommandHandler<CreateCollection> _createCollectionCommandHandler;
    private readonly IQueryHandler<GetCollections, IEnumerable<CollectionDto>> _getCollectionsQuery;
    private readonly IQueryHandler<GetCollection, CollectionDto> _getCollectionQuery;

    public CdCollectionController(ICollectionsService collectionsService,
        ICommandHandler<CreateCollection> createCollectionCommandHandler,
        IQueryHandler<GetCollections, IEnumerable<CollectionDto>> getCollectionsQuery,
        IQueryHandler<GetCollection, CollectionDto> getCollectionQuery)
    {
        _collectionsService = collectionsService;
        _createCollectionCommandHandler = createCollectionCommandHandler;
        _getCollectionsQuery = getCollectionsQuery;
        _getCollectionQuery = getCollectionQuery;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CollectionDto>>> GetAllCollections()
    {
        return Ok(await _getCollectionsQuery.HandleAsync(new GetCollections()));
        // return Ok(_collectionsService.GetCollections());
    }

    [HttpPost]
    public async Task<ActionResult<CollectionDto>> CreateCollection(CreateCollection command)
    {
        var guid = Guid.NewGuid();
        await _createCollectionCommandHandler.HandleAsync(command with { collectionId = guid });

        var newCollection = await _getCollectionQuery.HandleAsync(new GetCollection(guid));
        return Ok(newCollection);
    }


    [HttpGet("{collectionId:guid}")]
    public async Task<ActionResult<List<CollectionDto>>> GetCollection(Guid collectionId) =>
        Ok(await _getCollectionQuery.HandleAsync(new GetCollection(collectionId)));


    [HttpPut("{collectionId:guid}")]
    public ActionResult<CollectionDto> UpdateCollection(Guid collectionId, UpdateCollection command)
    {
        var collection = _collectionsService.UpdateCollection(collectionId, command.CollectionName, command.Items);
        if (collection == null)
        {
            return NotFound();
        }

        return Ok(collection);
    }


    [HttpDelete("{collectionId:guid}")]
    public async Task<ActionResult> DeleteCollection(Guid collectionId)
    {
        var isCollectionDeleted = _collectionsService.DeleteCollection(collectionId);
        if (isCollectionDeleted == false)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpPut("items/{collectionId:guid}/add")]
    public ActionResult<CollectionDto> AddItemToCollection(Guid itemId, Guid collectionId)
    {
        var collection = _collectionsService.AddItemToCollection(itemId, collectionId);
        if (collection == null)
        {
            return BadRequest();
        }

        return Ok(collection);
    }

    [HttpDelete("items/{collectionId:guid}/remove")]
    public ActionResult<CollectionDto> RemoveItemFromCollection(Guid itemId, Guid collectionId)
    {
        var collection = _collectionsService.RemoveItemFromCollection(itemId, collectionId);
        if (collection == null)
        {
            return BadRequest();
        }

        return Ok(collection);
    }
}