using cd_collection.Repository;

namespace cd_collection.tests;

public class CollectionsRepositoryTests
{
    private  ICollectionsRepository _repository;
    
    [SetUp]
    public void Setup()
    {
        _repository = new CollectionsInMemoryRepository();
    }

    [Test]
    public void TestCreate()
    {
        // Given
        // When
       var newCollection =  _repository.AddCollection("TestCollection");
        
        //Then
        var collection = _repository.GetCollection(newCollection.Id);
        
        Assert.IsNotNull(collection);

    }
    
    [Test]
    public void TestUpdate()
    {
        // Given
        var collection =  _repository.AddCollection("NewTestCollection");
        
        // When
        var newName = "ChangedName";
        _repository.UpdateCollection(collection.Id, newName);
        
        //Then
        var updated = _repository.GetCollection(collection.Id);
        
        Assert.True(updated.Name == newName);
    }
}