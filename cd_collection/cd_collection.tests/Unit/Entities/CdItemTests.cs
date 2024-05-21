using cd_collection.core.Entities;

namespace cd_collection.tests.Unit.Entities;

public class CdItemTests
{
    
    private CdItem? _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new CdItem("Artist", "Title", "Label", new DateOnly(2024, 05, 21));
    }

    [TearDown]
    public void TearDown()
    {
        _sut = null;
    }
    
    [Test]
    public void ChangeArtist_ShouldChangeArtist()
    {
        //given
        var newArtist = "Test";

        //when
        _sut.ChangeArtist(newArtist);

        //then  
        Assert.IsTrue(_sut.Artist == newArtist);
    }
    
    [Test]
    public void ChangeLabel_ShouldChangeLabel()
    {
        //given
        var newLabel = "Test";

        //when
        _sut.ChangeLabel(newLabel);

        //then
        Assert.IsTrue(_sut.Label == newLabel);
    }
    
    [Test]
    public void ChangeReleaseDate_ShouldChangeReleaseDate()
    {
        //given
        var newReleaseDate = new DateOnly(1986, 05, 13);

        //when
        _sut.ChangeReleaseDate(newReleaseDate);

        //then
        Assert.IsTrue(_sut.ReleaseDate == newReleaseDate);
    }
    
    [Test]
    public void IsSameItem_CompareItem()
    {
        //given
       var sameItem = new CdItem("Artist", "Title", "Label", new DateOnly(2024, 05, 21));

        //when
        var isSameItem = _sut.IsSameItem(sameItem.Artist, sameItem.Title, sameItem.Label, sameItem.ReleaseDate);

        //then
        Assert.IsTrue(isSameItem);
    }
}