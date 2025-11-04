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
        const string surname = " ";
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
}
