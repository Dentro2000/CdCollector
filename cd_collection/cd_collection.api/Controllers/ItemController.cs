using cd_collection.Commands;
using cd_collection.Models;
using cd_collection.Repository;
using Microsoft.AspNetCore.Mvc;

namespace cd_collection.Controllers;

[ApiController]
public class ItemController : ControllerBase
{
    private readonly IItemsService _itemsService;

    public ItemController(IItemsService itemsService)
    {
        _itemsService = itemsService;
    }

    [HttpGet]
    ActionResult<IEnumerable<CdItemModel>> GetItems()
    {
        return Ok(_itemsService.GetItems());
    }

    [HttpGet("items/{guid:guid}")]
    ActionResult<CdItemModel> GetItem(Guid guid)
    {
        return Ok(_itemsService.GetItem(guid));
    }

    [HttpPost]
    ActionResult<CdItemModel> CreateItem(CreateItem command)
    {
        var newItem = _itemsService.CreateItem(
            command.Artist,
            command.Title,
            command.Label,
            command.ReleaseDate);

        if (newItem == null)
        {
            return BadRequest("Item already exists");
        }

        return (Ok(newItem));
    }

    [HttpPut("items/{guid:guid}/update")]
    ActionResult<CdItemModel> UpdateItem(Guid guid, string artist, string title, string label,
        DateTime releaseDate)
    {
        var itemToUpdate = _itemsService.UpdateItem(guid, artist, title, label, releaseDate);
        if (itemToUpdate == null)
        {
            return BadRequest();
        }

        var updatedItem = _itemsService.GetItem(itemToUpdate.Id);

        return Ok(updatedItem);
    }

    [HttpDelete("items/{guid:guid}/remove")]
    public async Task<ActionResult> DeleteCollection(Guid guid)
        => _itemsService.DeleteItem(guid) == false ? BadRequest() : NoContent();
}