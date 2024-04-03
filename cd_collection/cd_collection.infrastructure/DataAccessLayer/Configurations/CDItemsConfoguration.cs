using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cd_collection.infrastructure.DataAccessLayer.Configurations;

public sealed class CDItems : IEntityTypeConfiguration<CdItem>
{
    public void Configure(EntityTypeBuilder<CdItem> builder)
    {
        builder.HasKey(x => x.Identifier);

        builder.Property(x => x.Identifier)
            .HasConversion(x => x.Value, x => new Identifier(x));
        
        builder.Property(x => x.Artist)
            .HasConversion(x => x.Value, x => new Artist(x));
        
        builder.Property(x => x.Title)
            .HasConversion(x => x.Value, x => new Title(x));
        
        builder.Property(x => x.Label)
            .HasConversion(x => x.Value, x => new Label(x));
        
        builder.Property(x => x.ReleaseDate)
            .HasConversion(x => x.Value, x => new Date(x));
        
        builder.Property(x => x.LastUpdate)
            .HasConversion(x => x.Value, x => new Date(x));
            
    }
}