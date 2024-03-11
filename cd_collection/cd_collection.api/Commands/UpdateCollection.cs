namespace cd_collection.Commands;

public record UpdateCollection(string? collectionName, List<Guid> items);