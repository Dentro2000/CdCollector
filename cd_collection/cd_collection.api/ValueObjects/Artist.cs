namespace cd_collection.ValueObjects;

public sealed record Artist
{
    public string Value { get; }

    public Artist(string value)
    {
        Value = value;
    }
    
    public static implicit operator string(Artist name)
        => name.Value;

    public static implicit operator Artist(string value)
        => new(value);
}