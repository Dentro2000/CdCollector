using cd_collection.application.DTO;
using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;

namespace cd_collection.application.Extensions;

public static class ToDtoExtensions
{
    public static CollectionDto ConvertToDto(this Collection? collection) =>
        new(id: collection.Identifier,
            name: collection.Name,
            itemsIds: collection
                .GetItemsIds()
                .ToGuids());

    public static CdItemDto ConvertToDto(this CdItem? item) =>
        new CdItemDto
        {
            Identifier = item.Identifier,
            Artist = item.Artist,
            Title = item.Title,
            Label = item.Label,
            ReleaseDate = item.ReleaseDate
        };
}