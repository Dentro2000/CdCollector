using cd_collection.Models;
using cd_collection.Repositories.Contracts;

namespace cd_collection.Repositories;

public class InMemoryCollectionRepository : IInMemoryCollectionRepository
{
    public List<Collection?>  collections = new List<Collection?>
    {
        new Collection(name: "OneTwoThree"),
        new Collection(name: "FourFiveSix"),
    };

    public void SaveCollections(Collection collection)
    {
        
    }
    

}