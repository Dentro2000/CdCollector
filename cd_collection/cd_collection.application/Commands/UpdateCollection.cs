namespace cd_collection.application.Commands;

public record UpdateCollection(string? CollectionName, List<Guid> Items);