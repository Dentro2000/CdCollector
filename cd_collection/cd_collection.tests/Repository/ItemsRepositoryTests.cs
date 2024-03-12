using cd_collection.Repository;

namespace cd_collection.tests.Repository;

public class ItemsRepositoryTests
{
    private  IItemsService _service;
    
    [SetUp]
    public void Setup()
    {
        _service = new ItemsService();
    }

    [Test]
    public void TestCreate()
    {
        // Given
        // When
        var newItem =  _service.CreateItem("Zenek", "Kupatasa", "KupatasaRecords", new DateTime(2024, 02, 29));
        
        //Then
        var item = _service.GetItem(newItem.Id);
        
        Assert.IsNotNull(item);
    }
    
    [Test]
    public void TestUpdate()
    {
        // Given
        var oldItem =  _service.CreateItem("Nowy", "Album", "KupatasaRecords", new DateTime(2024, 02, 29));

        
        // When
        var newLabel = "NewLabel";
        var newItem = _service.UpdateItem(oldItem.Id, oldItem.Artist, oldItem.Title, newLabel, oldItem.ReleaseDate);

        
        //Then
        var updated = _service.GetItem(oldItem.Id);
        
        Assert.True(updated.Label == newLabel);
    }
}