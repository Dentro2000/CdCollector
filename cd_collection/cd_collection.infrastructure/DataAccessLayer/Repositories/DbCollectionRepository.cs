using cd_collection.core.Contracts;
using cd_collection.core.Entities;


namespace cd_collection.infrastructure.DataAccessLayer.Repositories;

internal sealed class DbCollectionRepository: ICollectionRepository
{
    private CDCollectionDbContext _context;
    
    public DbCollectionRepository(CDCollectionDbContext context)
    {
        _context = context;
    }
    
    //TODO: change to async
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
        return _context.Collections;
    }

    public Collection? GetCollection(Guid id)
    {
        return _context.Collections.SingleOrDefault(x => x.Id.Value == id);
    }
    
}