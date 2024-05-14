using cd_collection.application.DTO;

namespace cd_collection.application.Services.Contracts;

public interface IItemsService
{
    IList<CdItemDto?> GetItems();

    CdItemDto GetItem(Guid id);

    CdItemDto? CreateItem(string artist, string title, string label, DateOnly releaseDate);

    void UpdateItem(Guid guid, string? artist, string? title, string? label, DateOnly? releaseDate);

    bool DeleteItem(Guid guid);
}