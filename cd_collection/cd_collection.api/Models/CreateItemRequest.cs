namespace cd_collection.Models;

public record CreateItemRequest(string Artist, string Title, string Label, DateOnly ReleaseDate);
