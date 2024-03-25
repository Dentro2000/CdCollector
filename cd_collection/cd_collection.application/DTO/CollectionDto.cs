namespace cd_collection.application.DTO;

public class CollectionDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    //MaybeList of items instead of ids 🤔
    public List<Guid> ItemsIds { get; set; }
}