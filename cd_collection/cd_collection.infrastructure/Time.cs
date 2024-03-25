using cd_collection.core.Contracts;

namespace cd_collection.infrastructure;

public class Time: ITime
{
    public DateTime Current() => DateTime.UtcNow;

}