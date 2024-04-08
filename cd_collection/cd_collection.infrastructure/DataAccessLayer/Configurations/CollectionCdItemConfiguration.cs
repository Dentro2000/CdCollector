using cd_collection.core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cd_collection.infrastructure.DataAccessLayer.Configurations;

internal sealed class CollectionCdItemConfiguration: IEntityTypeConfiguration<CollectionCdItem>
{
    public void Configure(EntityTypeBuilder<CollectionCdItem> builder)
    {
        builder.HasKey(collectionCdItem => new { collectionCdItem.ItemId, collectionCdItem.CollectionId });

        builder.HasOne(x => x.Collection)
            .WithMany(i => i.CollectionCdItems)
            .HasForeignKey(x => x.CollectionId);
        
        builder.HasOne(x => x.Item)
            .WithMany(i => i.CollectionCdItems)
            .HasForeignKey(x => x.ItemId);
        
    }
}

