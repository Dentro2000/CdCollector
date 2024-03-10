using cd_collection.Models;
using cd_collection.Repositories.Contracts;

namespace cd_collection.Repositories;

public class InMemoryItemsRepository: IInMemoryItemsRepository
{
    private List<CdItemModel?> _items = new List<CdItemModel?>()
    {
        new ("SomeArtist", "SomeTitle", "SomeLabel", DateTime.Now),
        new ("SomeArtist", "SomeTitle", "SomeLabel", DateTime.Now),
    };

    public void AddItem(CdItemModel item)
    {
        throw new NotImplementedException();
    }

    public void DeleteItem(Guid guid)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CdItemModel?> GetItems()
    {
        throw new NotImplementedException();
    }

    public CdItemModel? GetItem(Guid id)
    {
        throw new NotImplementedException();
    }
}