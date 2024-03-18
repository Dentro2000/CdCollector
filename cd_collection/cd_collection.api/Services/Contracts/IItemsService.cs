using cd_collection.DTO;
using cd_collection.Models;

namespace cd_collection.Repository;

public interface IItemsService
{
    IList<CdItemDto> GetItems();

    CdItemDto GetItem(Guid id);

    CdItemDto? CreateItem(string artist, string title, string label, DateTime releaseDate);
    
    CdItemDto? UpdateItem(Guid guid, string? artist, string? title, string? label, DateTime? releaseDate);

    bool DeleteItem(Guid guid);
}