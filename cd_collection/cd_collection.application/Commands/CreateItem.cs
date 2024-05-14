namespace cd_collection.application.Commands;

public record CreateItem(string Artist, string Title, string Label, DateOnly ReleaseDate);