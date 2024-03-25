using cd_collection.core.ValueObjects;

namespace cd_collection.core.Entities;

public class CdItem
{
    public Guid Id { get; private set; }
    public Artist Artist { get; private set; }
    public Title Title { get; private set; }
    public Label Label { get; private set; }
    public Date ReleaseDate { get; private set; }
    private Date LastUpdate { get;  set; }

    public CdItem(string artist, string title, string label, DateTime releaseDate)
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
