using DataAccessFakes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

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
    public void TestGetMoveByMoveIDWithValidInputReturnsMove()
    {
        // arrange
        const string inputedMoveID = "test move 1";
        const string expectedMoveID = "test move 1";
        const int expectedDamage = 10;
        const string expectedDescription = "This is a test move.";
        Move actualMove = null;

        // act
        actualMove = _moveManager.GetMovseByMoveID(inputedMoveID);

        // assert
        Assert.AreEqual(expectedMoveID, actualMove.MoveID);
        Assert.AreEqual(expectedDamage,actualMove.Damage);
        Assert.AreEqual(expectedDescription,actualMove.Description);
    }

    [TestMethod]
    public void TestGetMoveByMoveIDWithInvalidInputReturnsEmptyList()
    {
        // arrange
        const string inputedMoveID = "test";
        Move expectedMove = null;
        Move actualMove;

        // act
        actualMove = _moveManager.GetMovseByMoveID(inputedMoveID);

        // assert
        Assert.AreEqual(expectedMove, actualMove);
    }
}
