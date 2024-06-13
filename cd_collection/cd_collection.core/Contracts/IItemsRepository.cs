using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;

namespace cd_collection.core.Contracts;

public interface IItemsRepository
{
    void AddItem(CdItem item);

    bool DeleteItem(CdItemId guid);

    IEnumerable<CdItem> GetItems();

    CdItem? GetItem(CdItemId id);

    void UpdateItem(CdItem item);
}