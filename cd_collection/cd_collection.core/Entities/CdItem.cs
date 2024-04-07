using cd_collection.core.ValueObjects;

namespace cd_collection.core.Entities;

public class CdItem
{
    public CdItemId Id { get; private set; }
    public Artist Artist { get; private set; }
    public Title Title { get; private set; }
    public Label Label { get; private set; }
    public Date ReleaseDate { get; private set; }
    public Date LastUpdate { get;  private set; }

    public List<Collection> Colections{
        get;
        set;
    }
    
    public CdItem(Artist artist, Title title, Label label, Date releaseDate)
    {
        Id = Guid.NewGuid();
        Artist = artist;
        Title = title;
        Label = label;
        ReleaseDate = releaseDate;
        LastUpdate = DateTime.Now;
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

    public void ChangeReleaseDate(DateTime newReleaseDate)
    {
        ReleaseDate = newReleaseDate;
        SetLastUpdate();
    }

    private void SetLastUpdate() => LastUpdate = DateTime.Now;
}