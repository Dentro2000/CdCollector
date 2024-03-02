using cd_collection.Repository;

namespace cd_collection.tests;

public class ItemTests
{
    private  IItemsRepository _repository;
    
    [SetUp]
    public void Setup()
    {
        _repository = new ItemsInMemoryRepository();
    }

    [Test]
    public void TestCreate()
    {
        // Given
        // When
        var newItem =  _repository.CreateItem("Zenek", "Kupatasa", "KupatasaRecords", new DateTime(2024, 02, 29));
        
        //Then
        var item = _repository.GetItem(newItem.Id);
        
        Assert.IsNotNull(item);
    }
    
    [Test]
    public void TestUpdate()
    {
        // Given
        var oldItem =  _repository.CreateItem("Nowy", "Album", "KupatasaRecords", new DateTime(2024, 02, 29));

        
        // When
        var newLabel = "NewLabel";
        var newItem = _repository.UpdateItem(oldItem.Id, oldItem.Artist, oldItem.Title, newLabel, oldItem.ReleaseDate);

        
        //Then
        var updated = _repository.GetItem(oldItem.Id);
        
        Assert.True(updated.Label == newLabel);
    }
}