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

    public CdCollectionController(ICollectionsService collectionsService,
        ICommandHandler<CreateCollection> createCollectionCommandHandler,
        IQueryHandler<GetCollections, IEnumerable<CollectionDto>> getCollectionsQuery)
    {
        _collectionsService = collectionsService;
        _createCollectionCommandHandler = createCollectionCommandHandler;
        _getCollectionsQuery = getCollectionsQuery;
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
        await _createCollectionCommandHandler.HandleAsync(command);
        // var collection = _collectionsService.CreateCollection(command.Name);
        // return Ok(collection);

        //TODO: RETURN OK(COLLECTIONDTO) WHEN QUERY WILL BE IMPLEMENTED
        return NoContent();
    }


    [HttpGet("{collectionId:guid}")]
    public ActionResult<List<CollectionDto>> GetCollection(Guid collectionId)
    {
        var collection = _collectionsService.GetCollection(collectionId);

        if (collection == null)
        {
            return NotFound();
        }

        return (Ok(collection));
    }


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