using cd_collection.core.Entities;

namespace cd_collection.core.Exceptions.ItemServiceExceptions;

public class ItemAlreadyExistsException : CustomException
{
    public ItemAlreadyExistsException(CdItem item)
        : base($"Item {item.Title} by {item.Artist} released in {item.ReleaseDate} by {item.Label} label already exists ")
    {
    }
}