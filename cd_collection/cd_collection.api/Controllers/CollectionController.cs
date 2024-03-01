using cd_collection.Models;
using cd_collection.Repository;
using Microsoft.AspNetCore.Mvc;

namespace cd_collection.Controllers;

[ApiController]
[Route("collections")]
public class CdCollectionController : ControllerBase
{
    private readonly CollectionsInMemoryRepository _repository;

    public CdCollectionController(CollectionsInMemoryRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public Task<ActionResult<IEnumerable<Collection>>> GetAllCollections()
    {
        return Task.FromResult<ActionResult<IEnumerable<Collection>>>(Ok(_repository.GetCollections()));
    }

    [HttpPost]
    public Task<ActionResult<Collection>> CreateCollection(string collectionName)
    {
        var collection = _repository.AddCollection(collectionName);
        return Task.FromResult<ActionResult<Collection>>(Ok(collection));
    }


    [HttpGet("collections/{collectionId:guid}")]
    public Task<ActionResult<IEnumerable<Collection>>> GetCollection(Guid collectionId)
    {
        var collection = _repository.GetCollections().SingleOrDefault(x => x.Id == collectionId);
        if (collection == null)
        {
            return Task.FromResult<ActionResult<IEnumerable<Collection>>>(NotFound());
        }

        return Task.FromResult<ActionResult<IEnumerable<Collection>>>(Ok(collection));
    }


    [HttpPut("collections/{collectionId:guid}")]
    public Task<ActionResult<Collection>> UpdateCollection(Guid collectionId, string collectionName)
    {
        var collection = _repository.UpdateCollection(collectionId, collectionName);
        if (collection == null)
        {
            return Task.FromResult<ActionResult<Collection>>(NotFound());
        }

        return Task.FromResult<ActionResult<Collection>>(Ok(collection));
    }


    [HttpDelete("collections{collectionId:guid}")]
    public async Task<ActionResult> DeleteCollection(Guid collectionId)
    {
        var collection = _repository.DeleteCollection(collectionId);
        if (collection != null)
        {
            return NotFound();
        }
        return NoContent();
    }
}