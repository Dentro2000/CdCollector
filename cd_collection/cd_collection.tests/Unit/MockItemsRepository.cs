using cd_collection.Models;
using cd_collection.Repositories.Contracts;

namespace cd_collection.tests;

public class MockItemsRepository: IInMemoryItemsRepository
{
    
    private List<CdItemModel> _items = new List<CdItemModel> { };
    public void AddItem(CdItemModel item)
    {
        _items.Add(item);
    }

    public bool DeleteItem(Guid guid)
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