using cd_collection.Models;
using cd_collection.Repository;
using Microsoft.AspNetCore.Mvc;

namespace cd_collection.Controllers;

public class ItemController : ControllerBase
{
    private readonly IItemsService _itemsService;

    public ItemController(IItemsService itemsService)
    {
        _itemsService = itemsService;
    }

    [HttpGet]
    Task<ActionResult<IEnumerable<CdItemModel>>> GetItems()
    {
        return Task.FromResult<ActionResult<IEnumerable<CdItemModel>>>(Ok(_itemsService.GetItems()));
    }

    [HttpGet("items/{guid:guid}")]
    Task<ActionResult<CdItemModel>> GetItem(Guid guid)
    {
        return Task.FromResult<ActionResult<CdItemModel>>(Ok(_itemsService.GetItem(guid)));
    }

    [HttpPost]
    Task<ActionResult<CdItemModel>> CreateItem(string artist, string title, string label, DateTime releaseDate)
    {
        var newItem = _itemsService.CreateItem(artist, title, label, releaseDate);
        if (newItem == null)
        {
            return Task.FromResult<ActionResult<CdItemModel>>(BadRequest("Item already exists"));
        }

        return Task.FromResult<ActionResult<CdItemModel>>(Ok(newItem));
    }

    [HttpPut("items/{guid:guid}")]
    Task<ActionResult<CdItemModel>> UpdateItem(Guid guid, string artist, string title, string label, DateTime releaseDate)
    {
        var itemToUpdate = _itemsService.UpdateItem(guid, artist, title, label, releaseDate);
        var updatedItem = _itemsService.GetItem(itemToUpdate.Id);
        return Task.FromResult<ActionResult<CdItemModel>>(Ok(updatedItem));
    }
    
    [HttpDelete("items/{guid:guid}")]
    public async Task<ActionResult> DeleteCollection(Guid guid)
    {
       _itemsService.DeleteItem(guid);
       return NoContent();
    }
}