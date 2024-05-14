namespace cd_collection.core.Exceptions.ItemServiceExceptions;

public class ItemAlreadyExistsException : CustomException
{
    public ItemAlreadyExistsException(string title, string artist, string label, DateOnly releaseDate)
        : base($"Item {title} by {artist} released in {releaseDate} by {label} label already exists ")
    {
    }
}