//system

using cd_collection.Models;
using cd_collection.Repositories;
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
    private IInMemoryCollectionRepository _collectionRepositoryMock;

    [SetUp]
    public void Setup()
    {
        _collectionRepositoryMock = new MockRepo();
        _itemsRepositoryMock = new Mock<IInMemoryItemsRepository>();
        _itemsService = new ItemsService(_itemsRepositoryMock.Object);

        _sut = new CollectionsService(
            collectionsRepository: _collectionRepositoryMock,
            itemsRepository: _itemsRepositoryMock.Object);
        
    }

    [TearDown]
    public void TearDown()
    {
        _collectionRepositoryMock = null;
        _itemsRepositoryMock = null;
        _itemsService = null;

        _sut = null;
    }

    [Test]
    public void GetCollectionsTest()
    {
        //given
        var newColelction = new Collection("Test");
        _collectionRepositoryMock.AddCollection(newColelction);

        //when
        var collectionsDtos = _sut.GetCollections();

        //then
        Assert.True(collectionsDtos.ToArray().First()?.Name == newColelction.Name);
    }

    [Test]
    public void GetCollectionTests()
    {
        //given
        var newColelction = new Collection("Test");
        _collectionRepositoryMock.AddCollection(newColelction);

        //when
        var collectionDto = _sut.GetCollection(newColelction.Id);

        //then
        Assert.True(collectionDto?.Name == "Test");
    }

    [Test]
    public void CreateCollectionTest()
    {
        //given
        var newColelction = new Collection("Test");
        _collectionRepositoryMock.AddCollection(newColelction);
        
        var collectionName = "NewCollection";

        //when
        _sut.CreateCollection(collectionName);
        
        //then
        Assert.True(_collectionRepositoryMock.GetCollections().ToArray().Length == 2);
    }

    [Test]
    public void UpdateCollection_ShouldReturnNull_If_NoCollections()
    {
        //given
        Assert.True(_collectionRepositoryMock.GetCollections().ToArray().Length == 0);
        
        //when
        var updated = _sut.UpdateCollection(
            Guid.NewGuid(), 
            "Elo", 
            new List<Guid> { Guid.NewGuid() });
    
        //then
        Assert.IsNull(updated);
    }
    
    [Test]
    public void UpdateCollection_ShouldUpdateName_If_NameProvided()
    {
        //given
        var collections = _collectionRepositoryMock.GetCollections();
        
        var newCollection = new Collection("Test");
        _collectionRepositoryMock.AddCollection(newCollection);

        //when
        _sut.UpdateCollection(
            newCollection.Id, 
            "Elo", 
            new List<Guid> { });
    
        //then
        Assert.IsTrue(collections.First().Name == "Elo");
        Assert.IsTrue(collections.First().ItemsIds.Count == 0);
    }
    
    [Test]
    public void UpdateCollection_ShouldReturnUpdateItems_If_IfItemsProvided()
    {
        //given
        var collections = _collectionRepositoryMock.GetCollections();
        var newCollection = new Collection("Test");
        _collectionRepositoryMock.AddCollection(newCollection);

        //when
        var updated = _sut.UpdateCollection(
            newCollection.Id, 
            null, 
            new List<Guid> { Guid.NewGuid(), Guid.NewGuid() });
    
        //then
        Assert.IsTrue(collections.First().ItemsIds.Count == 2);
    }
    
    [Test]
    public void UpdateCollection_ShouldReturnUpdateItemsProperly()
    {

        //given
       _collectionRepositoryMock.GetCollections();
        
        var newCollection = new Collection("Test");
        _collectionRepositoryMock.AddCollection(newCollection);
        
        var cd1 = Guid.NewGuid();
        var cd2 = Guid.NewGuid();
        var cd3 = Guid.NewGuid();
        _sut.UpdateCollection(
            newCollection.Id, 
            null, 
            new List<Guid> { cd1, cd2, });
        
        
        //then
        var updated = _sut.UpdateCollection(
            newCollection.Id, 
            null, 
            new List<Guid> { cd1, cd2, cd3 });
        
        //then
        
        Assert.IsTrue(
            newCollection.ItemsIds.Count == 3);
    }
    
    [Test]
    public void UpdateCollection_ShouldReturnUpdateItemsProperly1()
    {
        //given
        _collectionRepositoryMock.GetCollections();
        
        var newCollection = new Collection("Test");
        _collectionRepositoryMock.AddCollection(newCollection);
        
        var cd1 = Guid.NewGuid();
        var cd2 = Guid.NewGuid();
        var cd3 = Guid.NewGuid();
        
        _sut.UpdateCollection(
            newCollection.Id, 
            null, 
            new List<Guid> { cd1, cd2, });
        
        //then
        var updated = _sut.UpdateCollection(
            newCollection.Id, 
            null, 
            new List<Guid> { cd2, cd3 });
        
        //then
        
        Assert.IsTrue(updated.ItemsIds.Count == 2);
        Assert.IsTrue(updated.ItemsIds.Contains(cd2));
        Assert.IsTrue(updated.ItemsIds.Contains(cd3));
        Assert.IsTrue(!updated.ItemsIds.Contains(cd1));
    }
}

class MockRepo: IInMemoryCollectionRepository
{
   private List<Collection> _collections = new List<Collection> { };


   public void AddCollection(Collection collection)
   {
       _collections.Add(collection);
   }

   public void DeleteCollection(Collection collection)
   {
       _collections.Remove(collection);
   }

   public IEnumerable<Collection?> GetCollections()
   {
       return _collections;
   }

   public Collection? GetCollection(Guid id)
   {
       return _collections.SingleOrDefault(x => x.Id == id);
   }
}