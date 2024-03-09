using cd_collection.Commands;
using cd_collection.DTO;
using cd_collection.Models;
using cd_collection.Repository;
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


    [HttpGet("collections/{collectionId:guid}")]
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


    [HttpPut("collections/{collectionId:guid}")]
    public ActionResult<CollectionDto> UpdateCollection(Guid collectionId, UpdateCollection command)
    {
        var collection = _collectionsService.UpdateCollection(collectionId, command.collectionName, command.itemId);
        if (collection == null)
        {
            return NotFound();
        }

        return Ok(collection);
    }


    [HttpDelete("collections{collectionId:guid}")]
    public async Task<ActionResult> DeleteCollection(Guid collectionId)
    {
        var collection = _collectionsService.DeleteCollection(collectionId);
        return NoContent();
    }
}