namespace cd_collection.ValueObjects;

public sealed record Date
{
    public DateTime Value { get; }

    public Date(DateTime value)
    {
        Value = value;
    }
    
    public static implicit operator DateTime(Date name)
        => name.Value;

    public static implicit operator Date(DateTime value)
        => new(value);
}