using cd_collection.core.Entities;

namespace cd_collection.core.Exceptions.ItemServiceExceptions;

public class ItemAlreadyExistsException : CustomException
{
    public ItemAlreadyExistsException(CdItem item)
        : base($"Item {item.Title.Value} by {item.Artist.Value} released in {item.ReleaseDate.Value} by {item.Label.Value} label already exists ")
    {
    }
}