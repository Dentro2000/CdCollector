using cd_collection.core.Contracts;
using cd_collection.core.Entities;

namespace cd_collection.infrastructure.Repositories;

internal class InMemoryItemsRepository : IItemsRepository
{
    private readonly ITime _time;

    public InMemoryItemsRepository(ITime time)
    {
        _time = time;
    }

    private List<CdItem?> _items => new List<CdItem?>()
    {
        new("SomeArtist", "SomeTitle", "SomeLabel", _time.Current()),
        new("SomeArtist", "SomeTitle", "SomeLabel", _time.Current()),
    };

    public void AddItem(CdItem item)
    {
        _items.Add(item);
    }

    public bool DeleteItem(Guid guid)
    {
        var itemToRemove = _items.SingleOrDefault(x => x.Identifier.Value == guid);
        if (itemToRemove == null)
        {
            //throw exception
            return false;
        }

        _items.Remove(itemToRemove);
        return true;
    }

    public IEnumerable<CdItem?> GetItems()
    {
        return _items;
    }

    public CdItem? GetItem(Guid id)
    {
        return _items.SingleOrDefault(x => x.Identifier.Value == id);
    }
}