using cd_collection.core.Exceptions;

namespace cd_collection.core.ValueObjects;

public record ColectionIdentfier
{
    public Guid Value { get; }

    public ColectionIdentfier(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new NotValidIdentifierException();
        }

        Value = value;
    }

    public static implicit operator Guid(ColectionIdentfier identifier)
        => identifier.Value;

    public static implicit operator ColectionIdentfier(Guid value)
        => new(value);
}