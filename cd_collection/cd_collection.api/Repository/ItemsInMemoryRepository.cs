using cd_collection.Models;

namespace cd_collection.Repository;

public class ItemsInMemoryRepository: IItemsRepository
{
    private List<CdItemModel?> _items;
    
    public ItemsInMemoryRepository()
    {
        _items = new List<CdItemModel?>()
        {
            new CdItemModel(Guid.NewGuid(), "SomeArtist", "SomeTitle", "SomeLabel", DateTime.Now),
            new CdItemModel(Guid.NewGuid(), "SomeArtist", "SomeTitle", "SomeLabel", DateTime.Now),
        };
    }

    public List<CdItemModel?> GetItems()
    {
        return _items;
    }

    public CdItemModel GetItem(Guid id)
    {
        return _items.SingleOrDefault(x => x.Id == id);
    }

    public CdItemModel? CreateItem(string artist, string title, string label, DateTime relaseDate)
    {
        var guid = Guid.NewGuid();
        var newItem = new CdItemModel(guid, artist, title, label, relaseDate);
        var ifItemAlreadyExist = _items.SingleOrDefault(x =>
            x.Artist == artist 
            && x.Title == title 
            && x.Label == label 
            && x.ReleaseDate == relaseDate);

        if (ifItemAlreadyExist != null)
        {
            //should throw exception
            return null;
        }

        _items.Add(newItem);
        return _items.Single(x => x.Id == guid);
    }

    public CdItemModel? UpdateItem(Guid guid, string artist, string title, string label, DateTime relaseDate)
    {
        var oldItem = _items.SingleOrDefault(x => x.Id == guid);
        if (oldItem == null)
        {
            return null;
            //throw exception
        }

        var newItem =
            new CdItemModel(oldItem.Id, artist, title, label, relaseDate);

        _items.Remove(oldItem);
        _items.Add(newItem);

        return newItem;
    }

    public void DeleteItem(Guid guid)
    {
        //return bool
        var itemToDelete = _items.SingleOrDefault(x => x.Id == guid);
        if (itemToDelete == null)
        {
            //throw exception
        }
        
        _items.Remove(itemToDelete);
    }
}