namespace cd_collection.Models;

public record CdItemModel(Guid Id, string Artist, string Title, string Label, DateTime ReleaseDate);