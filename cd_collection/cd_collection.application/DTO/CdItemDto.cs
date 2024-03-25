namespace cd_collection.DTO;

public class CdItemDto
{
    public Guid Id { get; set; }

    public string Artist { get; set; }

    public string Title { get; set; }

    public string Label { get; set; }

    public DateTime ReleaseDate { get; set; }
}