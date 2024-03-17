using cd_collection.Models;

namespace cd_collection.Repositories.Contracts;

public interface IInMemoryItemsRepository
{
    void AddItem(CdItem item);

    bool DeleteItem(Guid guid);

    IEnumerable<CdItem?> GetItems();
    
    CdItem? GetItem(Guid id);
}