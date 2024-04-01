using cd_collection.application.DTO;
using cd_collection.application.Extensions;
using cd_collection.application.Services.Contracts;
using cd_collection.core.Contracts;
using cd_collection.core.Entities;
using cd_collection.core.Exceptions.ItemServiceExceptions;

namespace cd_collection.application.Services;

public class CdItemsService : IItemsService
{
    private IItemsRepository _repository;

    public CdItemsService(IItemsRepository repository)
    {
        _repository = repository;
    }

    //TODO: Serwisy powinny zwracac listy a nie enumerable
    public IList<CdItemDto?> GetItems()
    {
        return _repository.GetItems().Select(item => new CdItemDto
        {
            Identifier = item.Identifier,
            Artist = item.Artist,
            Title = item.Title,
            Label = item.Label,
            ReleaseDate = item.ReleaseDate,
        }).ToList();
    }

    public CdItemDto? GetItem(Guid id) => _repository.GetItem(id: id).ConvertToDto();


    public CdItemDto CreateItem(string artist, string title, string label, DateTime releaseDate)
    {
        var newItem = new CdItem(artist, title, label, releaseDate);

        var ifItemAlreadyExist = _repository.GetItems()
            .SingleOrDefault(x =>
                x.Artist == artist
                && x.Title == title
                && x.Label == label
                && x.ReleaseDate.Value == releaseDate);

        if (ifItemAlreadyExist != null)
        {
            throw new ItemAlreadyExistsException(title, artist, label, releaseDate);
        }

        _repository.AddItem(newItem);
        return _repository.GetItem(newItem.Identifier).ConvertToDto();
    }

    public CdItemDto UpdateItem(Guid guid, string? artist, string? title, string? label, DateTime? releaseDate)
    {
        var item = _repository.GetItem(guid);

        if (item == null)
        {
            throw new CantUpdateItemException(guid);
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

        return item.ConvertToDto();
    }

    public bool DeleteItem(Guid guid)
    {
        return _repository.DeleteItem(guid);
    }
}