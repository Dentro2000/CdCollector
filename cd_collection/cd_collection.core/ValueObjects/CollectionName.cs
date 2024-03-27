namespace cd_collection.core.ValueObjects;

public sealed record CollectionName
{
    public string Value { get; }

    public CollectionName(string value)
    {
        Value = value;
    }

    public static implicit operator string(CollectionName collectionName)
        => collectionName.Value;

    public static implicit operator CollectionName(string value)
        => new(value);
}