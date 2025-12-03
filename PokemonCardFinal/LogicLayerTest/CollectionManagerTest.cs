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

    [TestMethod]
    public void TestGetCollectionIDByCollectionTypeReturnsValidID()
    {
        // arrange
        const string collectionType = "testType1";
        UserVM user = new UserVM()
        {
            Collections = new List<Collection>() 
            {
                new Collection()
                {
                    CollectionID = 1,
                    CollectionTypeID = "testType1"
                },
                new Collection()
                {
                    CollectionID = 1,
                    CollectionTypeID = "testType2"
                }
            }
        };
        const int expected = 1;
        int actual = 0;

        // act
        actual = _collectionManager.GetCollectionIDByCollectionType(user, collectionType);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException (typeof(ApplicationException))]
    public void TestGetCollectionIDByCollectionTypeThrowsAppplicationExceptionWithInvalidCollectionType()
    {
        // arrange
        const string collectionType = "fail";
        UserVM user = new UserVM()
        {
            Collections = new List<Collection>() 
            {
                new Collection()
                {
                    CollectionID = 1,
                    CollectionTypeID = "testType1"
                },
                new Collection()
                {
                    CollectionID = 2,
                    CollectionTypeID = "testType2"
                },
            }
        };
        const int expected = 1;
        int actual = 0;

        // act
        actual = _collectionManager.GetCollectionIDByCollectionType(user, collectionType);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestConvertCollectionCardToVMWithValidList()
    {
        // arrange
        List<CollectionCard> collectionCards = new List<CollectionCard>();
        const int collectionID = 1;
        const int count = 2;
        const string cardName1 = "test1";
        const string rarity2 = "rarity2";
        List<CollectionCardVM> actual = new List<CollectionCardVM>();

        // act
        collectionCards = _collectionManager.GetCollectionCardsByCollectionID(1);
        actual = _collectionManager.ConvertCollectionCardToVM(collectionCards);

        // assert
        Assert.AreEqual(count, actual.Count);
        Assert.AreEqual(cardName1, actual[0].Name);
        Assert.AreEqual(rarity2, actual[1].Rarity);
    }

    [TestMethod]
    public void TestConvertCollectionCardToVMWithEmptyInput()
    {
        // arrange
        List<CollectionCard> collectionCards = new List<CollectionCard>();
        const int count = 0;
        List<CollectionCardVM> actual = new List<CollectionCardVM>();

        // act
        actual = _collectionManager.ConvertCollectionCardToVM(collectionCards);

        // assert
        Assert.AreEqual(count, actual.Count);
    }

    [TestMethod]
    [ExpectedException (typeof(ApplicationException))]
    public void TestConvertCollectionCardToVMThrowsApplicationExceptionWithNullInput()
    {
        // arrange
        List<CollectionCard> collectionCards = null;
        List<CollectionCardVM> actual = new List<CollectionCardVM>();

        // act
        actual = _collectionManager.ConvertCollectionCardToVM(collectionCards);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetDeckElementsByUserIDWithValidInput() 
    {
        // arrange
        const int userID = 1;
        const int keyCount = 2;
        const int valueCount = 2;
        Dictionary<int, List<string>> actual = new Dictionary<int, List<string>>();

        // act
        actual = _collectionManager.GetDeckElementsByUserID(userID);

        // assert
        Assert.AreEqual(keyCount, actual.Keys.Count);
        Assert.AreEqual(valueCount, actual.Values.Count);
    }

    [TestMethod]
    public void TestGetDeckElementsByUserIDWithInvalidInput() 
    {
        // arrange
        const int userID = 999;
        const int keyCount = 0;
        const int valueCount = 0;
        Dictionary<int, List<string>> actual = new Dictionary<int, List<string>>();

        // act
        actual = _collectionManager.GetDeckElementsByUserID(userID);

        // assert
        Assert.AreEqual(keyCount, actual.Keys.Count);
        Assert.AreEqual(valueCount, actual.Values.Count);
    }
}