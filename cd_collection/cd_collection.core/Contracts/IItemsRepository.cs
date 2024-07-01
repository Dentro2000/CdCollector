using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;

namespace cd_collection.core.Contracts;

public interface IItemsRepository
{
    Task AddItemAsync(CdItem item);

    bool DeleteItem(CdItemId guid);

    IEnumerable<CdItem> GetItems();

    CdItem? GetItem(CdItemId id);

    Task UpdateItemAsync(CdItem item);
}