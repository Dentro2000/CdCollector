namespace cd_collection.core.Exceptions.Collection;

public sealed class CannotUpdateException : CustomException
{
    public CannotUpdateException(Guid collectionId) : base(
        $"Can't update collection. Collection {collectionId} don't exists.")
    {
    }
}