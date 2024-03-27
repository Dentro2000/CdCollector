using cd_collection.core.Exceptions.Collection;
using cd_collection.core.ValueObjects;

namespace cd_collection.core.Entities;

public class Collection
{
    public Identifier Identifier { get; private set; }
    public CollectionName Name { get; private set; }
    private List<Identifier> ItemIdentifiers { get; set; }
    private Date CreationDate { get; set; }
    private Date LastUpdate { get; set; }

    public Collection(string name)
    {
        Identifier = Guid.NewGuid();
        Name = name;
        ItemIdentifiers = new List<Identifier>();
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
        ItemIdentifiers.Add(itemId);
        SetLastUpdate();
        return this;
    }

    public List<Identifier> GetItemsIds() => ItemIdentifiers;


    public Collection RemoveItem(Guid itemId)
    {
        if (!ItemIdentifiers.Contains(itemId))
        {
            throw new CannotRemoveItemException(itemId);
        }

        ItemIdentifiers.Remove(itemId);
        SetLastUpdate();
        return this;
    }

    public void SetAllItems(List<Identifier> items)
    {
        ItemIdentifiers = items;
    }

    private void SetLastUpdate() => LastUpdate = DateTime.Now;
}