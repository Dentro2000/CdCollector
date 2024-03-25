using cd_collection.DTO;
using cd_collection.Entities;

namespace cd_collection.Extensions;


    public static class CollectionConvertingExtension
    {
        public static CollectionDto ConvertToDto(this Collection? collection) =>
            new CollectionDto
            {
                Id = collection.Id,
                Name = collection.Name,
                ItemsIds = collection.GetItemsIds()
            };
    }
