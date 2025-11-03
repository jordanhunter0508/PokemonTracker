using DataAccessFakes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest;

[TestClass]
public class ElementManagerTest
{
    IElementManager _elementManager;

    [TestInitialize]
    public void StartUp() 
    {
        _elementManager = new ElementManager(new ElementAccessorFakes());
    }

    [TestMethod]
    public void TestGetElementTypeByElementTypeIDWithValidID()
    {
        // arrrange
        const string elementTypeID = "testElement1";
        const string expectedID = "testElement1";
        const string expectedDescription = "Description test 1.";
        ElementType actualElement;

        // act
        actualElement = _elementManager.GetElementTypeByElementTypeID(elementTypeID);

        // assert
        Assert.AreEqual(expectedID, actualElement.ElementTypeID);
        Assert.AreEqual(expectedDescription, actualElement.Description);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestGetElementTypeByElementTypeIDThrowsApplicationException()
    {
        // arrrange
        const string elementTypeID = "ttest";
        const string expectedID = null;
        const string expectedDescription = null;
        ElementType actualElement = null;

        // act
        actualElement = _elementManager.GetElementTypeByElementTypeID(elementTypeID);

        // assert
        Assert.AreEqual(expectedID, actualElement.ElementTypeID);
        Assert.AreEqual(expectedDescription, actualElement.Description);
    }

    [TestMethod]
    public void TestGetElementTypes()
    {
        // arrange
        const int expectedLength = 3;
        const string expectedID1 = "testElement1";
        const string expectedID2 = "testElement2";
        List<ElementType> elementTypes = null;

        // act
        elementTypes = _elementManager.GetElementTypes();

        // assert
        Assert.AreEqual(expectedLength, elementTypes.Count);
        Assert.AreEqual(expectedID1, elementTypes[0].ElementTypeID);
        Assert.AreEqual(expectedID2, elementTypes[1].ElementTypeID);
    }

    [TestMethod]
    public void TestCreateElementTypeWithValidInput() 
    {
        // arrange
        const string elementTypeID = "Cool New Element";
        const string description = "This element is new and it is cool.";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _elementManager.CreateElementType(elementTypeID, description);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestCreateElementTypeThrowsApplicationExceptionWithDuplicateID()
    {
        // arrange
        const string elementTypeID = "Cool New Element";
        const string description = "This element is new and it is cool.";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _elementManager.CreateElementType(elementTypeID, description);
        actualResult = _elementManager.CreateElementType(elementTypeID, description);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestUpdateElementDescriptionByElementTypeIDReturnsTrue()
    {
        // arrange
        const string elementID = "testElement1";
        const string oldDescription = "Description test 1.";
        const string newDescription = "This is a new description";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _elementManager.UpdateElementDescritpionByElementTypeID(elementID, newDescription);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
        Assert.AreNotEqual(oldDescription, newDescription);
    }

    [TestMethod]
    public void TestUpdateElementDescriptionByElementTypeIDReturnsFalse()
    {
        // arrange
        const string elementID = "no";
        const string oldDescription = "Description test 1.";
        const string newDescription = "This is a new description";
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _elementManager.UpdateElementDescritpionByElementTypeID(elementID, newDescription);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
        Assert.AreNotEqual(oldDescription, newDescription);
    }

    [TestMethod]
    public void TestDeleteElementTypeByElementTypeIDReturnsTrueWithValidInput()
    {
        // arrange
        const string elementID = "testElement1";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _elementManager.DeleteElementTypeByElementTypeID(elementID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeleteElementTypeByElementTypeIDReturnsFalseWithInvalidInput()
    {
        // arrange
        const string elementID = "no";
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _elementManager.DeleteElementTypeByElementTypeID(elementID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestFormatElemetTypesReturnCorrectOrder() 
    {
        // arrange
        List<ElementType> inputList = new List<ElementType>();
        inputList.Add(new ElementType()
        {
            ElementTypeID = "test",
            Description = "test",
        });
        inputList.Add(new ElementType()
        {
            ElementTypeID = "another test",
            Description = "another test",
        });

        List<ElementType> expectedResult = new List<ElementType>();
        expectedResult.Add(new ElementType()
        {
            ElementTypeID = "Another test",
            Description = "another test",
        });
        expectedResult.Add(new ElementType()
        {
            ElementTypeID = "Test",
            Description = "test",
        });
      
        List<ElementType> actualResult = new List<ElementType>();

        // act
        actualResult = _elementManager.FormatElemetTypes(inputList).ToList();

        // assert
        Assert.AreEqual(expectedResult[0].ElementTypeID, actualResult[0].ElementTypeID);
        Assert.AreEqual(expectedResult[1].ElementTypeID, actualResult[1].ElementTypeID);
    }
}
