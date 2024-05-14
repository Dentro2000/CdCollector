using cd_collection.core.Exceptions.Collection;
using cd_collection.core.ValueObjects;

namespace cd_collection.core.Entities;

public class Collection
{
    public ColectionIdentfier Id { get; private set; }
    public CollectionName Name { get; private set; }
    public Date CreationDate { get; private set; }
    public Date LastUpdate { get; private set; }

    public List<CdItem> CdItems { get; private set; }

    public Collection(CollectionName name)
    {
        Id = Guid.NewGuid();
        Name = name;
        LastUpdate = DateTime.UtcNow;
        CreationDate = DateTime.UtcNow;
        CdItems = new List<CdItem>() { };
    }

    public void ChangeName(string name)
    {
        Name = name;
        SetLastUpdate();
    }

    public Collection AddItem(CdItem item)
    {
        CdItems.Add(item);
        SetLastUpdate();
        return this;
    }

    // public IEnumerable<CdItemId?> GetItemsIds() => CollectionCdItems.Select(x => x.ItemId);


    public Collection RemoveItem(CdItem item)
    {
        CdItems.Remove(item);
        if (!CdItems.Select(x => x.Id).ToList().Contains(item.Id.Value))
        {
            throw new CannotRemoveItemException(item.Id);
        }

        SetLastUpdate();
        return this;
    }

    public Collection SetAllItems(List<CdItem> items)
    {
        CdItems.AddRange(items);
        SetLastUpdate();
        return this;
    }

    private void SetLastUpdate() => LastUpdate = DateTime.UtcNow;
}