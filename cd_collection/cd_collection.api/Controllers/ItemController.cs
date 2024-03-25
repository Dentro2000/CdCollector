using cd_collection.application.Commands;
using cd_collection.application.Services.Contracts;
using cd_collection.core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace cd_collection.Controllers;

[ApiController]
[Route("items")]
public class ItemController : ControllerBase
{
    private readonly IItemsService _itemsService;

    public ItemController(IItemsService itemsService)
    {
        _itemsService = itemsService;
    }

    [HttpGet]
    ActionResult<IEnumerable<CdItem>> GetItems()
    {
        return Ok(_itemsService.GetItems());
    }

    [HttpGet("{itemIdentifier:guid}")]
    ActionResult<CdItem> GetItem(Guid itemIdentifier)
    {
        return Ok(_itemsService.GetItem(itemIdentifier));
    }

    [HttpPost]
    ActionResult<CdItem> CreateItem(CreateItem command)
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

    [HttpPut("{guid:guid}/update")]
    ActionResult<CdItem> UpdateItem(Guid guid, string artist, string title, string label,
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

    [HttpDelete("{guid:guid}/remove")]
    public async Task<ActionResult> DeleteCollection(Guid guid)
        => _itemsService.DeleteItem(guid) == false ? BadRequest() : NoContent();
}