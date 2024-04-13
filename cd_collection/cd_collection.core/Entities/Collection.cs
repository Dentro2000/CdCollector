using cd_collection.core.Exceptions.Collection;
using cd_collection.core.ValueObjects;

namespace cd_collection.core.Entities;

public class Collection
{
    public ColectionIdentfier Id { get; private set; }
    public CollectionName Name { get; private set; }
    public Date CreationDate { get; private set; }
    public Date LastUpdate { get; private set; }

    public ICollection<CollectionCdItem> CollectionCdItems { get; set; }


    public List<CdItem> Items { get; private set; }

    public Collection(CollectionName name)
    {
        Id = Guid.NewGuid();
        Name = name;
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
        var coll = new CollectionCdItem();
        CollectionCdItems.Add(coll);
        SetLastUpdate();
        return this;
    }

    public IEnumerable<CdItemId> GetItemsIds() => CollectionCdItems.Select(x => x.ItemId);


    public Collection RemoveItem(Guid itemId)
    {
        if (!CollectionCdItems.Select(x => x.ItemId).ToList().Contains(itemId))
        {
            throw new CannotRemoveItemException(itemId);
        }

        CollectionCdItems.Select(x => x.ItemId).ToList().Remove(itemId);
        SetLastUpdate();
        return this;
    }

    public void SetAllItems(List<CdItemId> items)
    {
        // CollectionCdItems.Select(x => x.ItemId) = items;
    }

    private void SetLastUpdate() => LastUpdate = DateTime.Now;
}