using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest;

[TestClass]
public class FilterCardManagerTest
{
    IFilterCardManager _filterCardManager;
    List<Card> _cards;

    [TestInitialize]
    public void TestSetup()
    {
        _filterCardManager = new FilterCardManager();
        _cards = new List<Card>();
        _cards.Add(new Card()
        {
            BoosterID = "Booster 1",
            ElementTypeID = "Element Type 1",
            Name = "Name 1",
            CardType = "Card Type 1",
            Rarity = "Common",
        });
        _cards.Add(new Card()
        {
            BoosterID = "Booster 2",
            ElementTypeID = "Element Type 1",
            Name = "Name 2",
            CardType = "Card Type 2",
            Rarity = "Common",
        });
        _cards.Add(new Card()
        {
            BoosterID = "Booster 1",
            ElementTypeID = "Element Type 3",
            Name = "Name 3",
            CardType = "Card Type 1",
            Rarity = "Common",
        });
        _cards.Add(new Card()
        {
            BoosterID = "Booster 1",
            ElementTypeID = "Element Type 3",
            Name = "Name 1",
            CardType = "Card Type 2",
            Rarity = "Rare",
        });
    }

    [TestMethod]
    public void TestFilterByCardNameWithValidInput()
    {
        // arrange
        const string name = "1";
        const int expectedCount = 2;
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByCardName(_cards, name).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestFilterByCardNameReturnsBlankListWithInvalidName()
    {
        // arrange
        const string name = "fails";
        const int expectedCount = 0;
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByCardName(_cards, name).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestFilterByCardNameThrowsArgumentNullExceptionWithBlankName()
    {
        // arrange
        const string name = "";
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByCardName(_cards, name).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestFilterByCardNameThrowsArgumentNullExceptionWithNullName()
    {
        // arrange
        const string name = null;
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByCardName(_cards, name).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestFilterByCardNameThrowsArgumentNullExceptionWithNullCardList()
    {
        // arrange
        const string name = "1";
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByCardName(null, name).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestFilterByRarityWithValidInput()
    {
        // arrange
        const string rarity = "common";
        const int expectedCount = 3;
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByRarity(_cards, rarity).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestFilterByRarityReturnsBlankListWithInvalidRarity()
    {
        // arrange
        const string rarity = "fails";
        const int expectedCount = 0;
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByRarity(_cards, rarity).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestFilterByRarityThrowsArgumentNullExceptionWithBlankRarity()
    {
        // arrange
        const string rarity = "";
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByRarity(_cards, rarity).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestFilterByRarityThrowsArgumentNullExceptionWithNullRarity()
    {
        // arrange
        const string rarity = null;
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByRarity(_cards, rarity).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestFilterByRarityThrowsArgumentNullExceptionWithNullCardList()
    {
        // arrange
        const string rarity = "1";
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByRarity(null, rarity).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestFilterByBoosterIDWithValidInput()
    {
        // arrange
        const string boosterID = "Booster 1";
        const int expectedCount = 3;
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByBoosterID(_cards, boosterID).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestFilterByBoosterIDReturnsBlankListWithInvalidBoosterID()
    {
        // arrange
        const string boosterID = "fails";
        const int expectedCount = 0;
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByBoosterID(_cards, boosterID).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestFilterByBoosterIDThrowsArgumentNullExceptionWithBlankBoosterID()
    {
        // arrange
        const string boosterID = "";
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByBoosterID(_cards, boosterID).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestFilterByBoosterIDThrowsArgumentNullExceptionWithNullBoosterID()
    {
        // arrange
        const string boosterID = null;
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByBoosterID(_cards, boosterID).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestFilterByBoosterIDThrowsArgumentNullExceptionWithNullCardList()
    {
        // arrange
        const string boosterID = "Booster 1";
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByBoosterID(null, boosterID).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestFilterByCardTypeWithValidInput()
    {
        // arrange
        const string cardType = "Card Type 1";
        const int expectedCount = 2;
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByCardType(_cards, cardType).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestFilterByCardTypeReturnsBlankListWithInvalidCardType()
    {
        // arrange
        const string cardType = "fails";
        const int expectedCount = 0;
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByCardType(_cards, cardType).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestFilterByCardTypeThrowsArgumentNullExceptionWithBlankCardType()
    {
        // arrange
        const string cardType = "";
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByCardType(_cards, cardType).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestFilterByCardTypeThrowsArgumentNullExceptionWithNullCardType()
    {
        // arrange
        const string cardType = null;
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByCardType(_cards, cardType).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestFilterByCardTypeThrowsArgumentNullExceptionWithNullCardList()
    {
        // arrange
        const string cardType = "Card Type 1";
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByCardType(null, cardType).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestFilterByElementTypeIDWithValidInput()
    {
        // arrange
        const string elementTypeID = "Element Type 3";
        const int expectedCount = 2;
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByElementTypeID(_cards, elementTypeID).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestFilterByElementTypeIDReturnsBlankListWithInvalidElement()
    {
        // arrange
        const string elementTypeID = "fails";
        const int expectedCount = 0;
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByElementTypeID(_cards, elementTypeID).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestFilterByElementTypeIDThrowsArgumentNullExceptionWithBlankElement()
    {
        // arrange
        const string elementTypeID = "";
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByElementTypeID(_cards, elementTypeID).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestFilterByElementTypeIDThrowsArgumentNullExceptionWithNullElement()
    {
        // arrange
        const string elementTypeID = null;
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByElementTypeID(_cards, elementTypeID).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestFilterByElementTypeIDThrowsArgumentNullExceptionWithNullCardList()
    {
        // arrange
        const string elementTypeID = "Element Type 3";
        List<Card> actual;

        // act
        actual = _filterCardManager.FilterByElementTypeID(null, elementTypeID).ToList();

        // assert
        // do nothing
    }
}
