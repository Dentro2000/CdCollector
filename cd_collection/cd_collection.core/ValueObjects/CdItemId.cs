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