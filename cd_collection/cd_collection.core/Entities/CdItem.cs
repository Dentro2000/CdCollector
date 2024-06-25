using cd_collection.core.ValueObjects;

namespace cd_collection.core.Entities;

public class CdItem
{
    public CdItemId Id { get; private set; }
    public Artist Artist { get; private set; }
    public Title Title { get; private set; }
    public Label Label { get; private set; }
    public ReleaseDate ReleaseDate { get; private set; }    
    public Date LastUpdate { get; private set; }

    public List<Collection> Collections { get; set; }

    public CdItem(CdItemId id, Artist artist, Title title, Label label, ReleaseDate releaseDate)
    {
        Id = id;
        Artist = artist;
        Title = title;
        Label = label;
        ReleaseDate = releaseDate;
        LastUpdate = DateTime.UtcNow;
        Collections = new List<Collection>() { };
    }

    public void ChangeArtist(string newArtist)
    {
        Artist = newArtist;
        SetLastUpdate();
    }

    public void ChangeTitle(string newTitle)
    {
        Title = newTitle;
        SetLastUpdate();
    }

    public void ChangeLabel(string newLabel)
    {
        Label = newLabel;
        SetLastUpdate();
    }

    public void ChangeReleaseDate(DateOnly newReleaseDate)
    {
        ReleaseDate = newReleaseDate;
        SetLastUpdate();
    }

    private void SetLastUpdate() => LastUpdate = DateTime.UtcNow;
}

public static class CdItemExtensions
{
    public static bool IsSameItem(
        this CdItem item, 
        CdItem itemToCompare)
        => item.Artist == itemToCompare.Artist &&
           item.Title == itemToCompare.Title &&
           item.Label == itemToCompare.Label &&
           item.ReleaseDate == itemToCompare.ReleaseDate.Value;
}