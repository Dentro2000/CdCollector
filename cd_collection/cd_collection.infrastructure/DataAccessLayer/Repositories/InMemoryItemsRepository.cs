using cd_collection.core.Contracts;
using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;

namespace cd_collection.infrastructure.DataAccessLayer.Repositories;

public class InMemoryItemsRepository : IItemsRepository
{
    private readonly ITime _time;

    public InMemoryItemsRepository(ITime time)
    {
        _time = time;
    }

    private List<CdItem?> _items => new List<CdItem?>()
    {
        new(Guid.NewGuid(), "SomeArtist", "SomeTitle", "SomeLabel", _time.CurrentDateOnly()),
        new(Guid.NewGuid(), "SomeArtist", "SomeTitle", "SomeLabel", _time.CurrentDateOnly()),
    };

    public async Task AddItemAsync(CdItem item) => _items.Add(item);


    public Task DeleteItem(CdItemId guid)
    {
        _items.SingleOrDefault(x => x.Id == guid);
        return Task.CompletedTask;
    }

    public IEnumerable<CdItem?> GetItems() => _items;

    public CdItem? GetItem(CdItemId id) => _items.SingleOrDefault(x => x.Id == id);

    public Task UpdateItemAsync(CdItem item)
    {
        throw new NotImplementedException();
    }
}