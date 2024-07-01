using cd_collection.core.Contracts;
using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace cd_collection.infrastructure.DataAccessLayer.Repositories;

internal sealed class DbItemsRepository : IItemsRepository
{
    private CdCollectionDbContext _context;

    public DbItemsRepository(CdCollectionDbContext context)
    {
        _context = context;
    }

    public async Task AddItemAsync(CdItem item)
    {
        await _context.CdItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteItem(CdItemId guid)
    {
        var item = _context.CdItems.Single(x => x.Id == guid);
        _context.Remove(item);
        await _context.SaveChangesAsync();
    }

    public IEnumerable<CdItem?> GetItems()
    {
        return _context.CdItems;
    }

    public CdItem? GetItem(CdItemId id) => _context.CdItems.SingleOrDefault(x => x.Id == id);

    public async Task UpdateItemAsync(CdItem item)
    {
        _context.Update(item);
        await _context.SaveChangesAsync();
    }
}