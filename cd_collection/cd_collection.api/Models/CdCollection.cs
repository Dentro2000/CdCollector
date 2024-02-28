namespace cd_collection.Models;

public record CdCollection(Guid CollectionId, string Name, List<Guid> CdIds, DateTime LastUpdate);

