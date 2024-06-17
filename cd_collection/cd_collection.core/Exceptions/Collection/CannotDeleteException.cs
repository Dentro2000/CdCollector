namespace cd_collection.core.Exceptions;

public sealed class CannotDeleteException : CustomException
{
    public CannotDeleteException(Guid collectionId) : base(
        $"Can't delete collection. Collection of id: {collectionId} dont exists")
    {
    }
}