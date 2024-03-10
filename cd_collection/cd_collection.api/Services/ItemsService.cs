using cd_collection.Models;
using cd_collection.Repositories.Contracts;

namespace cd_collection.Repository;

public class ItemsService: IItemsService
{
    private IInMemoryItemsRepository _repository;
    
    public ItemsService(IInMemoryItemsRepository repository)
    {
        _repository = repository;
    }

    //TODO: Serwisy powinny zwracac listy a nie enumerable
    public IList<CdItemModel?> GetItems()
    {
        return _repository.GetItems().ToList();
    }

    public CdItemModel? GetItem(Guid id)
    {
        return _repository.GetItem(id: id);
    }

    public CdItemModel? CreateItem(string artist, string title, string label, DateTime releaseDate)
    {
        var newItem = new CdItemModel(artist, title, label, releaseDate);
        
        var ifItemAlreadyExist = _repository.GetItems()
            .SingleOrDefault(x =>
            x.Artist == artist 
            && x.Title == title 
            && x.Label == label 
            && x.ReleaseDate == releaseDate);

        if (ifItemAlreadyExist != null)
        {
            //should throw exception
            return null;
        }

        _repository.AddItem(newItem);
        return _repository.GetItem(newItem.Id);
    }

    public CdItemModel? UpdateItem(Guid guid, string? artist, string? title, string? label, DateTime? releaseDate)
    {
        var item = _repository.GetItem(guid);

        if (item == null)
        {
            return null;
            //throw exception
        }

        if (!string.IsNullOrEmpty(artist))
        {
            item.ChangeArtist(artist);
        }

        if (!string.IsNullOrEmpty(title))
        {
         item.ChangeTitle(title);   
        }
        
        if (!string.IsNullOrEmpty(label))
        {
            item.ChangeLabel(label);
        }
        
        if (releaseDate != null)
        {
            item.ChangeReleaseDate(releaseDate.Value);
        }

        return item;
    }

    public void DeleteItem(Guid guid)
    {
        //return bool
        _repository.DeleteItem(guid);
    }
}