using cd_collection.Models;

namespace cd_collection.Repositories.Contracts;

public interface IInMemoryItemsRepository
{
    void AddItem(CdItemModel item);

    void DeleteItem(Guid guid);

    IEnumerable<CdItemModel?> GetItems();
    
    CdItemModel? GetItem(Guid id);
}