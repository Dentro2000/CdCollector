//system

using cd_collection.DTO;
using cd_collection.Models;
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
    private List<Collection> _collections;

    [SetUp]
    public void Setup()
    {
        _collectionRepositoryMock = new Mock<IInMemoryCollectionRepository>();
        _itemsRepositoryMock = new Mock<IInMemoryItemsRepository>();
        _itemsService = new ItemsService(_itemsRepositoryMock.Object);

        _sut = new CollectionsService(
            collectionsRepository: _collectionRepositoryMock.Object,
            itemsRepository: _itemsRepositoryMock.Object);
        
        _collections = new List<Collection>
        {
            new (name: "OneTwoThree"),
            new (name: "FourFiveSix"),
        };
    }

    [Test]
    public void GetCollectionsTest()
    {
        //given
        _collectionRepositoryMock.Setup(x => x.GetCollections()).Returns(_collections);
        
        //when
        var collectionsDtos = _sut.GetCollections();
        
        //then
        Assert.True(collectionsDtos.ToArray().First().Name == _collections.First().Name);
        Assert.True(typeof(IEnumerable<CollectionDto>) == collectionsDtos.GetType());
    }
    
    [Test]
    public void CreateCollection()
    {
        //given
        _collectionRepositoryMock.Setup(x => x.GetCollections()).Returns(_collections);
        
        //when
        var collectionsDtos = _sut.GetCollections();
        
        //then
        Assert.True(collectionsDtos.ToArray().First().Name == _collections.First().Name);
        
    }
}
