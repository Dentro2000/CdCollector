namespace cd_collection.core.Exceptions.Collection;

public sealed class CannotRemoveItemException: CustomException
{
    public CannotRemoveItemException(Guid itemGuid) : base($"Can't remove item. Item with identifier:{itemGuid} don't exist.")
    {
    }
}