using cd_collection.core.Contracts;
using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;
using Microsoft.EntityFrameworkCore;


namespace cd_collection.infrastructure.DataAccessLayer.Repositories;

internal sealed class DbCollectionRepository : ICollectionRepository
{
    private CDCollectionDbContext _context;

    public DbCollectionRepository(CDCollectionDbContext context)
    {
        _context = context;
    }
    
    public void AddCollection(Collection collection)
    {
        _context.Collections.Add(collection);
        _context.SaveChanges();
    }

    public void DeleteCollection(Collection collection)
    {
        _context.Collections.Remove(collection);
        _context.SaveChanges();
    }

    public IEnumerable<Collection?> GetCollections()
    {
        return _context.Collections.Include(x=>x.CdItems);
    }

    public Collection? GetCollection(ColectionIdentfier id)
    {
        return _context
            .Collections
            .Include(x => x.CdItems)
            .SingleOrDefault(x => x.Id == id);
    }

    public void UpdateCollection(Collection collection)
    {
        _context.Update(collection);
        _context.SaveChanges(); 
    }
}