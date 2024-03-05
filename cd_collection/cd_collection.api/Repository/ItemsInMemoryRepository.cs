using cd_collection.Models;

namespace cd_collection.Repository;

public class ItemsInMemoryRepository: IItemsRepository
{
    private List<CdItemModel?> _items;
    
    public ItemsInMemoryRepository()
    {
        _items = new List<CdItemModel?>()
        {
            new CdItemModel("SomeArtist", "SomeTitle", "SomeLabel", DateTime.Now),
            new CdItemModel( "SomeArtist", "SomeTitle", "SomeLabel", DateTime.Now),
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
        var newItem = new CdItemModel(artist, title, label, relaseDate);
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
        return _items.Single(x => x.Id == newItem.Id);
    }

    public CdItemModel? UpdateItem(Guid guid, string? artist, string? title, string? label, DateTime? relaseDate)
    {
        
        var item = _items.SingleOrDefault(x => x.Id == guid);
        
        if (item == null)
        {
            return null;
            //throw exception
        }

        if (!string.IsNullOrEmpty(artist))
        {
            item.ChangeArtist(artist);
        }

        if (!string.IsNullOrEmpty(title))
        {
         item.ChangeTitle(title);   
        }
        
        if (!string.IsNullOrEmpty(label))
        {
            item.ChangeLabel(label);
        }
        
        if (relaseDate != null)
        {
            item.ChangeReleaseDate(relaseDate.Value);
        }

        return item;
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