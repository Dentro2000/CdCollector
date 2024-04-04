using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cd_collection.infrastructure.DataAccessLayer.Configurations;

internal sealed class CollectionsConfiguration : IEntityTypeConfiguration<Collection>
{
    public void Configure(EntityTypeBuilder<Collection> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new ColectionIdentfier(x));

        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new CollectionName(x));

        builder.Property(x => x.CreationDate)
            .HasConversion(x => x.Value, x => new Date(x));

        builder.Property(x => x.LastUpdate)
            .HasConversion(x => x.Value, x => new Date(x));
    }
}