using DataAccessFakes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest;

[TestClass]
public class ArtistManagerTest
{
    IArtistManager _artistManager;
    [TestInitialize]
    public void TestSetup()
    {
        _artistManager = new ArtistManager(new ArtistAccessorFakes());
    }
    [TestMethod]
    public void TestGetArtistByArtistIDWithValidID()
    {
        // arrange
        const int artistID = 1;
        const string expectedGivenName = "Test Given 1";
        const string expectedSurname = "Test Surname 1";
        Artist actualArtist;

        // act
        actualArtist = _artistManager.GetArtistByArtistID(artistID);

        // assert
        Assert.AreEqual(expectedGivenName, actualArtist.GivenName);
        Assert.AreEqual(expectedSurname, actualArtist.Surname);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestGetArtistByArtistIDThrowsApplicationExpectionWithInvalidID()
    {

        // arrange
        const int artistID = 0;
        const string expectedGivenName = "Test Given 1";
        const string expectedSurname = "Test Surname 1";
        Artist actualArtist;

        // act
        actualArtist = _artistManager.GetArtistByArtistID(artistID);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetArtistByNameWithValidInput()
    {
        // arrange
        const string givenName = "Test Given 1";
        const string surname = "Test Surname 1";
        const int expectedID = 1;
        Artist actualArtist = null;

        // act
        actualArtist = _artistManager.GetArtistByName(givenName, surname);

        // assert
        Assert.AreEqual(expectedID, actualArtist.ArtistID);
    }

    [TestMethod]
    public void TestGetArtistByNameWithEmptySurname()
    {
        // arrange
        const string givenName = "Test Given 3";
        const string surname = "";
        const int expectedID = 3;
        Artist actualArtist = null;

        // act
        actualArtist = _artistManager.GetArtistByName(givenName, surname);

        // assert
        Assert.AreEqual(expectedID, actualArtist.ArtistID);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetArtistByNameThrowsArgumentNullExceptionWithEmpptyGivenName()
    {
        // arrange
        const string givenName = "";
        const string surname = "Test Surname 1";
        Artist actualArtist = null;

        // act
        actualArtist = _artistManager.GetArtistByName(givenName, surname);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetArtists()
    {
        // arrange
        const int expectedLength = 3;
        const string expectedGivenName1 = "Test Given 1";
        const string expectedGivenName2 = "Test Given 2";
        List<Artist> artists = null;

        // act
        artists = _artistManager.GetArtists();

        // assert
        Assert.AreEqual(expectedLength, artists.Count);
        Assert.AreEqual(expectedGivenName1, artists[0].GivenName);
        Assert.AreEqual(expectedGivenName2, artists[1].GivenName);
    }

    [TestMethod]
    public void TestAddArtistWithValidInput()
    {
        // arrange
        const string givenName = "Test";
        const string surname = "User";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _artistManager.AddArtist(givenName, surname);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddArtistThrowsApplicationExceptionWithDuplicateInput()
    {
        // arrange
        const string givenName = "Test";
        const string surname = "User";
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _artistManager.AddArtist(givenName, surname);
        actualResult = _artistManager.AddArtist(givenName, surname);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestEditArtistReturnsTrueWithValidInput()
    {
        // arrange
        const int artistID = 1;
        const string newGiveName = "Test";
        const string newSurname = "Artist";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _artistManager.EditArtist(artistID, newGiveName, newSurname);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestEditArtistWithInvalidArtistID()
    {
        // arrange
        const int artistID = -1;
        const string newGiveName = "Test";
        const string newSurname = "Artist";
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _artistManager.EditArtist(artistID, newGiveName, newSurname);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeleteArtistReturnsTrueWithValidArtistID()
    {
        
        // arrange
        const int artistID = 1;
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _artistManager.DeleteArtist(artistID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeleteArtistWithReturnsFalseWithInvalidArtistID()
    {
        // arrange
        const int artistID = -1;
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _artistManager.DeleteArtist(artistID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestFormatArtistsReturnCorrectOrder()
    {
        // arrange
        List<Artist> inputList = new List<Artist>();
        inputList.Add(new Artist()
        {
            ArtistID = 2,
            GivenName = "test",
            Surname = "test",
        });
        inputList.Add(new Artist()
        {
            ArtistID = 1,
            GivenName = "another test",
            Surname = "another test",
        });

        List<Artist> expectedResult = new List<Artist>();
        expectedResult.Add(new Artist()
        {
            ArtistID = 1,
            GivenName = "Another test",
            Surname = "another test",
        });
        expectedResult.Add(new Artist()
        {
            ArtistID = 2,
            GivenName = "Test",
            Surname = "test",
        });

        List<Artist> actualResult = new List<Artist>();

        // act
        actualResult = _artistManager.FormatArtists(inputList).ToList();

        // assert
        Assert.AreEqual(expectedResult[0].ArtistID, actualResult[0].ArtistID);
        Assert.AreEqual(expectedResult[1].ArtistID, actualResult[1].ArtistID);
    }
}