namespace cd_collection.core.ValueObjects;

public class ReleaseDate
{
    public DateOnly Value { get; }

    public ReleaseDate(DateOnly value)
    {
        Value = value;
    }

    public static implicit operator DateOnly(ReleaseDate date)
        => date.Value;

    public static implicit operator ReleaseDate(DateOnly value)
        => new(value);
}