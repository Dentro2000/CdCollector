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


    public Collection RemoveItem(Guid itemId)
    {
        if (!CdItems.Select(x => x.Id).ToList().Contains(itemId))
        {
            throw new CannotRemoveItemException(itemId);
        }

        CdItems.Select(x => x.Id).ToList().Remove(itemId);
        SetLastUpdate();
        return this;
    }

    public void SetAllItems(List<CdItemId> items)
    {
       // CollectionCdItems.Select(x => x.ItemId) = items;
       throw new NotImplementedException();
    }

    private void SetLastUpdate() => LastUpdate = DateTime.UtcNow;
}