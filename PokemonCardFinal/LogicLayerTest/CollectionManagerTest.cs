using DataAccessFakes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest;

[TestClass]
public class CollectionManagerTest
{
    ICollectionManager _collectionManager;
    [TestInitialize]
    public void TestSetup() 
    {
        _collectionManager = new CollectionManager(new CollectionAccessorFakes());
    }

    [TestMethod]
    public void TestGetCollectionCardsByCollectionIDWithValidInput()
    {
        // arrange
        const int collectionID = 1;
        const int count = 2;
        const int quantity2 = 4;
        List<CollectionCard> actual = new List<CollectionCard>();

        // act
        actual = _collectionManager.GetCollectionCardsByCollectionID(collectionID);

        //Assert
        Assert.AreEqual(count, actual.Count);
        Assert.AreEqual(quantity2, actual[1].Quantity);
    }

    [TestMethod]
    public void TestGetCollectionCardsByCollectionIDWithInvalidInput()
    {
        // arrange
        const int collectionID = 999;
        const int count = 0;
        List<CollectionCard> actual = new List<CollectionCard>();

        // act
        actual = _collectionManager.GetCollectionCardsByCollectionID(collectionID);

        //Assert
        Assert.AreEqual(count, actual.Count);
    }

    [TestMethod]
    public void TestGetCollectionElementsByCollectionIDWithValidInput()
    {
        // arrange
        const int collectionID = 1;
        const int count = 1;
        List<string> actual = new List<string>();

        // act
        actual = _collectionManager.GetCollectionElementsByCollectionID(collectionID);

        //Assert
        Assert.AreEqual(count, actual.Count);
    }

    [TestMethod]
    public void TestGetCollectionElementsByCollectionIDReturnsBlankList()
    {
        // arrange
        const int collectionID = 2;
        const int count = 0;
        List<string> actual = new List<string>();

        // act
        actual = _collectionManager.GetCollectionElementsByCollectionID(collectionID);

        //Assert
        Assert.AreEqual(count, actual.Count);
    }

    [TestMethod]
    public void TestGetCollectionElementsByCollectionIDWithInvalidInput()
    {
        // arrange
        const int collectionID = 999;
        const int count = 0;
        List<string> actual = new List<string>();

        // act
        actual = _collectionManager.GetCollectionElementsByCollectionID(collectionID);

        //Assert
        Assert.AreEqual(count, actual.Count);
    }

    [TestMethod]
    public void TestGetCollectionTypeMaxSizeWithValidInput() 
    {
        // arrange
        const string collectionType = "type1";
        const int expected = 100;
        int actual = 0;

        // act
        actual = _collectionManager.GetCollectionTypeMaxSize(collectionType);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestGetCollectionTypeMaxSizeThrowsApplicationExceptionWithInvalidInput() 
    {
        // arrange
        const string collectionType = "fail";
        const int expected = 100;
        int actual = 0;

        // act
        actual = _collectionManager.GetCollectionTypeMaxSize(collectionType);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetCollectionTypeMaxSizeThrowsArgumentNullExceptionWithBlankinput() 
    {
        // arrange
        const string collectionType = " ";
        const int expected = 100;
        int actual = 0;

        // act
        actual = _collectionManager.GetCollectionTypeMaxSize(collectionType);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetCollectionTypeMaxSizeThrowsArgumentNullExceptionWithNullInput() 
    {
        // arrange
        const string collectionType = null;
        const int expected = 100;
        int actual = 0;

        // act
        actual = _collectionManager.GetCollectionTypeMaxSize(collectionType);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestGetCollectionByCollectionIDWithValidInput()
    {
        // arrange
        const int collectionID = 1;
        const int userID = 1;
        const string collectionType = "type1";
        const string name = "test";
        Collection actual = null;

        // act
        actual = _collectionManager.GetCollectionByCollectionID(collectionID);

        //Assert
        Assert.AreEqual(userID, actual.UserID);
        Assert.AreEqual(collectionType, actual.CollectionTypeID);
        Assert.AreEqual(name, actual.Name);
    }

    [TestMethod]
    public void TestGetCollectionByCollectionIDReturnsNullWithInvalidInput()
    {
        // arrange
        const int collectionID = 999;
        Collection expected = null;
        Collection actual = new Collection();

        // act
        actual = _collectionManager.GetCollectionByCollectionID(collectionID);

        //Assert
        Assert.AreEqual(expected,actual);
    }

    [TestMethod]
    public void TestGetCollectionVMByCollectionIDWithValidInput()
    {
        // arrange
        const int collectionID = 1;
        const int userID = 1;
        const string collectionType = "type1";
        const string name = "test";
        const int cardCount = 2;
        const int maxSize = 100;
        CollectionVM actual = null;

        // act
        actual = _collectionManager.GetCollectionVMByCollectionID(collectionID);

        //Assert
        Assert.AreEqual(userID, actual.UserID);
        Assert.AreEqual(collectionType, actual.CollectionTypeID);
        Assert.AreEqual(name, actual.Name);
        Assert.AreEqual(cardCount, actual.Cards.Count);
        Assert.AreEqual(maxSize, actual.MaxSize);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestGetCollectionVMByCollectionIDReturnsNullWithInvalidInput()
    {
        // arrange
        const int collectionID = 999;
        CollectionVM actual = new CollectionVM();

        // act
        actual = _collectionManager.GetCollectionVMByCollectionID(collectionID);

        //Assert
        // do nothing
    }
}