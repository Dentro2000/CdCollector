namespace cd_collection.core.Exceptions;

public abstract class CustomException : Exception
{
    protected CustomException(string massage) : base(massage)
    {
    }
}