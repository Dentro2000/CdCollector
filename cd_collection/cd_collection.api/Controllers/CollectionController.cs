using cd_collection.Models;
using cd_collection.Repository;
using Microsoft.AspNetCore.Mvc;

namespace cd_collection.Controllers;

[ApiController]
[Route("collections")]
public class CdCollectionController : ControllerBase
{
    private readonly ICollectionsRepository _collectionsRepository;
    private readonly ICollectionsRepository _itemsRepository;

    public CdCollectionController(ICollectionsRepository collectionsRepository)
    {
        _collectionsRepository = collectionsRepository;
    }

    [HttpGet]
    public Task<ActionResult<IEnumerable<Collection>>> GetAllCollections()
    {
        return Task.FromResult<ActionResult<IEnumerable<Collection>>>(Ok(_collectionsRepository.GetCollections()));
    }

    [HttpPost]
    public Task<ActionResult<Collection>> CreateCollection(string collectionName)
    {
        var collection = _collectionsRepository.AddCollection(collectionName);
        return Task.FromResult<ActionResult<Collection>>(Ok(collection));
    }


    [HttpGet("collections/{collectionId:guid}")]
    public Task<ActionResult<IEnumerable<Collection>>> GetCollection(Guid collectionId)
    {
        var collection = _collectionsRepository.GetCollections().SingleOrDefault(x => x.Id == collectionId);
        if (collection == null)
        {
            return Task.FromResult<ActionResult<IEnumerable<Collection>>>(NotFound());
        }

        return Task.FromResult<ActionResult<IEnumerable<Collection>>>(Ok(collection));
    }


    [HttpPut("collections/{collectionId:guid}")]
    public Task<ActionResult<Collection>> UpdateCollection(Guid collectionId, string collectionName)
    {
        var collection = _collectionsRepository.UpdateCollection(collectionId, collectionName);
        if (collection == null)
        {
            return Task.FromResult<ActionResult<Collection>>(NotFound());
        }

        return Task.FromResult<ActionResult<Collection>>(Ok(collection));
    }


    [HttpDelete("collections{collectionId:guid}")]
    public async Task<ActionResult> DeleteCollection(Guid collectionId)
    {
        var collection = _collectionsRepository.DeleteCollection(collectionId);
        return NoContent();
    }
}