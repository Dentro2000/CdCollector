using cd_collection.Models;
using cd_collection.Repositories.Contracts;

namespace cd_collection.tests;

public class MockItemsRepository : IInMemoryItemsRepository
{
    private List<CdItemModel> _items = new List<CdItemModel> { };

    public void AddItem(CdItemModel item)
    {
        _items.Add(item);
    }

    public bool DeleteItem(Guid guid)
    {
        throw new NotImplementedException();
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

public static class MockItem
{
    public static CdItemModel MockCdItem =>
        new(
            "MockArtist",
            "MockTitle",
            "MockLabel",
            new DateTime(2024, 01, 01));
}