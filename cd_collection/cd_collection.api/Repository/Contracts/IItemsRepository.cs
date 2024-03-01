using cd_collection.Models;

namespace cd_collection.Repository;

public interface IItemsRepository
{
    List<ItemModel?> GetItems();

    ItemModel GetItem(Guid id);

    ItemModel? CreateItem(string artist, string title, string label, DateTime relaseDate);
    
    ItemModel? UpdateItem(string artist, string title, string label, DateTime relaseDate);

    void DeleteItem(Guid guid);
}