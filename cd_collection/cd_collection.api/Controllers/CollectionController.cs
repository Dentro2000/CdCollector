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
    public Task<ActionResult<Collection>> CreateCollection(string collectionName)
    {
        var collection = _collectionsService.AddCollection(collectionName);
        return Task.FromResult<ActionResult<Collection>>(Ok(collection));
    }


    [HttpGet("collections/{collectionId:guid}")]
    public Task<ActionResult<IEnumerable<Collection>>> GetCollection(Guid collectionId)
    {
        var collection = _collectionsService.GetCollections().SingleOrDefault(x => x.Id == collectionId);
        if (collection == null)
        {
            return Task.FromResult<ActionResult<IEnumerable<Collection>>>(NotFound());
        }

        return Task.FromResult<ActionResult<IEnumerable<Collection>>>(Ok(collection));
    }


    [HttpPut("collections/{collectionId:guid}")]
    public Task<ActionResult<Collection>> UpdateCollection(Guid collectionId, string? collectionName, Guid? itemId)
    {
        var collection = _collectionsService.UpdateCollection(collectionId, collectionName, itemId);
        if (collection == null)
        {
            return Task.FromResult<ActionResult<Collection>>(NotFound());
        }

        return Task.FromResult<ActionResult<Collection>>(Ok(collection));
    }


    [HttpDelete("collections{collectionId:guid}")]
    public async Task<ActionResult> DeleteCollection(Guid collectionId)
    {
        var collection = _collectionsService.DeleteCollection(collectionId);
        return NoContent();
    }
}