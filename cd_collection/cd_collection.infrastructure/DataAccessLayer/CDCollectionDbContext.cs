using cd_collection.core.Entities;
using cd_collection.infrastructure.DataAccessLayer.Configurations;
using Microsoft.EntityFrameworkCore;

namespace cd_collection.infrastructure.DataAccessLayer;

internal sealed class CdCollectionDbContext : DbContext
{
    public DbSet<Collection> Collections { get; set; }
    public DbSet<CdItem> CdItems { get; set; }

    public CdCollectionDbContext(DbContextOptions<CdCollectionDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        // modelBuilder.ApplyConfiguration(new CdItemsConfiguration());
        // modelBuilder.ApplyConfiguration(new CollectionsConfiguration());
        // modelBuilder.ApplyConfiguration(new CollectionCdItemConfiguration());
    }
}