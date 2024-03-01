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
    Task<ActionResult<IEnumerable<ItemModel>>> GetItems()
    {
        return Task.FromResult<ActionResult<IEnumerable<ItemModel>>>(Ok(_itemsRepository.GetItems()));
    }

    [HttpGet]
    Task<ActionResult<ItemModel>> GetItem(Guid guid)
    {
        return Task.FromResult<ActionResult<ItemModel>>(Ok(_itemsRepository.GetItem(guid)));
    }

    [HttpPost]
    Task<ActionResult<IEnumerable<ItemModel>>> CreateItem()
    {
        
    }
}