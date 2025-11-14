using DataAccessFakes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;
using static System.Net.Mime.MediaTypeNames;

namespace LogicLayerTest;

[TestClass]
public class MoveManagerTest
{
    IMoveManager _moveManager;

    [TestInitialize]
    public void TestSetUp()
    {
        _moveManager = new MoveManager(new MoveAccessorFakes());
    }

    [TestMethod]
    public void TestGetMoveByMoveIDWithValidMoveID()
    {
        // arrange
        const string inputedMoveID = "test move 1";
        const string expectedMoveID = "test move 1";
        const int expectedDamage = 10;
        const string expectedDescription = "This is a test move.";
        Move actualMove = null;

        // act
        actualMove = _moveManager.GetMoveByMoveID(inputedMoveID);

        // assert
        Assert.AreEqual(expectedMoveID, actualMove.MoveID);
        Assert.AreEqual(expectedDamage, actualMove.Damage);
        Assert.AreEqual(expectedDescription, actualMove.Description);
    }

    [TestMethod]
    public void TestGetMoveByMoveIDWithInvalidInputReturnsEmptyList()
    {
        // arrange
        const string inputedMoveID = "test";
        Move expectedMove = null;
        Move actualMove;

        // act
        actualMove = _moveManager.GetMoveByMoveID(inputedMoveID);

        // assert
        Assert.AreEqual(expectedMove, actualMove);
    }

    [TestMethod]
    public void TestGetMoveCostByMoveIDWithValidMoveID()
    {
        // arrange
        const string moveID = "test move 1";
        const int expectedCount = 2;
        const string elementType1 = "element";
        const string elementType2 = "test element";
        List<MoveCost> actualMoveCost;

        // act
        actualMoveCost = _moveManager.GetMoveCostsByMoveID(moveID);

        // assert
        Assert.AreEqual(expectedCount, actualMoveCost.Count);
        Assert.AreEqual(elementType1, actualMoveCost[0].ElementType);
        Assert.AreEqual(elementType2, actualMoveCost[1].ElementType);
    }

    [TestMethod]
    public void TestGetMoveCostByMoveIDWithInvalidMoveID()
    {
        // arrange
        const string moveID = "test";
        List<MoveCost> expectedMove = new List<MoveCost>();
        List<MoveCost> actualMoveCost;

        // act
        actualMoveCost = _moveManager.GetMoveCostsByMoveID(moveID);

        // assert
        Assert.AreEqual(expectedMove.Count, actualMoveCost.Count);
    }

    [TestMethod]
    public void TestGetMoveCostByMoveIDWithMoveIDWithNoMoveCost()
    {
        // arrange
        const string moveID = "test move 3";
        List<MoveCost> expectedMoveCost = new List<MoveCost>();
        List<MoveCost> actualMoveCost;

        // act
        actualMoveCost = _moveManager.GetMoveCostsByMoveID(moveID);

        // assert
        Assert.AreEqual(expectedMoveCost.Count, actualMoveCost.Count);
    }

    [TestMethod]
    public void TestGetMoveVMByMoveIDWithValidMoveID()
    {
        // arrange
        const string moveID = "test move 1";
        const int costCount = 2;
        const int damage = 10;
        const string moveCostElementType1 = "element";
        MoveVM actualMoveVM;

        // act
        actualMoveVM = _moveManager.GetMoveVMByMoveID(moveID);

        // assert
        Assert.AreEqual(costCount, actualMoveVM.Costs.Count);
        Assert.AreEqual(moveID, actualMoveVM.MoveID);
        Assert.AreEqual(damage, actualMoveVM.Damage);
        Assert.AreEqual(moveCostElementType1, actualMoveVM.Costs[0].ElementType);
    }

    [TestMethod]
    public void TestGetMoveVMByMoveIDWithValidMoveIDAndNoMoveCost()
    {
        // arrange
        const string moveID = "test move 3";
        const int costCount = 0;
        const int damage = 0;
        MoveVM actualMoveVM;

        // act
        actualMoveVM = _moveManager.GetMoveVMByMoveID(moveID);

        // assert
        Assert.AreEqual(costCount, actualMoveVM.Costs.Count);
        Assert.AreEqual(moveID, actualMoveVM.MoveID);
        Assert.AreEqual(damage, actualMoveVM.Damage);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestGetMoveVMByMoveIDThrowsApplicationExceptionWithInvalidMoveID()
    {
        // arrange
        const string moveID = "test failed";
        MoveVM actualMoveVM;

        // act
        actualMoveVM = _moveManager.GetMoveVMByMoveID(moveID);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetMoveVMs() 
    {
        // arrange
        const int expectedCount = 3;
        const string expectedMoveID = "test move 1";
        const int expectedCostCount = 2;
        List<MoveVM> artists = null;

        // act
        artists = _moveManager.GetMoveVMs();

        // assert
        Assert.AreEqual(expectedCount, artists.Count);
        Assert.AreEqual(expectedMoveID, artists[0].MoveID);
        Assert.AreEqual(expectedCostCount, artists[1].Costs.Count);
    }

    //
}
