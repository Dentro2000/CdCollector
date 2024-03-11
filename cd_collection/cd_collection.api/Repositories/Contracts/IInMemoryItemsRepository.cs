using cd_collection.Models;

namespace cd_collection.Repositories.Contracts;

public interface IInMemoryItemsRepository
{
    void AddItem(CdItemModel item);

    bool DeleteItem(Guid guid);

    IEnumerable<CdItemModel?> GetItems();
    
    CdItemModel? GetItem(Guid id);
}