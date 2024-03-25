using cd_collection.core.Entities;

namespace cd_collection.core.Contracts;

public interface IItemsRepository
{
    void AddItem(CdItem item);

    bool DeleteItem(Guid guid);

    IEnumerable<CdItem?> GetItems();

    CdItem? GetItem(Guid id);
}