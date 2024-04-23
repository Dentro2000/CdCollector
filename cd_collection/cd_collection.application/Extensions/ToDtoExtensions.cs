using cd_collection.application.DTO;
using cd_collection.core.Entities;
using cd_collection.core.ValueObjects;

namespace cd_collection.application.Extensions;

public static class ToDtoExtensions
{
    public static CollectionDto ConvertToDto(this Collection? collection) =>
        new(id: collection.Id,
            name: collection.Name,
            itemsIds: collection.CdItems.Select(x => x.Id.Value).ToList());


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