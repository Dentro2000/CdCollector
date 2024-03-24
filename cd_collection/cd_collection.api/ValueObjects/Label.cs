namespace cd_collection.ValueObjects;

public sealed record Label
{
    public string Value { get; }

    public Label(string value)
    {
        Value = value;
    }
    
    public static implicit operator string(Label name)
        => name.Value;

    public static implicit operator Label(string value)
        => new(value);
}