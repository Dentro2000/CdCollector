using cd_collection.Repositories.Contracts;
using cd_collection.Repository;
using cd_collection.Services;

namespace cd_collection.tests.Unit.Services;

public class ItemsServiceTests
{
    private IItemsService _sut;
    private IInMemoryItemsRepository _mockItemsRepository;

    [SetUp]
    public void SetUp()
    {
        _mockItemsRepository = new MockItemsRepository();
        _sut = new ItemsService(_mockItemsRepository);
    }

    [TearDown]
    public void TearDown()
    {
        _mockItemsRepository = null;
        _sut = null;
    }

    [Test]
    public void GetItems_Should_ReturnItemDtos()
    {
        
    }

    [Test]
    public void GetItem_Should_ReturnSingleItem()
    {
        
    }

    [Test]
    public void CreateItem_Should_CreateNewItem()
    {
        
    }


    [Test]
    public void CreateItem_Should_ReturnExceptionIfItemAlreadyExists()
    {
        
    }
   
    [Test]
    public void UpdateItem_Should_UpdateOnlyNonNullProperties()
    {
        
    }
    
    [Test]
    public void DeleteItem_Should_DeleteItem()
    {
        
    }
}