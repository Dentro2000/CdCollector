using cd_collection.core.Entities;
using Microsoft.EntityFrameworkCore;

namespace cd_collection.infrastructure.DataAccessLayer;

internal sealed class CDCollectionDbContext : DbContext
{
    public DbSet<Collection> Collections { get; set; }
    public DbSet<CdItem> CdItems { get; set; }

    protected CDCollectionDbContext(DbContextOptions<CDCollectionDbContext> options) : base(options)
    {
        
     
    }
}