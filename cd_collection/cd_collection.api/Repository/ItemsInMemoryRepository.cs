using cd_collection.Models;

namespace cd_collection.Repository;

public class ItemsInMemoryRepository: IItemsRepository
{
    private List<ItemModel?> _items;
    
    public ItemsInMemoryRepository()
    {
        _items = new List<ItemModel?>()
        {
            new ItemModel(Guid.NewGuid(), "SomeArtist", "SomeTitle", "SomeLabel", DateTime.Now),
            new ItemModel(Guid.NewGuid(), "SomeArtist", "SomeTitle", "SomeLabel", DateTime.Now),
        };
    }

    public List<ItemModel?> GetItems()
    {
        return _items;
    }

    public ItemModel GetItem(Guid id)
    {
        return _items.SingleOrDefault(x => x.Id == id);
    }

    public ItemModel? CreateItem(string artist, string title, string label, DateTime relaseDate)
    {
        var guid = Guid.NewGuid();
        var newItem = new ItemModel(guid, artist, title, label, relaseDate);
        var ifItemAlreadyExist = _items.SingleOrDefault(x =>
            x.Artist == artist 
            && x.Title == title 
            && x.Label == label 
            && x.ReleaseDate == relaseDate);

        if (ifItemAlreadyExist == null)
        {
            //should throw exception
            return null;
        }

        _items.Add(newItem);
        return _items.Single(x => x.Id == guid);
    }

    public ItemModel? UpdateItem(string artist, string title, string label, DateTime relaseDate)
    {
        throw new NotImplementedException();
    }

    public void DeleteItem(Guid guid)
    {
        throw new NotImplementedException();
    }
}