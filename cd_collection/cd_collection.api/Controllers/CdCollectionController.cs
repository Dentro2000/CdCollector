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
    public Task<ActionResult<IEnumerable<CdCollection>>> GetAllCollections()
    {
        return Task.FromResult<ActionResult<IEnumerable<CdCollection>>>(Ok(_repository.GetCollections()));
    }

    [HttpPost]
    public Task<ActionResult<CdCollection>> CreateCollection(string collectionName)
    {
        var collection = _repository.AddCollection(collectionName);
        return Task.FromResult<ActionResult<CdCollection>>(Ok(collection));
    }


    [HttpGet("collections/{collectionId:guid}")]
    public Task<ActionResult<IEnumerable<CdCollection>>> GetCollection(Guid collectionId)
    {
        var collection = _repository.GetCollections().SingleOrDefault(x => x.CollectionId == collectionId);
        if (collection == null)
        {
            return Task.FromResult<ActionResult<IEnumerable<CdCollection>>>(NotFound());
        }

        return Task.FromResult<ActionResult<IEnumerable<CdCollection>>>(Ok(collection));
    }


    [HttpPut("collections/{collectionId:guid}")]
    public Task<ActionResult<CdCollection>> UpdateCollection(Guid collectionId, string collectionName)
    {
        var collection = _repository.UpdateCollection(collectionId, collectionName);
        if (collection == null)
        {
            return Task.FromResult<ActionResult<CdCollection>>(NotFound());
        }

        return Task.FromResult<ActionResult<CdCollection>>(Ok(collection));
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