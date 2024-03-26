using cd_collection.application.Commands;
using cd_collection.application.DTO;
using cd_collection.application.Services.Contracts;
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
    public ActionResult<List<CdItemDto>> GetAllItems()
    {
        return Ok(_itemsService.GetItems());
    }

    [HttpGet("{itemIdentifier:guid}")]
    public ActionResult<CdItemDto> GetItem(Guid itemIdentifier)
    {
        return Ok(_itemsService.GetItem(itemIdentifier));
    }

    [HttpPost]
    public ActionResult<CdItemDto> CreateItem(CreateItem command)
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

    [HttpPut("{itemIdentifier:guid}/update")]
    public ActionResult<CdItemDto> UpdateItem(Guid itemIdentifier, string artist, string title, string label,
        DateTime releaseDate)
    {
        var itemToUpdate = _itemsService.UpdateItem(itemIdentifier, artist, title, label, releaseDate);
        if (itemToUpdate == null)
        {
            return BadRequest();
        }

        var updatedItem = _itemsService.GetItem(itemToUpdate.Id);

        return Ok(updatedItem);
    }

    [HttpDelete("{itemIdentifier:guid}/remove")]
    public ActionResult DeleteCollection(Guid itemIdentifier)
        => _itemsService.DeleteItem(itemIdentifier) == false ? BadRequest() : NoContent();
}