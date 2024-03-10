using cd_collection.Models;

namespace cd_collection.Repository;

public interface IItemsService
{
    IList<CdItemModel?> GetItems();

    CdItemModel GetItem(Guid id);

    CdItemModel? CreateItem(string artist, string title, string label, DateTime releaseDate);
    
    CdItemModel? UpdateItem(Guid guid, string? artist, string? title, string? label, DateTime? releaseDate);

    void DeleteItem(Guid guid);
}