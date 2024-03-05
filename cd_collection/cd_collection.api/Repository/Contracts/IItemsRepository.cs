using cd_collection.Models;

namespace cd_collection.Repository;

public interface IItemsRepository
{
    List<CdItemModel?> GetItems();

    CdItemModel GetItem(Guid id);

    CdItemModel? CreateItem(string artist, string title, string label, DateTime relaseDate);
    
    CdItemModel? UpdateItem(Guid guid, string? artist, string? title, string? label, DateTime? relaseDate);

    void DeleteItem(Guid guid);
}