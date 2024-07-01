using cd_collection.application.Abstractions;
using cd_collection.application.Commands.ItemsCommands;
using cd_collection.application.DTO;
using cd_collection.application.Queries;
using cd_collection.Models;
using Microsoft.AspNetCore.Mvc;

namespace cd_collection.Controllers;

[ApiController]
[Route("items")]
public class ItemController : ControllerBase
{
    private readonly IQueryHandler<GetItem, CdItemDto> _getItemQueryHandler;
    private readonly IQueryHandler<GetItems, IEnumerable<CdItemDto>> _getItemsQueryHandler;
    private readonly ICommandHandler<CreateItem> _createItemCommandHandler;
    private readonly ICommandHandler<UpdateItem> _updateItemCommandHandler;
    private readonly ICommandHandler<DeleteItem> _deleteItemCommandHandler;


    public ItemController(
        IQueryHandler<GetItem, CdItemDto> getItemQueryHandler,
        ICommandHandler<CreateItem> createItemCommandHandler, 
        ICommandHandler<UpdateItem> updateItemCommandHandler,
        ICommandHandler<DeleteItem> deleteItemCommandHandler, 
        IQueryHandler<GetItems, IEnumerable<CdItemDto>> getItemsQueryHandler)
    {
        _getItemQueryHandler = getItemQueryHandler;
        _createItemCommandHandler = createItemCommandHandler;
        _updateItemCommandHandler = updateItemCommandHandler;
        _deleteItemCommandHandler = deleteItemCommandHandler;
        _getItemsQueryHandler = getItemsQueryHandler;
    }

    [HttpGet]
    public ActionResult<List<CdItemDto>> GetAllItems()
    {
        return Ok(_getItemsQueryHandler.HandleAsync(new GetItems()));
    }

    [HttpGet("{itemIdentifier:guid}")]
    public async Task<ActionResult<CdItemDto>> GetItem(Guid itemIdentifier)
    {
        return Ok(await _getItemQueryHandler.HandleAsync(new GetItem(itemIdentifier)));
    }

    [HttpPost]
    public async Task<ActionResult<CdItemDto>> CreateItem(CreateItemRequest request)
    {
        var itemGuid = Guid.NewGuid();
        var command = new CreateItem(itemGuid, request.Artist, request.Title, request.Label, request.ReleaseDate);
        await _createItemCommandHandler.HandleAsync(command);
        var item = await _getItemQueryHandler.HandleAsync(new GetItem(itemGuid));

        return (Ok(item));
    }

    [HttpPut("{itemIdentifier:guid}/update")]
    public async Task<ActionResult<CdItemDto>> UpdateItem(Guid itemIdentifier, string artist, string title,
        string label,
        DateOnly releaseDate)
    {
        var updateItemCommand = new UpdateItem(itemIdentifier, artist, title, label, releaseDate);
        await _updateItemCommandHandler.HandleAsync(updateItemCommand);

        var updatedItem = await _getItemQueryHandler.HandleAsync(new GetItem(itemIdentifier));

        return Ok(updatedItem);
    }

    [HttpDelete("{itemIdentifier:guid}/remove")]
    public async Task<ActionResult> DeleteItem(Guid itemIdentifier)
    {
        await _deleteItemCommandHandler.HandleAsync(new DeleteItem(itemIdentifier));
        return NoContent();
    }

}