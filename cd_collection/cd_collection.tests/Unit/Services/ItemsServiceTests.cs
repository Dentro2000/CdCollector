using cd_collection.application.Services;
using cd_collection.application.Services.Contracts;
using cd_collection.core.Contracts;
using cd_collection.core.Exceptions.ItemServiceExceptions;

namespace cd_collection.tests.Unit.Services;

public class ItemsServiceTests
{
    private IItemsService _sut;
    private IItemsRepository _mockItemsRepository;

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
        //given
        _mockItemsRepository.AddItem(MockItem.MockCdItem);
        _mockItemsRepository.AddItem(MockItem.MockCdItem);

        //when
        var items = _sut.GetItems();

        //then
        Assert.IsTrue(items.Count == 2);
    }

    [Test]
    public void GetItem_Should_ReturnSingleItem()
    {
        //given
        var item1 = MockItem.MockCdItem;
        _mockItemsRepository.AddItem(item1);
        _mockItemsRepository.AddItem(MockItem.MockCdItem);

        //when
        var item = _sut.GetItem(item1.Identifier);

        //then
        Assert.IsTrue(item.Id == item1.Identifier.Value);
    }

    [Test]
    public void CreateItem_Should_CreateNewItem()
    {
        //given
        var title = "Title";
        var artist = "Fancy Artist";
        var label = "Best Music Productions";
        var releaseDate = new DateTime(1987, 02, 21);

        //when
        var newItem = _sut.CreateItem(artist, title, label, releaseDate);

        //then
        var result = _sut.GetItem(newItem.Id);

        Assert.IsTrue(result.Artist == artist);
        Assert.IsTrue(result.Label == label);
        Assert.IsTrue(result.Title == title);
        Assert.IsTrue(result.ReleaseDate == releaseDate);
    }


    [Test]
    public void CreateItem_Should_ReturnExceptionIfItemAlreadyExists()
    {
        //given
        var title = "Title";
        var artist = "Fancy Artist";
        var label = "Best Music Productions";
        var releaseDate = new DateTime(1987, 02, 21);
        var newItem = _sut.CreateItem(artist, title, label, releaseDate);
        //when
        //then
        Assert.Throws<ItemAlreadyExistsException>(() => _sut.CreateItem(artist, title, label, releaseDate));
    }

    [Test]
    public void UpdateItem_Should_UpdateOnlyNonNullProperties()
    {
        //given
        var item1 = MockItem.MockCdItem;
        _mockItemsRepository.AddItem(item1);
        var newLabel = "some oother label";
        //when
        var updatedItem = _sut.UpdateItem(item1.Identifier, null, null, newLabel, null);
        //then

        Assert.IsTrue(updatedItem.Artist == item1.Artist);
        Assert.IsTrue(updatedItem.Title == item1.Title);
        Assert.IsTrue(updatedItem.ReleaseDate == item1.ReleaseDate.Value);
        Assert.IsTrue(updatedItem.Label == newLabel);
    }


    [Test]
    public void UpdateItem_Should_ReturnException_IfItemDontExists()
    {
        //given
        //when
        //then
        Assert.Throws<CantUpdateItemException>(() =>
            _sut.UpdateItem(Guid.NewGuid(), null, null, "newLabel", null));
    }

    [Test]
    public void DeleteItem_Should_DeleteItem()
    {
        //given
        var item1 = MockItem.MockCdItem;
        _mockItemsRepository.AddItem(item1);

        //when
        var isDeleted = _sut.DeleteItem(item1.Identifier);

        //then
        Assert.IsEmpty(_sut.GetItems());
        Assert.IsTrue(isDeleted);
    }
}