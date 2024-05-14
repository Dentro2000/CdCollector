namespace cd_collection.core.Exceptions.Collection;

public sealed class CantAddItemToCollectionException: CustomException
{
    public CantAddItemToCollectionException() : base($"Cant add item to collection. Collection or item does not exists.")
    {
    }
}