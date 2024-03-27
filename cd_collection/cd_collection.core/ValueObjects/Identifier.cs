namespace cd_collection.core.ValueObjects;

public sealed record Identifier
{
    public Guid Value { get; }

    public Identifier(Guid value)
    {
        Value = value;
    }

    public static implicit operator Guid(Identifier identifier)
        => identifier.Value;

    public static implicit operator Identifier(Guid value)
        => new(value);
}

public static class IdentifierListExtension
{ 
    public static List<Identifier> ToIdentifiers(this List<Guid> guids) => guids.Select(x => new Identifier(x)).ToList();
    public static List<Guid> ToGuids(this List<Identifier> identifiers) => identifiers.Select(x => x.Value).ToList();
}