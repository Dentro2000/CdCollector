namespace cd_collection.application.DTO;

public class CdItemDto
{
    public Guid Identifier { get; set; }

    public string Artist { get; set; }

    public string Title { get; set; }

    public string Label { get; set; }

    public DateOnly ReleaseDate { get; set; }
}