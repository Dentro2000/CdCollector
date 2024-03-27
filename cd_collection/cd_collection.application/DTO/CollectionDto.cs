namespace cd_collection.application.DTO;

public class CollectionDto
{
    public CollectionDto()
    {
    }

    public CollectionDto(Guid id, string name, List<Guid> itemsIds)
    {
        Id = id;
        Name = name;
        ItemsIds = itemsIds;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    //MaybeList of items instead of ids 🤔
    public List<Guid> ItemsIds { get; set; }
}