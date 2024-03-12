//system

using cd_collection.Repositories.Contracts;
using Moq;

//local
using cd_collection.Repository;
using cd_collection.Services;

namespace cd_collection.tests.Services;

public class CollectionServiceTests
{
    private CollectionsService _sut;
    private IItemsService _itemsService;
    private Mock<IInMemoryItemsRepository> _itemsRepositoryMock;
    private Mock<IInMemoryCollectionRepository> _collectionRepositoryMock;

    [SetUp]
    public void Setup()
    {
        _collectionRepositoryMock = new Mock<IInMemoryCollectionRepository>();
        _itemsRepositoryMock = new Mock<IInMemoryItemsRepository>();
        _itemsService = new ItemsService(_itemsRepositoryMock.Object);

        _sut = new CollectionsService(
            collectionsRepository: _collectionRepositoryMock.Object,
            itemsRepository: _itemsRepositoryMock.Object);
    }

    [Test]
    public void GetCollectionTest()
    {
        //given
        
        
        //when
        //then
    }
}