using cd_collection.Models;
using cd_collection.Repository;
using Microsoft.AspNetCore.Mvc;

namespace cd_collection.Controllers;

public class ItemController : ControllerBase
{
    private readonly IItemsRepository _itemsRepository;

    public ItemController(IItemsRepository itemsRepository)
    {
        _itemsRepository = itemsRepository;
    }

    [HttpGet]
    Task<ActionResult<IEnumerable<CdItemModel>>> GetItems()
    {
        return Task.FromResult<ActionResult<IEnumerable<CdItemModel>>>(Ok(_itemsRepository.GetItems()));
    }

    [HttpGet("items/{guid:guid}")]
    Task<ActionResult<CdItemModel>> GetItem(Guid guid)
    {
        return Task.FromResult<ActionResult<CdItemModel>>(Ok(_itemsRepository.GetItem(guid)));
    }

    [HttpPost]
    Task<ActionResult<CdItemModel>> CreateItem(string artist, string title, string label, DateTime releaseDate)
    {
        var newItem = _itemsRepository.CreateItem(artist, title, label, releaseDate);
        if (newItem == null)
        {
            return Task.FromResult<ActionResult<CdItemModel>>(BadRequest("Item already exists"));
        }

        return Task.FromResult<ActionResult<CdItemModel>>(Ok(newItem));
    }

    [HttpPut("items/{guid:guid}")]
    Task<ActionResult<CdItemModel>> UpdateItem(Guid guid, string artist, string title, string label, DateTime releaseDate)
    {
        var itemToUpdate = _itemsRepository.UpdateItem(guid, artist, title, label, releaseDate);
        var updatedItem = _itemsRepository.GetItem(itemToUpdate.Id);
        return Task.FromResult<ActionResult<CdItemModel>>(Ok(updatedItem));
    }
    
    [HttpDelete("items/{guid:guid}")]
    public async Task<ActionResult> DeleteCollection(Guid guid)
    {
       _itemsRepository.DeleteItem(guid);
       return NoContent();
    }
}