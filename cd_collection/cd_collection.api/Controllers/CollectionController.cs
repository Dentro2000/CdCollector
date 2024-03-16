using cd_collection.Commands;
using cd_collection.DTO;
using cd_collection.Repository;
using cd_collection.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace cd_collection.Controllers;

[ApiController]
[Route("collections")]
public class CdCollectionController : ControllerBase
{
    private readonly ICollectionsService _collectionsService;
    private readonly ICollectionsService _itemsService;

    public CdCollectionController(ICollectionsService collectionsService)
    {
        _collectionsService = collectionsService;
    }

    [HttpGet]
    public ActionResult<List<CollectionDto>> GetAllCollections()
    {
        return Ok(_collectionsService.GetCollections());
    }

    [HttpPost]
    public ActionResult<CollectionDto> CreateCollection(CreateCollection command)
    {
        var collection = _collectionsService.CreateCollection(command.Name);
        return Ok(collection);
    }


    [HttpGet("{collectionId:guid}")]
    public ActionResult<List<CollectionDto>> GetCollection(Guid collectionId)
    {
        var collection = _collectionsService
            .GetCollections()
            .SingleOrDefault(x => x.Id == collectionId);

        if (collection == null)
        {
            return NotFound();
        }

        return (Ok(collection));
    }


    [HttpPut("{collectionId:guid}")]
    public ActionResult<CollectionDto> UpdateCollection(Guid collectionId, UpdateCollection command)
    {
        var collection = _collectionsService.UpdateCollection(collectionId, command.collectionName, command.items);
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
    public ActionResult<CollectionDto> AddItemToCollection( Guid itemId, Guid collectionId)
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