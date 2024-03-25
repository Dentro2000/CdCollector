namespace cd_collection.ValueObjects;

public sealed record Title
{
    public string Value { get; }

    public Title(string value)
    {
        Value = value;
    }
    
    public static implicit operator string(Title title)
        => title.Value;

    public static implicit operator Title(string value)
        => new(value);
}