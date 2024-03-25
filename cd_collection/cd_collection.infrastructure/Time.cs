using cd_collection.core.Contracts;

namespace cd_collection.Abstractions;

public class Time: ITime
{
    public DateTime Current() => DateTime.UtcNow;

}