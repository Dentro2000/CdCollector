namespace cd_collection.core.Exceptions.ItemServiceExceptions;

public class CantUpdateItemException : CustomException
{
    public CantUpdateItemException(Guid guid) : base($"Item of guid: {guid} do not exists")
    {
    }
}