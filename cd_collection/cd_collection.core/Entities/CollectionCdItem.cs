using cd_collection.core.ValueObjects;

namespace cd_collection.core.Entities;

public class CollectionCdItem
{
    public CdItemId ItemId { get; set; }
    public CdItem Item { get; set; }
    public ColectionIdentfier CollectionId { get; set; }
    public Collection Collection { get; set; }
}