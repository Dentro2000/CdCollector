using cd_collection.core.Contracts;
using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;

namespace cd_collection.tests;

public class MockItemsRepository : IItemsRepository
{
    private List<CdItem> _items = new List<CdItem> { };

    public void AddItem(CdItem item)
    {
        _items.Add(item);
    }
    
    public bool DeleteItem(CdItemId guid)
    {
        var item = GetItem(guid);
        if (item != null)
        {
            _items.Remove(item);
            return true;
        }

        return false;
    }

    public IEnumerable<CdItem?> GetItems()
    {
        return _items;
    }

    public void UpdateItem(CdItem item)
    {
        var z = _items.Single(x => x.Id == item.Id);
        _items.Remove(z);
        _items.Add(item);
    }

    public CdItem? GetItem(CdItemId id)
    {
        return _items.SingleOrDefault(x => x.Id == id);
    }
}

public static class MockItem
{
    public static CdItem MockCdItem =>
        new(
            "MockArtist",
            "MockTitle",
            "MockLabel",
            new DateOnly(2024, 01, 01)
        );
}