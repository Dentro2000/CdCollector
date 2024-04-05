using cd_collection.core.Exceptions;

namespace cd_collection.core.ValueObjects;

public sealed record CdItemId
{
    public Guid Value { get; private set; }
    
    
    public CdItemId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new NotValidIdentifierException();
        }

        Value = value;
    }

    public static implicit operator Guid(CdItemId cdItemId)
        => cdItemId.Value;

    public static implicit operator CdItemId(Guid value)
        => new(value);
}

public static class IdentifierListExtension
{ 
    public static List<CdItemId> ToIdentifiers(this List<Guid> guids) => guids.Select(x => new CdItemId(x)).ToList();
    public static List<Guid> ToGuids(this List<CdItemId> identifiers) => identifiers.Select(x => x.Value).ToList();
}