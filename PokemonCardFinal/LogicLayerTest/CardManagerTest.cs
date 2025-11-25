using DataAccessFakes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest;

[TestClass]
public class CardManagerTest
{
    ICardManager _cardManager;

    [TestInitialize]
    public void TestSetup()
    {
        _cardManager = new CardManager(new CardAccessorFakes());
    }

    [TestMethod]
    public void TestGetCardByCardIDWithValidCardID()
    {
        // arrange
        const int cardID = 1;
        Card expectedCard = new Card()
        {
            CardID = 1,
            ArtistID = 1,
            AbilityID = "test ability 1",
            BoosterID = "test booster 1",
            PokemonRuleID = "test pokemon rule 1",
            ElementTypeID = "test element",
            Name = "test 1",
            BoosterNumber = 1,
            CardType = "test type 1",
            Rarity = "test rarity 1",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        Card actualCard = null;

        // act
        actualCard = _cardManager.GetCardByCardID(cardID);

        // assert
        Assert.AreEqual(expectedCard.CardID, actualCard.CardID);
        Assert.AreEqual(expectedCard.BoosterID, actualCard.BoosterID);
        Assert.AreEqual(expectedCard.BoosterNumber, actualCard.BoosterNumber);
        Assert.AreEqual(expectedCard.Stage, actualCard.Stage);
        Assert.AreEqual(expectedCard.ElementTypeID, actualCard.ElementTypeID);
        
    }

    [TestMethod]
    public void TestGetCardByCardIDWithInvalidCardID()
    {
        // arrange
        const int cardID = 999;
        Card expectedCard = null;
        Card actualCard = null;

        // act
        actualCard = _cardManager.GetCardByCardID(cardID);

        // assert
        Assert.AreEqual(expectedCard, actualCard);
    }

    [TestMethod]
    public void TestGetMovesByCardIDWithValidCardID() 
    {
        // arrange
        const int cardID = 1;
        const int count = 2;
        const string moveName1 = "testMove1";
        const string moveName2 = "testMove2";
        List<MoveVM> actualMoves = null;

        // act
        actualMoves = _cardManager.GetMovesByCardID(cardID);

        // assert
        Assert.AreEqual(count, actualMoves.Count);
        Assert.AreEqual(moveName1, actualMoves[0].Name);
        Assert.AreEqual(moveName2, actualMoves[1].Name);
    }

    [TestMethod]
    public void TestGetMovesByCardIDWithInvalidCardID() 
    {
        // arrange
        const int cardID = 999;
        const int count = 0;
        List<MoveVM> actualMoves = null;

        // act
        actualMoves = _cardManager.GetMovesByCardID(cardID);

        // assert
        Assert.AreEqual(count, actualMoves.Count);
    }

    [TestMethod]
    public void TestGetAlternateArtsByCardIDWithValidCardID()
    {
        // arrange
        const int cardID = 1;
        const int count = 2;
        const string altArt1 = "test Alternate Art 1";
        const string altArt2 = "test Alternate Art 2";
        List<string> actualMoves = null;

        // act
        actualMoves = _cardManager.GetAlternateArtsByCardID(cardID);

        // assert
        Assert.AreEqual(count, actualMoves.Count);
        Assert.AreEqual(altArt1, actualMoves[0]);
        Assert.AreEqual(altArt2, actualMoves[1]);
    }

    [TestMethod]
    public void TestGetAlternateArtsByCardIDWithInvalidCardID()
    {
        // arrange
        const int cardID = 999;
        const int count = 0;
        List<string> actualMoves = null;

        // act
        actualMoves = _cardManager.GetAlternateArtsByCardID(cardID);

        // assert
        Assert.AreEqual(count, actualMoves.Count);
    }

    [TestMethod]
    public void TestGetCardVMByCardIDWithValidCardID() 
    {
        // arrange
        const int cardID = 1;
        const string cardName = "test 1";
        const int costCount = 2;
        const int altArtCount = 2;
        CardVM actualCardVM = null;

        // act
        actualCardVM = _cardManager.GetCardVMByCardID(cardID);

        // assert
        Assert.AreEqual(cardID, actualCardVM.CardID);
        Assert.AreEqual(costCount, actualCardVM.Moves.Count);
        Assert.AreEqual(altArtCount, actualCardVM.AlternateArts.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestGetCardVMByCardIDThrowsApplicationExceptionWithInvaildCardID() 
    {
        // arrange
        const int cardID = 999;
        CardVM expectedVM = null;
        CardVM actualCardVM = null;

        // act
        actualCardVM = _cardManager.GetCardVMByCardID(cardID);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetCards()
    {
        // arrange
        const int keyCount = 3;
        const int valueCount = 3;
        const int cardID2 = 2;
        const string cardName1 = "test 1";
        Dictionary<int,Card> actualResult = null;

        // act
        actualResult = _cardManager.GetCards();

        // assert
        Assert.AreEqual(keyCount, actualResult.Keys.Count);
        Assert.AreEqual(valueCount, actualResult.Values.Count);
        Assert.AreEqual(cardName1, actualResult[1].Name);
        Assert.AreEqual(cardID2, actualResult[2].CardID);
    }

    [TestMethod]
    public void TestGetCardMoves()
    {
        // arrange
        const int keyCount = 2;
        const int valueCount = 2;
        const int cardCount2 = 1;
        const int moveID1 = 1;
        Dictionary<int,List<MoveVM>> actualResult = null;

        // act
        actualResult = _cardManager.GetCardMoves();

        // assert
        Assert.AreEqual(keyCount, actualResult.Keys.Count);
        Assert.AreEqual(valueCount, actualResult.Values.Count);
        Assert.AreEqual(moveID1, actualResult[1][0].MoveID);
        Assert.AreEqual(cardCount2, actualResult[2].Count);
    }

    [TestMethod]
    public void TestGetCardAlternateArts()
    {
        // arrange
        const int keyCount = 2;
        const int valueCount = 2;
        const int artCount2 = 1;
        const string altArt1 = "test Alternate Art 1";
        Dictionary<int, List<string>> actualResult = null;

        // act
        actualResult = _cardManager.GetCardAlternateArts();

        // assert
        Assert.AreEqual(keyCount, actualResult.Keys.Count);
        Assert.AreEqual(valueCount, actualResult.Values.Count);
        Assert.AreEqual(altArt1, actualResult[1][0]);
        Assert.AreEqual(artCount2, actualResult[2].Count);
    }

    [TestMethod]
    public void TestGetCardVMs()
    {
        // arrange
        const int count = 3;
        const string cardName1 = "test 1";
        const int moveID1 = 1;
        const string altArt1 = "test Alternate Art 1";
        List<CardVM> actualResult = null;

        // act
        actualResult = _cardManager.GetCardVMs();

        // assert
        Assert.AreEqual(count, actualResult.Count);
        Assert.AreEqual(cardName1, actualResult[0].Name);
        Assert.AreEqual(moveID1, actualResult[0].Moves[0].MoveID);
        Assert.AreEqual(altArt1, actualResult[0].AlternateArts[0]);

    }

    [TestMethod]
    public void TestDeleteCardReturnsTrueWithValidID()
    {
        // arrange
        const int cardID = 1;
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _cardManager.DeleteCard(cardID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeleteCardReturnsFalseWithInvalidID()
    {
        // arrange
        const int cardID = 999;
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _cardManager.DeleteCard(cardID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestGetCardsByCardNameWithValidName()
    {
        // arrange
        const string name = "test 1";
        const int keyCount = 2;
        const int valueCount = 2;
        const int cardID3 = 3;
        const string cardName1 = "test 1";
        Dictionary<int, Card> actualResult = null;

        // act
        actualResult = _cardManager.GetCardsByCardName(name);

        // assert
        Assert.AreEqual(keyCount, actualResult.Keys.Count);
        Assert.AreEqual(valueCount, actualResult.Values.Count);
        Assert.AreEqual(cardName1, actualResult[1].Name);
        Assert.AreEqual(cardID3, actualResult[3].CardID);

    }

    [TestMethod]
    public void TestGetCardsByCardNameWithInvalidName()
    {
        // arrange
        const string name = "fail";
        const int keyCount = 0;
        const int valueCount = 0;

        Dictionary<int, Card> actualResult = null;

        // act
        actualResult = _cardManager.GetCardsByCardName(name);

        // assert
        Assert.AreEqual(keyCount, actualResult.Keys.Count);
        Assert.AreEqual(valueCount, actualResult.Values.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetCardsByCardNameThrowsArgumentNullExceptionWithNullName()
    {
        // arrange
        const string name = null;
        Dictionary<int, Card> actualResult = null;

        // act
        actualResult = _cardManager.GetCardsByCardName(name);

        // assert
        // do nothing

    }

    [TestMethod]
    public void TestGetCardMovesByCardNameWithValidName()
    {
        // arrange
        const string name = "test 1";
        const int keyCount = 2;
        const int valueCount = 2;
        Dictionary<int, List<MoveVM>> actualResult = null;

        // act
        actualResult = _cardManager.GetCardMovesByCardName(name);

        // assert
        Assert.AreEqual(keyCount, actualResult.Keys.Count);
        Assert.AreEqual(valueCount, actualResult.Values.Count);

    }

    [TestMethod]
    public void TestGetCardMovesByCardNameWithInvalidName()
    {
        // arrange
        const string name = "fail";
        const int keyCount = 0;
        const int valueCount = 0;
        Dictionary<int, List<MoveVM>> actualResult = null;

        // act
        actualResult = _cardManager.GetCardMovesByCardName(name);

        // assert
        Assert.AreEqual(keyCount, actualResult.Keys.Count);
        Assert.AreEqual(valueCount, actualResult.Values.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetCardMovesByCardNameThrowsArgumentNullExceptionWithNullName()
    {
        // arrange
        const string name = null;
        Dictionary<int, List<MoveVM>> actualResult = null;

        // act
        actualResult = _cardManager.GetCardMovesByCardName(name);

        // assert
        // do nothing

    }

    [TestMethod]
    public void TestGetCardAlternateArtsByCardNameWithValidName()
    {
        // arrange
        const string name = "test 1";
        const int keyCount = 2;
        const int valueCount = 2;
        Dictionary<int, List<string>> actualResult = null;

        // act
        actualResult = _cardManager.GetCardAlternateArtsByCardName(name);

        // assert
        Assert.AreEqual(keyCount, actualResult.Keys.Count);
        Assert.AreEqual(valueCount, actualResult.Values.Count);

    }

    [TestMethod]
    public void TestGetCardAlternateArtsByCardNameWithInvalidName()
    {
        // arrange
        const string name = "fail";
        const int keyCount = 0;
        const int valueCount = 0;
        Dictionary<int, List<string>> actualResult = null;

        // act
        actualResult = _cardManager.GetCardAlternateArtsByCardName(name);

        // assert
        Assert.AreEqual(keyCount, actualResult.Keys.Count);
        Assert.AreEqual(valueCount, actualResult.Values.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetCardAlternateArtsByCardNameThrowsArgumentNullExceptionWithNullName()
    {
        // arrange
        const string name = null;
        Dictionary<int, List<string>> actualResult = null;

        // act
        actualResult = _cardManager.GetCardAlternateArtsByCardName(name);

        // assert
        // do nothing

    }

    [TestMethod]
    public void TestGetCardVMsByCardNameWithValidName()
    {
        // arrange
        const string name = "test 1";
        const int count = 2;
        List<CardVM> actualResult = new List<CardVM>();

        // act
        actualResult = _cardManager.GetCardVMsByCardName(name);

        // assert
        Assert.AreEqual(count, actualResult.Count);

    }

    [TestMethod]
    public void TestGetCardVMsByCardNameWithInvalidName()
    {
        // arrange
        const string name = "fail";
        const int count = 0;

        List<CardVM> actualResult = new List<CardVM>();

        // act
        actualResult = _cardManager.GetCardVMsByCardName(name);

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetCardVMsByCardName()
    {
        // arrange
        const string name = null;
        List<CardVM> actualResult = null;

        // act
        actualResult = _cardManager.GetCardVMsByCardName(name);

        // assert
        // do nothing

    }
}

// need null and invalid
