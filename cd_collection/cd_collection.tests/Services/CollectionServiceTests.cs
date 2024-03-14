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
        Assert.True(collectionsDtos.ToArray().First()?.Name == _collections.First().Name);
    }
    
    [Test]
    public void GetCollectionTests()
    {
        //given
        var collection = _collections.First();
        
        _collectionRepositoryMock.Setup(x => 
            x.GetCollection(collection.Id))
            .Returns(collection);
        
        //when
        var collectionDto = _sut.GetCollection(collection.Id);
        
        //then
        Assert.True(collectionDto?.Name == _collections.First().Name);
    }
    
    [Test]
    public void CreateCollectionTest()
    {
        //given
        var collectionName = "NewCollection";

        _collectionRepositoryMock.Setup(
                x => x.AddCollection(It.IsAny<Collection>()))
            .Callback(() =>
            {
                _collections.Add(new Collection(collectionName));
            });
        
        _collectionRepositoryMock.Setup(x => 
                x.GetCollection(It.IsAny<Guid>()))
            .Returns(_collections.Last());
        
        //when
        var collectionDto = _sut.CreateCollection(collectionName);
        
        //then
        Assert.True(collectionDto?.Name == _collections.Last().Name);
    }
    
}
