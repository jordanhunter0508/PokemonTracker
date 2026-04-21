using DataAccessFakes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest;

[TestClass]
public class SearchManagerTest
{
    ISearchManager _searchManager;

    [TestInitialize]
    public void TestSetup()
    {
        _searchManager = new SearchManager(new SearchAccessorFakes());
    }

    [TestMethod]
    public void TestSearchCardsByNameReturnsListWithValidName()
    {
        // arrange
        const string name = "Test";
        const int expected = 3;
        List<Card> acutal;

        // act
        acutal = _searchManager.SearchCardsByName(name);

        // assert
        Assert.AreEqual(expected, acutal.Count);
    }

    [TestMethod]
    public void TestSearchCardsByNameReturnsEmptyListWithInvalidName()
    {
        // arrange
        const string name = "fails";
        const int expected = 0;
        List<Card> acutal;

        // act
        acutal = _searchManager.SearchCardsByName(name);

        // assert
        Assert.AreEqual(expected, acutal.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestSearchCardsByNameThrowsArgumentNullExceptionWithBlankName()
    {
        // arrange
        const string name = "";
        List<Card> acutal;

        // act
        acutal = _searchManager.SearchCardsByName(name);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestSearchCardsByNameThrowsArgumentNullExceptionWithNullName()
    {
        // arrange
        const string name = null;
        List<Card> acutal;

        // act
        acutal = _searchManager.SearchCardsByName(name);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetCardsbyNameReturnsList()
    {
        // arrange
        FilterOption filterOption = new FilterOption()
        {
            CardName = "Test Name 1"
        };
        const int expectedCount = 2;
        List<Card> actual;

        // act
        actual = _searchManager.GetCards(filterOption);

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestGetCardsbyBoosterIDReturnsList()
    {
        // arrange
        FilterOption filterOption = new FilterOption()
        {
            BoosterID = "Booster 2"
        };
        const int expectedCount = 3;
        List<Card> actual;

        // act
        actual = _searchManager.GetCards(filterOption);

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestGetCardsbyRarityReturnsList()
    {
        // arrange
        FilterOption filterOption = new FilterOption()
        {
            Rarity = "Uncommon"
        };
        const int expectedCount = 2;
        List<Card> actual;

        // act
        actual = _searchManager.GetCards(filterOption);

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestGetCardsbyCardTypeReturnsList()
    {
        // arrange
        FilterOption filterOption = new FilterOption()
        {
            CardType = "Trainer"
        };
        const int expectedCount = 2;
        List<Card> actual;

        // act
        actual = _searchManager.GetCards(filterOption);

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestGetCardsbyElementTypeIDReturnsList()
    {
        // arrange
        FilterOption filterOption = new FilterOption()
        {
            ElementTypeID = "Element 2"
        };
        const int expectedCount = 1;
        List<Card> actual;

        // act
        actual = _searchManager.GetCards(filterOption);

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestGetCardsbyArtistIDReturnsList()
    {
        // arrange
        FilterOption filterOption = new FilterOption()
        {
            ArtistID = 1
        };
        const int expectedCount = 3;
        List<Card> actual;

        // act
        actual = _searchManager.GetCards(filterOption);

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestGetCardsWtihMultipleFiltersReturnsList()
    {
        // arrange
        FilterOption filterOption = new FilterOption()
        {
            Rarity = "Uncommon",
            ArtistID = 1
        };
        const int expectedCount = 2;
        List<Card> actual;

        // act
        actual = _searchManager.GetCards(filterOption);

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestGetCardsReturnsBlankListWithInvaliFilterOption()
    {
        // arrange
        FilterOption filterOption = new FilterOption()
        {
            CardType = "fail",
        };
        const int expectedCount = 0;
        List<Card> actual;

        // act
        actual = _searchManager.GetCards(filterOption);

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }
}
