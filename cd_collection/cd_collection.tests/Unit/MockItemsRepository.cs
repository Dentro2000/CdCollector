using cd_collection.core.Contracts;
using cd_collection.core.Entities;

namespace cd_collection.tests;

public class MockItemsRepository : IItemsRepository
{
    private List<CdItem> _items = new List<CdItem> { };

    public void AddItem(CdItem item)
    {
        _items.Add(item);
    }

    public bool DeleteItem(Guid guid)
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

    public CdItem? GetItem(Guid id)
    {
        return _items.SingleOrDefault(x => x.Identifier.Value == id);
    }
}

public static class MockItem
{
    public static CdItem MockCdItem =>
        new(
            "MockArtist",
            "MockTitle",
            "MockLabel",
            new DateTime(2024, 01, 01));
}