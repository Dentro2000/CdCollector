//system

using cd_collection.application.Services;
using cd_collection.core.Contracts;
using cd_collection.core.Entities;
using cd_collection.core.Exceptions.Collection;


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
    public void UpdateCollection_ShouldReturnException_If_NoCollections()
    {
        //given
        Assert.True(_collectionRepositoryMock.GetCollections().ToArray().Length == 0);

        var cd2 = new CdItem("666", "9", "3", new DateOnly(2024, 05, 1));
        _itemsRepositoryMock.AddItem(cd2);

        //when
        //then
        Assert.Throws<CannotUpdateException>(() => _sut.UpdateCollection(
            Guid.NewGuid(),
            "Elo",
            new List<Guid> { cd2.Id }));
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
        Assert.IsTrue(collections.First().CdItems.ToList().Count == 0);
    }

    [Test]
    public void UpdateCollection_ShouldReturnUpdateItems_If_IfItemsProvided()
    {
        //given
        var collections = _collectionRepositoryMock.GetCollections();
        var newCollection = new Collection("Test");
        _collectionRepositoryMock.AddCollection(newCollection);

        var cd1 = new CdItem("2", "9", "3", new DateOnly(2024, 05, 1));
        var cd2 = new CdItem("666", "9", "3", new DateOnly(2024, 05, 1));
        _itemsRepositoryMock.AddItem(cd1);
        _itemsRepositoryMock.AddItem(cd2);


        //when
        var updated = _sut.UpdateCollection(
            newCollection.Id,
            null,
            new List<Guid> { cd1.Id, cd2.Id });

        //then
        Assert.IsTrue(collections.First().CdItems.ToList().Count == 2);
    }

    [Test]
    public void UpdateCollection_ShouldAddItem()
    {
        //given
        _collectionRepositoryMock.GetCollections();

        var newCollection = new Collection("Test");
        _collectionRepositoryMock.AddCollection(newCollection);

        var cd1 = new CdItem("1", "6", "3", new DateOnly(2024, 05, 1));
        var cd2 = new CdItem("2", "8", "3", new DateOnly(2024, 05, 1));
        var cd3 = new CdItem("2", "9", "3", new DateOnly(2024, 05, 1));
        _itemsRepositoryMock.AddItem(cd1);
        _itemsRepositoryMock.AddItem(cd2);
        _itemsRepositoryMock.AddItem(cd3);

        _sut.UpdateCollection(
            newCollection.Id,
            null,
            new List<Guid> { cd1.Id, cd2.Id, });


        //then
        var updated = _sut.UpdateCollection(
            newCollection.Id,
            null,
            new List<Guid> { cd1.Id, cd2.Id, cd3.Id });

        //then
        Assert.IsTrue(
            updated.ItemsIds.Count == 3);
    }

    [Test]
    public void UpdateCollection_ShouldRemoveItem()
    {
        //given
        var newCollection = new Collection("Test");
        _collectionRepositoryMock.AddCollection(newCollection);

        var cd1 = new CdItem("1", "6", "3", new DateOnly(2024, 05, 1));
        var cd2 = new CdItem("2", "8", "3", new DateOnly(2024, 05, 1));
        var cd3 = new CdItem("2", "9", "3", new DateOnly(2024, 05, 1));

        _itemsRepositoryMock.AddItem(cd1);
        _itemsRepositoryMock.AddItem(cd2);
        _itemsRepositoryMock.AddItem(cd3);

        _sut.UpdateCollection(
            newCollection.Id,
            null,
            new List<Guid> { cd1.Id, cd2.Id, });

        //then
        var updated = _sut.UpdateCollection(
            newCollection.Id,
            null,
            new List<Guid> { cd2.Id, cd3.Id });

        //then
        Assert.IsTrue(updated.ItemsIds.Count == 2);
        Assert.IsTrue(updated.ItemsIds.Contains(cd2.Id));
        Assert.IsTrue(updated.ItemsIds.Contains(cd3.Id));
        Assert.IsTrue(!updated.ItemsIds.Contains(cd1.Id));
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
        Assert.True(collection.ItemsIds.First() == mockItem.Id.Value);
    }

    [Test]
    public void AddItemToCollection_Should_ReturnException_IfNoItem()
    {
        //given
        var newCollection = new Collection("Test");

        _collectionRepositoryMock.AddCollection(newCollection);

        //when
        //then
        Assert.Throws<CantAddItemToCollectionException>(() =>
            _sut.AddItemToCollection(Guid.NewGuid(), newCollection.Id));
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

        Assert.True(newCollection.CdItems.First() == mockItem);

        //when
        var collectionWithoutItem = _sut.RemoveItemFromCollection(mockItem.Id, newCollection.Id);

        //then
        Assert.True(collectionWithoutItem.ItemsIds.Count == 0);
    }
}