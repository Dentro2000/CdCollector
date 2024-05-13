using cd_collection.application.DTO;
using cd_collection.core.Entities;

namespace cd_collection.application.Extensions;

public static class ToDtoExtensions
{
    public static CollectionDto ConvertToDto(this Collection? collection)
    {
        if (collection?.CdItems != null && !collection.CdItems.Any())
        {
            return new CollectionDto(id: collection.Id.Value,
                name: collection.Name,
                itemsIds: collection.CdItems.Select(x => x.Id.Value).ToList());
        }
        
        return new CollectionDto(id: collection.Id.Value,
            name: collection.Name,
            itemsIds: new List<Guid>());
    }


    public static CdItemDto ConvertToDto(this CdItem? item) =>
        new CdItemDto
        {
            Identifier = item.Id,
            Artist = item.Artist,
            Title = item.Title,
            Label = item.Label,
            ReleaseDate = item.ReleaseDate
        };
}