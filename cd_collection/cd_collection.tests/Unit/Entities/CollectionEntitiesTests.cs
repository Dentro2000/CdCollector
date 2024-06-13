using cd_collection.core.Entities;
using cd_collection.core.Exceptions.Collection;

namespace cd_collection.tests.Unit.Entities;

public class CollectionEntitiesTests
{
    private Collection _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new Collection("MyCollection", Guid.NewGuid());
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
        var newItemGuid = MockItem.MockCdItem;
        _sut.AddItem(newItemGuid);
        Assert.IsTrue(_sut.GetItemsIds().ToList().Count == 1);

        //when
        _sut.RemoveItem(newItemGuid);

        //then
        Assert.IsTrue(_sut.GetItemsIds().ToList().Count == 0);
    }

    [Test]
    public void RemoveItem_ShouldRemoveItem_ShouldThrowException_If_NoItemID()
    {
        //given
        //when
        //then
        Assert.Throws<CannotRemoveItemException>(() =>
            _sut.RemoveItem(MockItem.MockCdItem));
    }

    [Test]
    public void AddItem_ShouldAddItem()
    {
        //given
        var item = MockItem.MockCdItem;
        //when
        var collection = _sut.AddItem(item);
        //then
        Assert.IsTrue(collection.GetItemsIds().First() == item.Id);
    }

    [Test]
    public void GetItemsIds_ShouldGetItemsIds()
    {
        //given
        var item = MockItem.MockCdItem;
        _sut.AddItem(item);

        //when
        var items = _sut.GetItemsIds();

        //then
        Assert.IsTrue(items.First() == item.Id);
    }

    [Test]
    public void SetAllItems_ShouldSetAllItems()
    {
        //given
        var item1 = MockItem.MockCdItem;
        var item2 = MockItem.MockCdItem;
        var item3 = MockItem.MockCdItem;

        var items = new List<CdItem>
        {
            item1, item2, item3,
        };
        //when
        _sut.SetAllItems(items);

        //then
        Assert.IsTrue(_sut.GetItemsIds().ToList().Count == 3);
    }
}