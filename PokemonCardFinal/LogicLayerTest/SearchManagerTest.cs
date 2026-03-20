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
}
