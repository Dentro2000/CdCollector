using cd_collection.core.Exceptions.Collection;
using cd_collection.core.ValueObjects;

namespace cd_collection.core.Entities;

public class Collection
{
    public Identifier Identifier { get; private set; }
    public CollectionName Name { get; private set; }
    public IEnumerable<Identifier> ItemIdentifiers { get; private set; }
    public Date CreationDate { get; private set; }
    public Date LastUpdate { get; private set; }

    public Collection(CollectionName name)
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
        ItemIdentifiers.ToList().Add(itemId);
        SetLastUpdate();
        return this;
    }

    public IEnumerable<Identifier> GetItemsIds() => ItemIdentifiers;


    public Collection RemoveItem(Guid itemId)
    {
        if (!ItemIdentifiers.ToList().Contains(itemId))
        {
            throw new CannotRemoveItemException(itemId);
        }

        ItemIdentifiers.ToList().Remove(itemId);
        SetLastUpdate();
        return this;
    }

    public void SetAllItems(List<Identifier> items)
    {
        ItemIdentifiers = items;
    }

    private void SetLastUpdate() => LastUpdate = DateTime.Now;
}