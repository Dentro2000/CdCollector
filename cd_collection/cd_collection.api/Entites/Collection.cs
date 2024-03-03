namespace cd_collection.Models;

public record Collection(Guid Id, string Name, List<Guid> ItemsIds, DateTime LastUpdate);

