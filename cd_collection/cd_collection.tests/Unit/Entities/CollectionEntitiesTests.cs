using cd_collection.Entities;

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
    public void RemoveItem_ShouldRemoveItem_ShouldThrowException_If()
    {
        
    }
    
    [Test]
    public void AddItem_ShouldAddItem()
    {
        
    }
    
    [Test]
    public void AddItem_ShouldThrowException_If()
    {
        
    }
    
    [Test]
    public void GetItemsIds_ShouldGetItemsIds ()
    {
        
    }
    
    [Test]
    public void SetAllItems_ShouldSetAllItems()
    {
        
    }
    
    [Test]
    public void SetLastUpdate_ShouldSetLastUpdate()
    {
        
    }
}