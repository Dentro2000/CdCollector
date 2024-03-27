using cd_collection.core.Entities;
using cd_collection.core.Exceptions.Collection;
using cd_collection.core.ValueObjects;

namespace cd_collection.tests.Unit.Entities;

public class CollectionEntitiesTests
{
    private Collection _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new Collection("MyCollection");
    }

    [TearDown]
    public void TearDown()
    {
    }

    [Test]
    public void ChangeCollectionName_ShouldChangeName()
    {
        //given
        var newName = "NewCollectionName";

        //when
        _sut.ChangeName(newName);

        //then
        Assert.IsTrue(_sut.Name == newName);
    }

    [Test]
    public void RemoveItem_ShouldRemoveItem()
    {
        //given
        var newItemGuid = Guid.NewGuid();
        _sut.AddItem(newItemGuid);
        Assert.IsTrue(_sut.GetItemsIds().Count == 1);

        //when
        _sut.RemoveItem(newItemGuid);

        //then
        Assert.IsTrue(_sut.GetItemsIds().Count == 0);
    }

    [Test]
    public void RemoveItem_ShouldRemoveItem_ShouldThrowException_If_NoItemID()
    {
        //given
        //when
        //then
        Assert.Throws<CannotRemoveItemException>(() =>
            _sut.RemoveItem(Guid.NewGuid()));
    }

    [Test]
    public void AddItem_ShouldAddItem()
    {
        //given
        var itemId = Guid.NewGuid();
        //when
        var collection = _sut.AddItem(itemId);
        //then
        Assert.IsTrue(collection.GetItemsIds().First().Value == itemId);
    }

    [Test]
    public void GetItemsIds_ShouldGetItemsIds()
    {
        //given
        var newItemGuid = Guid.NewGuid();
        _sut.AddItem(newItemGuid);

        //when
        var items = _sut.GetItemsIds();

        //then
        Assert.IsTrue(items.First().Value == newItemGuid);
    }

    [Test]
    public void SetAllItems_ShouldSetAllItems()
    {
        //given
        var items = new List<Guid>
        {
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
        };
        //when
        _sut.SetAllItems(items.ToIdentifiers());

        //then
        Assert.IsTrue(_sut.GetItemsIds().Count == 3);
    }

    [Test]
    public void SetLastUpdate_ShouldSetLastUpdate()
    {
        //given
        //when
        //then
    }
}