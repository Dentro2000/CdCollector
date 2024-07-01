using cd_collection.application.Abstractions;
using cd_collection.application.Commands;
using cd_collection.application.DTO;
using cd_collection.application.Queries;
using cd_collection.application.Services.Contracts;
using cd_collection.Models;
using Microsoft.AspNetCore.Mvc;

namespace cd_collection.Controllers;

[ApiController]
[Route("items")]
public class ItemController : ControllerBase
{
    private readonly IItemsService _itemsService;
    private readonly IQueryHandler<GetCdItem, CdItemDto> _getItemQueryHandler;
    private readonly ICommandHandler<CreateItem> _createItemCommandHandler;
    private readonly ICommandHandler<UpdateItem> _updateItemCommandHandler;

    public ItemController(IItemsService itemsService, IQueryHandler<GetCdItem, CdItemDto> getItemQueryHandler,
        ICommandHandler<CreateItem> createItemCommandHandler, ICommandHandler<UpdateItem> updateItemCommandHandler)
    {
        _itemsService = itemsService;
        _getItemQueryHandler = getItemQueryHandler;
        _createItemCommandHandler = createItemCommandHandler;
        _updateItemCommandHandler = updateItemCommandHandler;
    }

    [HttpGet]
    public ActionResult<List<CdItemDto>> GetAllItems()
    {
        return Ok(_itemsService.GetItems());
    }

    [HttpGet("{itemIdentifier:guid}")]
    public async Task<ActionResult<CdItemDto>> GetItem(Guid itemIdentifier)
    {
        return Ok(await _getItemQueryHandler.HandleAsync(new GetCdItem(itemIdentifier)));
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
    public async Task<ActionResult<CdItemDto>> UpdateItem(Guid itemIdentifier, string artist, string title, string label,
        DateOnly releaseDate)
    {
        var updateItemCommand = new UpdateItem(itemIdentifier, artist, title, label, releaseDate);
        await _updateItemCommandHandler.HandleAsync(updateItemCommand);

        var updatedItem = await _getItemQueryHandler.HandleAsync(new GetCdItem(itemIdentifier));
        
        return Ok(updatedItem);
    }

    [HttpDelete("{itemIdentifier:guid}/remove")]
    public ActionResult DeleteItem(Guid itemIdentifier)
        => _itemsService.DeleteItem(itemIdentifier) == false ? BadRequest() : NoContent();
}