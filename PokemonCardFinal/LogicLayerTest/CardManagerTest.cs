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
            ElementTypeID = "test element 1",
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
        Dictionary<int, Card> actualResult = null;

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
        Dictionary<int, List<MoveVM>> actualResult = null;

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
    public void TestGetCardVMsByCardNameThrowsArgumentNullExceptionWithNullName()
    {
        // arrange
        const string name = null;
        List<CardVM> actualResult = null;

        // act
        actualResult = _cardManager.GetCardVMsByCardName(name);

        // assert
        // do nothing

    }

    [TestMethod]
    public void TestGetCardVMsByCardNameWithValidIEnumerable()
    {
        // arrange
        const string name = "test 1";
        const int count = 2;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByCardName(cards, name).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
        Assert.AreEqual(cardID2, actualResult[1].CardID);
    }

    [TestMethod]
    public void TestGetCardVMsByCardNameWithEmptyIEnumerable()
    {
        // arrange
        const string name = "failed";
        const int count = 0;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        actualResult = _cardManager.GetCardVMsByCardName(cards, name).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    public void TestGetCardVMsByCardNameWithEmptyRarity()
    {
        // arrange
        const string name = "";
        const int count = 3;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByCardName(cards, name).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    public void TestGetCardVMsByCardNameWithInvalidRarity()
    {
        // arrange
        const string name = "Failed";
        const int count = 0;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByCardName(cards, name).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetCardVMsByCardNameThrowsArgumentNullExceptionWithNullIEnumberable()
    {
        // arrange
        const string name = "test 1";
        List<CardVM> cards = null;
        List<CardVM> actualResult = new List<CardVM>();

        // act
        actualResult = _cardManager.GetCardVMsByCardName(cards, name).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetCardVMsByCardNameThrowsArgumentNullExceptionWithNullInput()
    {
        // arrange
        const string name = null;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByCardName(cards, name).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetCardVMsByRarityWithValidIEnumerable()
    {
        // arrange
        const string rarity = "test rarity 1";
        const int count = 2;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByRarity(cards, rarity).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
        Assert.AreEqual(cardID2, actualResult[1].CardID);
    }

    [TestMethod]
    public void TestGetCardVMsByRarityWithEmptyIEnumerable()
    {
        // arrange
        const string rarity = "test rarity 1";
        const int count = 0;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        actualResult = _cardManager.GetCardVMsByRarity(cards, rarity).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    public void TestGetCardVMsByRarityWithEmptyRarity()
    {
        // arrange
        const string rarity = "";
        const int count = 0;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByRarity(cards, rarity).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    public void TestGetCardVMsByRarityWithInvalidRarity()
    {
        // arrange
        const string rarity = "Failed";
        const int count = 0;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByRarity(cards, rarity).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetCardVMsByRarityThrowsArgumentNullExceptionWithNullIEnumberable()
    {
        // arrange
        const string rarity = "test rarity 1";
        List<CardVM> cards = null;
        List<CardVM> actualResult = new List<CardVM>();

        // act
        actualResult = _cardManager.GetCardVMsByRarity(cards, rarity).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetCardVMsByRarityThrowsArgumentNullExceptionWithNullInput()
    {
        // arrange
        const string rarity = null;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByRarity(cards, rarity).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetCardVMsByBoosterIDWithValidIEnumerable()
    {
        // arrange
        const string rarity = "test booster 1";
        const int count = 2;
        const int cardID2 = 2;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByBoosterID(cards, rarity).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
        Assert.AreEqual(cardID2, actualResult[1].CardID);
    }

    [TestMethod]
    public void TestGetCardVMsByBoosterIDWithEmptyIEnumerable()
    {
        // arrange
        const string rarity = "test booster 1";
        const int count = 0;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        actualResult = _cardManager.GetCardVMsByBoosterID(cards, rarity).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    public void TestGetCardVMsByBoosterIDWithEmptyRarity()
    {
        // arrange
        const string rarity = "";
        const int count = 0;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByBoosterID(cards, rarity).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    public void TestGetCardVMsByBoosterIDWithInvalidRarity()
    {
        // arrange
        const string rarity = "Failed";
        const int count = 0;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByBoosterID(cards, rarity).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetCardVMsByBoosterIDThrowsArgumentNullExceptionWithNullIEnumberable()
    {
        // arrange
        const string rarity = "test booster 1";
        List<CardVM> cards = null;
        List<CardVM> actualResult = new List<CardVM>();

        // act
        actualResult = _cardManager.GetCardVMsByBoosterID(cards, rarity).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetCardVMsByBoosterIDThrowsArgumentNullExceptionWithNullInput()
    {
        // arrange
        const string rarity = null;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByBoosterID(cards, rarity).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetCardVMsByCardTypeWithValidIEnumerable()
    {
        // arrange
        const string rarity = "test type 1";
        const int count = 2;
        const int cardID2 = 2;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByCardType(cards, rarity).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
        Assert.AreEqual(cardID2, actualResult[1].CardID);
    }

    [TestMethod]
    public void TestGetCardVMsByCardTypeWithEmptyIEnumerable()
    {
        // arrange
        const string rarity = "test type 1";
        const int count = 0;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        actualResult = _cardManager.GetCardVMsByCardType(cards, rarity).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    public void TestGetCardVMsByCardTypeWithEmptyRarity()
    {
        // arrange
        const string rarity = "";
        const int count = 0;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByCardType(cards, rarity).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    public void TestGetCardVMsByCardTypeWithInvalidRarity()
    {
        // arrange
        const string rarity = "Failed";
        const int count = 0;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByCardType(cards, rarity).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetCardVMsByCardTypeThrowsArgumentNullExceptionWithNullIEnumberable()
    {
        // arrange
        const string rarity = "test type 1";
        List<CardVM> cards = null;
        List<CardVM> actualResult = new List<CardVM>();

        // act
        actualResult = _cardManager.GetCardVMsByCardType(cards, rarity).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetCardVMsByCardTypeThrowsArgumentNullExceptionWithNullInput()
    {
        // arrange
        const string rarity = null;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByCardType(cards, rarity).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetCardVMsByElementTypeIDWithValidIEnumerable()
    {
        // arrange
        const string rarity = "test element 1";
        const int count = 2;
        const int cardID2 = 2;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByElementTypeID(cards, rarity).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
        Assert.AreEqual(cardID2, actualResult[1].CardID);
    }

    [TestMethod]
    public void TestGetCardVMsByElementTypeIDWithEmptyIEnumerable()
    {
        // arrange
        const string rarity = "test element";
        const int count = 0;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        actualResult = _cardManager.GetCardVMsByElementTypeID(cards, rarity).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    public void TestGetCardVMsByElementTypeIDWithEmptyRarity()
    {
        // arrange
        const string rarity = "";
        const int count = 0;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByElementTypeID(cards, rarity).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    public void TestGetCardVMsByElementTypeIDWithInvalidRarity()
    {
        // arrange
        const string rarity = "Failed";
        const int count = 0;
        const int cardID2 = 3;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByElementTypeID(cards, rarity).ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetCardVMsByElementTypeIDThrowsArgumentNullExceptionWithNullIEnumberable()
    {
        // arrange
        const string rarity = "test element";
        List<CardVM> cards = null;
        List<CardVM> actualResult = new List<CardVM>();

        // act
        actualResult = _cardManager.GetCardVMsByElementTypeID(cards, rarity).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetCardVMsByElementTypeIDThrowsArgumentNullExceptionWithNullInput()
    {
        // arrange
        const string rarity = null;
        List<CardVM> cards = new List<CardVM>();
        List<CardVM> actualResult = new List<CardVM>();

        // act
        cards = _cardManager.GetCardVMs();
        actualResult = _cardManager.GetCardVMsByElementTypeID(cards, rarity).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetCardsByReleaseDateWithValidReleaseDate()
    {
        // arrange
        DateTime releaseDate = DateTime.Parse("2025-11-06");
        const int count = 2;
        List<Card> actual = null;

        // act
        actual = _cardManager.GetCardsByReleaseDate(releaseDate);

        // assert
        Assert.AreEqual(count, actual.Count);
    }

    [TestMethod]
    public void TestGetCardsByReleaseDateWithInvalidReleaseDate()
    {
        // arrange
        DateTime releaseDate = DateTime.Parse("1999-01-01");
        const int count = 0;
        List<Card> actual = null;

        // act
        actual = _cardManager.GetCardsByReleaseDate(releaseDate);

        // assert
        Assert.AreEqual(count, actual.Count);
    }

    [TestMethod]
    public void TestAddCardAlternateArtReturnsTrueWithValidInput()
    {
        // arrange
        const int cardID = 1;
        const string alternateArtID = "test Alternate Art 3";
        const bool expected = true;
        bool actual = false;

        // act
        actual = _cardManager.AddCardAlternateArt(cardID, alternateArtID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardAlternateArtThrowsApplicationExceptionWithInvalidCardID()
    {
        // arrange
        const int cardID = 999;
        const string alternateArtID = "test Alternate Art 3";
        const bool expected = false;
        bool actual = true;

        // act
        actual = _cardManager.AddCardAlternateArt(cardID, alternateArtID);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardAlternateArtThrowsApplicationExceptionWithInvalidAltArtID()
    {
        // arrange
        const int cardID = 1;
        const string alternateArtID = "failed";
        const bool expected = false;
        bool actual = true;

        // act
        actual = _cardManager.AddCardAlternateArt(cardID, alternateArtID);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAddCardAlternateArtThrowsArgumentNullExceptionWithNullString()
    {
        // arrange
        const int cardID = 1;
        const string alternateArtID = null;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _cardManager.AddCardAlternateArt(cardID, alternateArtID);

        // assert
        // do nothing
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardAlternateArtThrowsApplicationExceptionWithDuplicateInput()
    {
        // arrange
        const int cardID = 1;
        const string alternateArtID = "test Alternate Art 1";
        const bool expected = true;
        bool actual = false;

        // act
        actual = _cardManager.AddCardAlternateArt(cardID, alternateArtID);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestDeleteCardAlternateArtReturnsTrueWithValidInput()
    {
        // arrange
        const int cardID = 1;
        const string alternateArtID = "test Alternate Art 1";
        const bool expected = true;
        bool actual = false;

        // act
        actual = _cardManager.DeleteCardAlternateArt(cardID, alternateArtID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestDeleteCardAlternateArtReturnsFalseWithInvalidCardID()
    {
        // arrange
        const int cardID = 999;
        const string alternateArtID = "test Alternate Art 1";
        const bool expected = false;
        bool actual = true;

        // act
        actual = _cardManager.DeleteCardAlternateArt(cardID, alternateArtID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestDeleteCardAlternateArtReturnsFalseWithInvalidAltArtID()
    {
        // arrange
        const int cardID = 1;
        const string alternateArtID = "failed";
        const bool expected = false;
        bool actual = true;

        // act
        actual = _cardManager.DeleteCardAlternateArt(cardID, alternateArtID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestDeleteCardAlternateArtThrowsArgumentNullExceptionWithNullString()
    {
        // arrange
        const int cardID = 1;
        const string alternateArtID = null;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _cardManager.DeleteCardAlternateArt(cardID, alternateArtID);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestAddCardMoveReturnsTrueWithValidInput()
    {
        // arrange
        const int cardID = 1;
        const int moveID = 3;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _cardManager.AddCardMove(cardID, moveID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardMoveThrowsApplicationExceptionWithInvalidCardID()
    {
        // arrange
        const int cardID = 999;
        const int moveID = 1;
        bool actual = false;

        // act
        actual = _cardManager.AddCardMove(cardID, moveID);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardMoveThrowsApplicationExceptionWithMoveID()
    {
        // arrange
        const int cardID = 1;
        const int moveID = 999;
        bool actual = false;

        // act
        actual = _cardManager.AddCardMove(cardID, moveID);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardMoveThrowsApplicationExceptionWithDuplicateID()
    {
        // arrange
        const int cardID = 1;
        const int moveID = 1;
        bool actual = false;

        // act
        actual = _cardManager.AddCardMove(cardID, moveID);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestDeleteCardMoveReturnsTrueWithValidInput()
    {
        // arrange
        const int cardID = 1;
        const int moveID = 1;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _cardManager.DeleteCardMove(cardID, moveID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestDeleteCardMoveReturnsFalseWithValidCardID()
    {
        // arrange
        const int cardID = 999;
        const int moveID = 1;
        const bool expected = false;
        bool actual = true;

        // act
        actual = _cardManager.DeleteCardMove(cardID, moveID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestDeleteCardMoveReturnsFalseWithValidMoveID()
    {
        // arrange
        const int cardID = 1;
        const int moveID = 999;
        const bool expected = false;
        bool actual = true;

        // act
        actual = _cardManager.DeleteCardMove(cardID, moveID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestAddCardWithReturnsTrueValidInput()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 5,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        const int expected = 5;
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionWithInvalidArtistID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 999,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionWithInvalidAbilityID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "failed",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionWithInvalidBoosterID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "failed",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionWithInvalidRuleID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "failed",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionWithInvalidElementID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "failed",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionWithUniqueKey()
    {
        // BoosterId, BoosterNumber and Rarity are already used.

        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 1,
            AbilityID = "test ability 1",
            BoosterID = "test booster 1",
            PokemonRuleID = "test pokemon rule 1",
            ElementTypeID = "test element 1",
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
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullAbilityID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = null,
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullBoosterID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = null,
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullRuleID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = null,
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullElementID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = null,
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullName()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = null,
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullCardType()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = null,
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullRarity()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = null,
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullWeaknessType()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = null,
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullResistanceType()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = null,
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullStage()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = null
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullCard()
    {
        // arrange
        Card card = null;
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionBlankCard()
    {
        // arrange
        Card card = new Card();
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    public void TestEditCardWithReturnsTrueValidInput()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        const bool expected = true;
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionWithInvalidArtistID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 999,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionWithInvalidAbilityID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "failed",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionWithInvalidBoosterID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "failed",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionWithInvalidRuleID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "failed",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionWithInvalidElementID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "failed",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionWithUniqueKey()
    {
        // BoosterId, BoosterNumber and Rarity are already used.

        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 1,
            AbilityID = "test ability 1",
            BoosterID = "test booster 1",
            PokemonRuleID = "test pokemon rule 1",
            ElementTypeID = "test element 1",
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
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullAbilityID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = null,
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullBoosterID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = null,
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullRuleID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = null,
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullElementID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = null,
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullName()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = null,
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullCardType()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = null,
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullRarity()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = null,
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullWeaknessType()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = null,
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullResistanceType()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = null,
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullStage()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = null
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullCard()
    {
        // arrange
        Card card = null;
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionBlankCard()
    {
        // arrange
        Card card = new Card();
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    public void TestAddCardVMReturnsTrueWithValidCardVM()
    {
        // arrange
        CardVM cardVM = new CardVM()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 2",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 1",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage",
            Moves = new List<MoveVM>()
            { 
                new MoveVM()
                {
                    MoveID = 1,
                },
                new MoveVM()
                {
                    MoveID = 2,
                }
            },
            AlternateArts = new List<string>()
            {
                "test Alternate Art 1"
            },
        };
        const bool expectd = true;
        bool actual = false;

        // act
        actual = _cardManager.AddCardVM(cardVM);

        // assert
        Assert.AreEqual(expectd, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAddCardVMThrowsArgumentNullExceptionWithValidCardVM()
    {
        // arrange
        CardVM cardVM = null;
        bool actual = false;

        // act
        actual = _cardManager.AddCardVM(cardVM);

        // assert
        // do nothing
    }
}
