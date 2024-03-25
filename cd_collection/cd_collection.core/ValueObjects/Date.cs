namespace cd_collection.core.ValueObjects;

public sealed record Date
{
    public DateTime Value { get; }

    public Date(DateTime value)
    {
        Value = value;
    }

    public static implicit operator DateTime(Date date)
        => date.Value;

    public static implicit operator Date(DateTime value)
        => new(value);
}