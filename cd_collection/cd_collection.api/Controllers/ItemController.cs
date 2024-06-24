using cd_collection.application.Commands;
using cd_collection.application.Commands.Handlers;
using cd_collection.application.DTO;
using cd_collection.application.Queries;
using cd_collection.application.Services.Contracts;
using cd_collection.infrastructure.DataAccessLayer.Queries;
using cd_collection.Models;
using Microsoft.AspNetCore.Mvc;

namespace cd_collection.Controllers;

[ApiController]
[Route("items")]
public class ItemController : ControllerBase
{
    private readonly IItemsService _itemsService;
    private readonly GetItemQueryHandler _getItemQueryHandler;
    private readonly CreateItemCommandHandler _createItemCommandHandler;

    public ItemController(IItemsService itemsService, GetItemQueryHandler getItemQueryHandler,
        CreateItemCommandHandler createItemCommandHandler)
    {
        _itemsService = itemsService;
        _getItemQueryHandler = getItemQueryHandler;
        _createItemCommandHandler = createItemCommandHandler;
    }

    [HttpGet]
    public ActionResult<List<CdItemDto>> GetAllItems()
    {
        return Ok(_itemsService.GetItems());
    }

    [HttpGet("{itemIdentifier:guid}")]
    public ActionResult<CdItemDto> GetItem(Guid itemIdentifier)
    {
        return Ok(_getItemQueryHandler.HandleAsync(new GetCdItem(itemIdentifier)));
    }

    [HttpPost]
    public async Task<ActionResult<CdItemDto>> CreateItem(CreateItemRequest request)
    {
        var itemGuid = Guid.NewGuid();
        var command = new CreateItem(itemGuid, request.Artist, request.Title, request.Label, request.ReleaseDate);
        await _createItemCommandHandler.HandleAsync(command);
        var item = await _getItemQueryHandler.HandleAsync(new GetCdItem(itemGuid));

        return (Ok(item));
    }

    [HttpPut("{itemIdentifier:guid}/update")]
    public ActionResult<CdItemDto> UpdateItem(Guid itemIdentifier, string artist, string title, string label,
        DateOnly releaseDate)
    {
        _itemsService.UpdateItem(itemIdentifier, artist, title, label, releaseDate);
        var updatedItem = _itemsService.GetItem(itemIdentifier);

        return Ok(updatedItem);
    }

    [HttpDelete("{itemIdentifier:guid}/remove")]
    public ActionResult DeleteCollection(Guid itemIdentifier)
        => _itemsService.DeleteItem(itemIdentifier) == false ? BadRequest() : NoContent();
}