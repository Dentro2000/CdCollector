using cd_collection.Models;
using cd_collection.Repositories.Contracts;

namespace cd_collection.Repositories;

public class InMemoryItemsRepository : IInMemoryItemsRepository
{
    private List<CdItemModel?> _items = new List<CdItemModel?>()
    {
        new("SomeArtist", "SomeTitle", "SomeLabel", DateTime.Now),
        new("SomeArtist", "SomeTitle", "SomeLabel", DateTime.Now),
    };

    public void AddItem(CdItemModel item)
    {
        _items.Add(item);
    }

    public bool DeleteItem(Guid guid)
    {
        var itemToRemove = _items.SingleOrDefault(x => x.Id == guid);
        if (itemToRemove == null)
        {
            //throw exception
            return false;
        }

        _items.Remove(itemToRemove);
        return true;
    }

    public IEnumerable<CdItemModel?> GetItems()
    {
        return _items;
    }

    public CdItemModel? GetItem(Guid id)
    {
        return _items.SingleOrDefault(x => x.Id == id);
    }
}