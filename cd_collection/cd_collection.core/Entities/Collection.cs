using cd_collection.core.Exceptions.Collection;

namespace cd_collection.core.Entities;

public class Collection
{
    //TODO: Add value objects
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    private List<Guid> ItemsIds { get; set; }
    private DateTime CreationDate { get; set;  }

    //TODO: ADD ABSTRACTION TO DATE TIME
    private DateTime LastUpdate { get;  set; }

    public Collection(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        ItemsIds = new List<Guid>();
        LastUpdate = DateTime.Now;
        CreationDate = DateTime.Now;
    }

    public void ChangeName(string name)
    {
        Name = name;
        SetLastUpdate();
    }

    public Collection AddItem(Guid itemId)
    {
        ItemsIds.Add(itemId);
        SetLastUpdate();
        return this;
    }

    public List<Guid> GetItemsIds() => ItemsIds;


    public Collection RemoveItem(Guid itemId)
    {
        if (!ItemsIds.Contains(itemId))
        {
            throw new CannotRemoveItemException(itemId);
        }

        ItemsIds.Remove(itemId);
        SetLastUpdate();
        return this;
    }

    public void SetAllItems(List<Guid> items)
    {
        ItemsIds = items;
    }

    private void SetLastUpdate() => LastUpdate = DateTime.Now;
}