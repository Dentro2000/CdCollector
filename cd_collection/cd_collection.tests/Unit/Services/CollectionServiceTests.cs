//system

using cd_collection.core.Contracts;
using cd_collection.Entities;
using cd_collection.Models;
using cd_collection.Repository;
using cd_collection.Services;


//local
namespace cd_collection.tests.Unit.Services;

public class CollectionServiceTests
{
    private CollectionsService _sut;
    private IItemsRepository _itemsRepositoryMock;
    private ICollectionRepository _collectionRepositoryMock;

    [SetUp]
    public void Setup()
    {
        _collectionRepositoryMock = new MockCollectionRepository();
        _itemsRepositoryMock = new MockItemsRepository();

        _sut = new CollectionsService(
            collectionsRepository: _collectionRepositoryMock,
            itemsRepository: _itemsRepositoryMock);
    }

    [TearDown]
    public void TearDown()
    {
        _collectionRepositoryMock = null;
        _itemsRepositoryMock = null;

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
        Assert.True(collectionsDtos.First()?.Name == newColelction.Name);
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
        Assert.True(_collectionRepositoryMock.GetCollections().Count() == 2);
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
        Assert.IsTrue(collections.First().GetItemsIds().Count == 0);
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
        Assert.IsTrue(collections.First().GetItemsIds().Count == 2);
    }

    [Test]
    public void UpdateCollection_ShouldAddItem()
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
            newCollection.GetItemsIds().Count == 3);
    }

    [Test]
    public void UpdateCollection_ShouldRemoveItem()
    {
        //given
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

    [Test]
    public void DeleteCollection_Should_DeleteCollection()
    {
        //given
        var newCollection = new Collection("Test");
        _collectionRepositoryMock.AddCollection(newCollection);

        //when
        var isDeleted = _sut.DeleteCollection(newCollection.Id);

        //then
        Assert.True(isDeleted);
    }

    [Test]
    public void DeleteCollection_Should_ReturnFalseIfNoCollection()
    {
        //given
        //when
        var isDeleted = _sut.DeleteCollection(Guid.NewGuid());

        //then
        Assert.False(isDeleted);
    }

    [Test]
    public void AddItemToCollection_Should_AddItemToCollection()
    {
        //given
        var newCollection = new Collection("Test");
        var mockItem = MockItem.MockCdItem;

        _collectionRepositoryMock.AddCollection(newCollection);
        _itemsRepositoryMock.AddItem(mockItem);

        //when
        var collection = _sut.AddItemToCollection(mockItem.Id, newCollection.Id);

        //then
        Assert.True(collection.ItemsIds.First() == mockItem.Id);
    }

    [Test]
    public void AddItemToCollection_Should_ReturnNullIfNoItem()
    {
        //given
        var newCollection = new Collection("Test");

        _collectionRepositoryMock.AddCollection(newCollection);

        //when
        var collection = _sut.AddItemToCollection(Guid.NewGuid(), newCollection.Id);

        //then
        Assert.Null(collection);
    }

    [Test]
    public void RemoveItemFromCollection_Should_ReturnNullIfNoItem()
    {
        //given
        var newCollection = new Collection("Test");
        _collectionRepositoryMock.AddCollection(newCollection);

        //when
        var collectionWithoutItem = _sut.RemoveItemFromCollection(Guid.NewGuid(), newCollection.Id);

        //then
        Assert.Null(collectionWithoutItem);
    }

    [Test]
    public void RemoveItemFromCollection_Should_ReturnCollection()
    {
        //given
        var newCollection = new Collection("Test");
        var mockItem = MockItem.MockCdItem;

        _collectionRepositoryMock.AddCollection(newCollection);
        _itemsRepositoryMock.AddItem(mockItem);
        _sut.AddItemToCollection(mockItem.Id, newCollection.Id);

        Assert.True(newCollection.GetItemsIds().First() == mockItem.Id);

        //when
        var collectionWithoutItem = _sut.RemoveItemFromCollection(mockItem.Id, newCollection.Id);

        //then
        Assert.True(collectionWithoutItem.ItemsIds.Count == 0);
    }
}