using DataAccessFakes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest;

[TestClass]
public class CardComponentManagerTest
{
    ICardComponentManager _componentManager;

    [TestInitialize]
    public void TestSetup()
    {
        _componentManager = new CardComponentManager(new CardComponentAccessorFakes());
    }

    [TestMethod]
    public void TestGetMovesByCardIDRetrunsMovesWithValidID()
    {
        // arrange
        const int cardID = 1;
        const int expectedCount = 2;
        List<MoveVM> actual;

        // act
        actual = _componentManager.GetMovesByCardID(cardID);

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestGetMovesByCardIDRetrunsBlankListWithInvalidID()
    {
        // arrange
        const int cardID = 999;
        const int expectedCount = 0;
        List<MoveVM> actual;

        // act
        actual = _componentManager.GetMovesByCardID(cardID);

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestGetAlternateArtsByCardIDRetrunsMovesWithValidID()
    {
        // arrange
        const int cardID = 1;
        const int expectedCount = 2;
        List<string> actual;

        // act
        actual = _componentManager.GetAlternateArtsByCardID(cardID);

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TesGetAlternateArtsByCardIDRetrunsBlankListWithInvalidID()
    {
        // arrange
        const int cardID = 999;
        const int expectedCount = 0;
        List<string> actual;

        // act
        actual = _componentManager.GetAlternateArtsByCardID(cardID);

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestAddCardMoveReturnsTrueWithValidIDs()
    {
        // arrange
        const int cardID = 1;
        const int moveID = 3;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _componentManager.AddCardMove(cardID, moveID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardMoveThrowsApplicationExceptionWithInvalidCardID()
    {
        // arrange
        const int cardID = 999;
        const int moveID = 3;
        bool actual = true;

        // act
        actual = _componentManager.AddCardMove(cardID, moveID);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardMoveThrowsApplicationExceptionWithInvalidMoveID()
    {
        // arrange
        const int cardID = 1;
        const int moveID = 999;
        bool actual = true;

        // act
        actual = _componentManager.AddCardMove(cardID, moveID);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardMoveThrowsApplicationExceptionWithDuplicateIDs()
    {
        // arrange
        const int cardID = 1;
        const int moveID = 1;
        bool actual = true;

        // act
        actual = _componentManager.AddCardMove(cardID, moveID);
        actual = _componentManager.AddCardMove(cardID, moveID);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestDeleteCardMovesReturnsTrueWithValidCardID()
    {
        // arrange
        const int cardID = 1;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _componentManager.DeleteCardMoves(cardID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestDeleteCardMovesReturnsFalseWithInvalidCardID()
    {
        // arrange
        const int cardID = 999;
        const bool expected = false;
        bool actual = true;

        // act
        actual = _componentManager.DeleteCardMoves(cardID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestAddCardAlternateArtReturnsTrueWithValidIDs()
    {
        // arrange
        const int cardID = 1;
        const string altArtID = "test Alternate Art 3";
        const bool expected = true;
        bool actual = false;

        // act
        actual = _componentManager.AddCardAlternateArt(cardID, altArtID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardAlternateArtThrowsApplicationExceptionWithInvalidCardID()
    {
        // arrange
        const int cardID = 999;
        const string altArtID = "test Alternate Art 3";
        bool actual = true;

        // act
        actual = _componentManager.AddCardAlternateArt(cardID, altArtID);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardAlternateArtThrowsApplicationExceptionWithInvalidAltArtID()
    {
        // arrange
        const int cardID = 1;
        const string altArtID = "fails";
        bool actual = true;

        // act
        actual = _componentManager.AddCardAlternateArt(cardID, altArtID);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardAlternateArtThrowsApplicationExceptionWithDuplicateIDs()
    {
        // arrange
        const int cardID = 1;
        const string altArtID = "test Alternate Art 3";
        bool actual = true;

        // act
        actual = _componentManager.AddCardAlternateArt(cardID, altArtID);
        actual = _componentManager.AddCardAlternateArt(cardID, altArtID);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAddCardAlternateArtThrowsArgumentNullExceptionWithBlankAltArtID()
    {
        // arrange
        const int cardID = 1;
        const string altArtID = "";
        bool actual = true;

        // act
        actual = _componentManager.AddCardAlternateArt(cardID, altArtID);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAddCardAlternateArtThrowsArgumentNullExceptionWithNullAltArtID()
    {
        // arrange
        const int cardID = 1;
        const string altArtID = null;
        bool actual = true;

        // act
        actual = _componentManager.AddCardAlternateArt(cardID, altArtID);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestDeleteCardAlternateArtsReturnsTrueWithValidCardID()
    {
        // arrange
        const int cardID = 1;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _componentManager.DeleteCardAlternateArts(cardID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TesDeleteCardAlternateArtsReturnsFalseWithInvalidCardID()
    {
        // arrange
        const int cardID = 999;
        const bool expected = false;
        bool actual = true;

        // act
        actual = _componentManager.DeleteCardAlternateArts(cardID);

        // assert
        Assert.AreEqual(expected, actual);
    }
}
