using DataAccessFakes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest;

[TestClass]
public class AltArtManagerTest
{
    IAltArtManager _altArtManager;
    [TestInitialize]
    public void TestSetup()
    {
        _altArtManager = new AltArtManager(new AltArtAccessorFakes());
    }

    [TestMethod]
    public void TestGetAbilityByAbilityIDWithValidInput()
    {
        // arrange
        const string abilityID = "Test Alternate Art 1";
        const string description = "This is a description 1.";
        AlternateArt actualResult = null;

        // act
        actualResult = _altArtManager.GetAlternateArtByID(abilityID);

        // assert
        Assert.AreEqual(abilityID, actualResult.AlternateArtID);
        Assert.AreEqual(description, actualResult.Description);

    }

    [TestMethod]
    public void TestGetAbilityByAbilityIDWithInvalidInput()
    {
        // arrange
        const string abilityID = "Test Fails";
        const AlternateArt expectedResult = null;
        AlternateArt actualResult = null;

        // act
        actualResult = _altArtManager.GetAlternateArtByID(abilityID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);

    }

    [TestMethod]
    public void TestGetAlternateArtsWithValidInput()
    {
        // arrange
        const int count = 3;
        const string id2 = "Test Alternate Art 2";
        const string description3 = "This is a description 3.";
        List<AlternateArt> actualResult;

        // act
        actualResult = _altArtManager.GetAlternateArts();

        // assert
        Assert.AreEqual(count, actualResult.Count);
        Assert.AreEqual(id2, actualResult[1].AlternateArtID);
        Assert.AreEqual(description3, actualResult[2].Description);
    }

    [TestMethod]
    public void TestAddAlternateArtReturnsTrueWithValidAbility()
    {
        // arrange
        AlternateArt alternateArt = new AlternateArt()
        {
            AlternateArtID = "New AbilityID",
            Description = "Test",
        };
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _altArtManager.AddAlternateArt(alternateArt);

        // assert 
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAddAlternateArtThrowsArgumentNullExceptionWithNullAbility()
    {
        // arrange
        AlternateArt alternateArt = null;
        bool actualResult = false;

        // act
        actualResult = _altArtManager.AddAlternateArt(alternateArt);

        // assert 
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddAlternateArtThrowsApplicationExceptionWitDuplicateID()
    {
        // arrange
        AlternateArt alternateArt = new AlternateArt()
        {
            AlternateArtID = "Test Alternate Art 2",
            Description = "Test",
        };
        bool actualResult = false;

        // act
        actualResult = _altArtManager.AddAlternateArt(alternateArt);

        // assert 
        // do nothing
    }

    [TestMethod]
    public void TestEditAlternateArtReturnsTrueWithValidAbility()
    {
        // arrange
        AlternateArt alternateArt = new AlternateArt()
        {
            AlternateArtID = "Test Alternate Art 2",
            Description = "Test Update",
        };
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _altArtManager.EditAlternateArt(alternateArt);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }
    [TestMethod]
    public void TestEditAlternateArtReturnsFalseWithInvalidAbilityID()
    {
        // arrange
        AlternateArt alternateArt = new AlternateArt()
        {
            AlternateArtID = "Test Failed",
            Description = "Test Update",
        };
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _altArtManager.EditAlternateArt(alternateArt);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestEditAlternateArtThrowsArgumentNullExceptionWithNullAbility()
    {
        // arrange
        AlternateArt alternateArt = null;
        bool actualResult = false;

        // act
        actualResult = _altArtManager.EditAlternateArt(alternateArt);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestDeleteAlternateArtReturnsTrueWithValidInput()
    {
        // arrange
        const string alternateArtID = "Test Alternate Art 2";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _altArtManager.DeleteAlternateArt(alternateArtID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeleteAlternateArtReturnsFalseWithInvalidInput()
    {
        // arrange
        const string alternateArtID = "Test Failed";
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _altArtManager.DeleteAlternateArt(alternateArtID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }
}
