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
    public void TestGetActiveAlternateArtsWithValidInput()
    {
        // arrange
        const int count = 3;
        const string id2 = "Test Alternate Art 2";
        const string description3 = "This is a description 3.";
        PaginatedResult<AlternateArt> actualResult;

        // act
        actualResult = _altArtManager.GetActiveAlternateArts();

        // assert
        Assert.AreEqual(count, actualResult.Items.Count);
        Assert.AreEqual(id2, actualResult.Items[1].AlternateArtID);
        Assert.AreEqual(description3, actualResult.Items[2].Description);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetActiveAlternateArtsThrowsArgumentExceptionWithNegativePageNumber()
    {
        // arrange
        const int pageNumber = -1;
        PaginatedResult<AlternateArt> actualResult;

        // act
        actualResult = _altArtManager.GetActiveAlternateArts(pageNumber: pageNumber);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetActiveAlternateArtsThrowsArgumentExceptionWithNegativePageSize()
    {
        // arrange
        const int pageSize = -1;
        PaginatedResult<AlternateArt> actualResult;

        // act
        actualResult = _altArtManager.GetActiveAlternateArts(pageSize: pageSize);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetDeactiveAlternateArtsWithValidInput()
    {
        // arrange
        const int count = 1;
        PaginatedResult<AlternateArt> actualResult;

        // act
        actualResult = _altArtManager.GetDeactiveAlternateArts();

        // assert
        Assert.AreEqual(count, actualResult.Items.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetDeactiveAlternateArtsThrowsArgumentExceptionWithNegativePageNumber()
    {
        // arrange
        const int pageNumber = -1;
        PaginatedResult<AlternateArt> actualResult;

        // act
        actualResult = _altArtManager.GetDeactiveAlternateArts(pageNumber: pageNumber);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetDeactiveAlternateArtsThrowsArgumentExceptionWithNegativePageSize()
    {
        // arrange
        const int pageSize = -1;
        PaginatedResult<AlternateArt> actualResult;

        // act
        actualResult = _altArtManager.GetDeactiveAlternateArts(pageSize: pageSize);

        // assert
        // do nothing
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

    [TestMethod]
    public void TestDeactivateAlternateArtReturnsTrueWithValidInput()
    {
        // arrange
        const string alternateArtID = "Test Alternate Art 1";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _altArtManager.DeactivateAlternateArt(alternateArtID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeactivateAlternateArtReturnsTrueWithAlreadyDeactiveAbility()
    {
        // arrange
        const string alternateArtID = "Test Alternate Art 4";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _altArtManager.DeactivateAlternateArt(alternateArtID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeactivateAlternateArtReturnsFalseWithInvalidInput()
    {
        // arrange
        const string alternateArtID = "failed";
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _altArtManager.DeactivateAlternateArt(alternateArtID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestDeactivateAlternateArtThrowsArgumentNullExceptionWithNullAbilityID()
    {
        // arrange
        const string alternateArtID = null;
        bool actualResult = true;

        // act
        actualResult = _altArtManager.DeactivateAlternateArt(alternateArtID);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TesDeactivateAlternateArtThrowsArgumentNullExceptionWithBlankAbilityID()
    {
        // arrange
        const string alternateArtID = "";
        bool actualResult = true;

        // act
        actualResult = _altArtManager.DeactivateAlternateArt(alternateArtID);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestReactivateAlternateArtReturnsTrueWithValidInput()
    {
        // arrange
        const string alternateArtID = "Test Alternate Art 4";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _altArtManager.ReactivateAlternateArt(alternateArtID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestReactivateAlternateArtReturnsTrueWithAlreadyActiveAbility()
    {
        // arrange
        const string alternateArtID = "Test Alternate Art 4";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _altArtManager.ReactivateAlternateArt(alternateArtID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestReactivateAbilityReturnsFalseWithInvalidInput()
    {
        // arrange
        const string alternateArtID = "failed";
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _altArtManager.ReactivateAlternateArt(alternateArtID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestReactivateAlternateArtThrowsArgumentNullExceptionWithNullAbilityID()
    {
        // arrange
        const string alternateArtID = null;
        bool actualResult = true;

        // act
        actualResult = _altArtManager.ReactivateAlternateArt(alternateArtID);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestReactivateAlternateArtThrowsArgumentNullExceptionWithBlankAbilityID()
    {
        // arrange
        const string alternateArtID = "";
        bool actualResult = true;

        // act
        actualResult = _altArtManager.ReactivateAlternateArt(alternateArtID);

        // assert
        // do nothing
    }
}
