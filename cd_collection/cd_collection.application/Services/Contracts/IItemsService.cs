using cd_collection.application.DTO;

namespace cd_collection.application.Services.Contracts;

public interface IItemsService
{
    IList<CdItemDto> GetItems();

    CdItemDto GetItem(Guid id);

    CdItemDto? CreateItem(string artist, string title, string label, DateTime releaseDate);
    
    CdItemDto? UpdateItem(Guid guid, string? artist, string? title, string? label, DateTime? releaseDate);

    bool DeleteItem(Guid guid);
}