using cd_collection.application.DTO;
using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;

namespace cd_collection.application.Extensions;

public static class CollectionConvertingExtension
{
    public static CollectionDto ConvertToDto(this Collection? collection) =>
        new(id: collection.Identifier,
            name: collection.Name,
            itemsIds: collection
                .GetItemsIds()
                .ToGuids());

    public static CdItemDto(this CdItem item)
    {
        return new CdItemDto
        {
            Id = item,
            Artist = null,
            Title = null,
            Label = null,
            ReleaseDate = default
        }
    }
}