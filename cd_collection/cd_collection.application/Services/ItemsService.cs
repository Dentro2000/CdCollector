using cd_collection.application.DTO;
using cd_collection.application.Services.Contracts;
using cd_collection.core.Contracts;
using cd_collection.core.Entities;
using cd_collection.core.Exceptions.ItemServiceExceptions;

namespace cd_collection.application.Services;

public class ItemsService : IItemsService
{
    private IItemsRepository _repository;

    public ItemsService(IItemsRepository repository)
    {
        _repository = repository;
    }

    //TODO: Serwisy powinny zwracac listy a nie enumerable
    public IList<CdItemDto?> GetItems()
    {
        return _repository.GetItems().Select(item => new CdItemDto
        {
            Id = item.Id,
            Artist = item.Artist,
            Title = item.Title,
            Label = item.Label,
            ReleaseDate = item.ReleaseDate,
        }).ToList();
    }

    public CdItemDto? GetItem(Guid id)
    {
        var item = _repository.GetItem(id: id);
        return new CdItemDto
        {
            Id = item.Id,
            Artist = item.Artist,
            Title = item.Title,
            Label = item.Label,
            ReleaseDate = item.ReleaseDate,
        };
    }

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
        var item = _repository.GetItem(newItem.Id);

        //TODO: move to extension converting to dto
        return new CdItemDto
        {
            Id = item.Id,
            Artist = item.Artist,
            Title = item.Title,
            Label = item.Label,
            ReleaseDate = item.ReleaseDate,
        };
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

        return new CdItemDto
        {
            Id = item.Id,
            Artist = item.Artist,
            Title = item.Title,
            Label = item.Label,
            ReleaseDate = item.ReleaseDate
        };
    }

    public bool DeleteItem(Guid guid)
    {
        return _repository.DeleteItem(guid);
    }
}