namespace cd_collection.Exceptions;

public abstract class CustomException : Exception
{
    protected CustomException(string massage) : base(massage)
    {
    }
}