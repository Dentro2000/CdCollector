namespace cd_collection.Models;

public class Collection
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public List<Guid> ItemsIds { get; }
    
    //TODO: ADD ABSTRACTION
    public DateTime LastUpdate { get; private set; }

    public Collection(string name, List<Guid> itemsIds)
    {
        Id = Guid.NewGuid();
        Name = name;
        ItemsIds = itemsIds;
        LastUpdate = DateTime.Now;
    }

    public string? ChangeName(string name)
    {
        Name = name;
        SetLastUpdate();
        return Name;
    }

    public List<Guid>? AddItem(Guid itemId)
    {
        if (ItemsIds.Contains(itemId))
        {
             //TODO: throw exception
            return null;
        }

        ItemsIds.Add(itemId);
        SetLastUpdate();
        return ItemsIds;
    }
    
    public List<Guid>? RemoveItem(Guid itemId)
    {
        if (!ItemsIds.Contains(itemId))
        {
            //TODO: throw exception
            return null;
        }

        ItemsIds.Remove(itemId);
        SetLastUpdate();
        return ItemsIds;
    }

    private void SetLastUpdate() => LastUpdate = DateTime.Now;
}