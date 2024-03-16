namespace cd_collection.Models;

public class CdItemModel
{
    public Guid Id { get; private set; }
    public string Artist { get; private set; }
    public string Title { get; private set; }
    public string Label { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public DateTime LastUpdate { get; private set; }

    public CdItemModel(string artist, string title, string label, DateTime releaseDate)
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

    public CdItemModel MockCdItem
    {
        get
        {
            return new CdItemModel(
                "MockArtist", 
                "MockTitle", 
                "MockLabel", 
                new DateTime(2024, 01, 01));
        }
    }
}
