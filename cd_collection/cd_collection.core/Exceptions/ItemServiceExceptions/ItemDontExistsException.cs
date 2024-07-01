namespace cd_collection.core.Exceptions.ItemServiceExceptions;

public class ItemDontExistsException : CustomException
{
    public ItemDontExistsException(Guid guid) : base($"Item of guid: {guid} do not exists")
    {
    }
}