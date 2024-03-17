using cd_collection.DTO;

namespace cd_collection.Models;

public class Collection
{
    public Guid Id { get; private set;  } 
    public string Name { get; private set; }
    public List<Guid> ItemsIds { get; private set; }

    public DateTime CreationDate { get; private set;  }

    //TODO: ADD ABSTRACTION TO DATE TIME
    public DateTime LastUpdate { get; private set; }

    public Collection(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        ItemsIds = new List<Guid>();
        LastUpdate = DateTime.Now;
        CreationDate = DateTime.Now;
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

    public void SetAllItems(List<Guid> items)
    {
        ItemsIds = items;
    }

    private void SetLastUpdate() => LastUpdate = DateTime.Now;
}


public static class CollectionConvertingExtension
{

    public static CollectionDto ConvertToDto(this Collection? collection) =>
        new CollectionDto
        {
            Id = collection.Id,
            Name = collection.Name,
            ItemsIds = collection.ItemsIds
        };

}