using cd_collection.core.Contracts;
using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace cd_collection.infrastructure.DataAccessLayer.Repositories;

internal sealed class DbItemsRepository : IItemsRepository
{
    private CDCollectionDbContext _context;

    public DbItemsRepository(CDCollectionDbContext context)
    {
        _context = context;
    }

    public void AddItem(CdItem item)
    {
        _context.CdItems.Add(item);
        _context.SaveChanges();
    }

    public bool DeleteItem(CdItemId guid)
    {
        var item = _context.CdItems.Single(x => x.Id == guid);
        _context.Remove(item);
        _context.SaveChanges();
        //TODO: should not return
        return true;
    }

    public IEnumerable<CdItem?> GetItems()
    {
        return _context.CdItems;
    }

    public CdItem? GetItem(CdItemId id)
    {
        return _context.CdItems.SingleOrDefault(x => x.Id == id);
    }

    public void UpdateItem(CdItem item)
    {
        _context.Update(item);
        _context.SaveChanges();
    }
}