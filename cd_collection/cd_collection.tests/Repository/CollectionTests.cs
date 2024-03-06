using cd_collection.Repository;

namespace cd_collection.tests;

public class CollectionsRepositoryTests
{
    private  ICollectionsService _service;
    
    [SetUp]
    public void Setup()
    {
        _service = new CollectionsService();
    }

    [Test]
    public void TestCreate()
    {
        // Given
        // When
       var newCollection =  _service.AddCollection("TestCollection");
        
        //Then
        var collection = _service.GetCollection(newCollection.Id);
        
        Assert.IsNotNull(collection);

    }
    
    [Test]
    public void TestUpdate()
    {
        // Given
        var collection =  _service.AddCollection("NewTestCollection");
        
        // When
        var newName = "ChangedName";
        _service.UpdateCollection(collection.Id, newName, null);
        
        //Then
        var updated = _service.GetCollection(collection.Id);
        
        Assert.True(updated.Name == newName);
    }
}