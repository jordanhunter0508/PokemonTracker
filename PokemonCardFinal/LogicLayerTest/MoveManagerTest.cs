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
    public void TestGetMoveVMsWithMoveCost() 
    {
        // arrange
        const int expectedCount = 2;
        const string expectedMoveID = "test move 1";
        const int expectedCostCount = 2;
        List<MoveVM> actualMoveVMs = null;

        // act
        actualMoveVMs = _moveManager.GetMoveVMsWithMoveCost();

        // assert
        Assert.AreEqual(expectedCount, actualMoveVMs.Count);
        Assert.AreEqual(expectedMoveID, actualMoveVMs[0].MoveID);
        Assert.AreEqual(expectedCostCount, actualMoveVMs[1].Costs.Count);
    }

    [TestMethod]
    public void TestGetMovesWithoutMoveCost() 
    {
        // arrange
        const int expectedCount = 1;
        const string expectedMoveID = "test move 3";
        List<Move> actualMoves = null;

        // act
        actualMoves = _moveManager.GetMovesWithoutMoveCost();

        // assert
        Assert.AreEqual(expectedCount, actualMoves.Count);
        Assert.AreEqual(expectedMoveID, actualMoves[0].MoveID);
    }

    [TestMethod]
    public void TestGetMoveVMs()
    {
        // arrange
        const int expectedCount = 3;
        const string expectedMoveID = "test move 1";
        const int expectedCostCount = 0;
        List<MoveVM> actualMoves = null;

        // act
        actualMoves = _moveManager.GetMoveVMs();

        // assert
        Assert.AreEqual(expectedCount, actualMoves.Count);
        Assert.AreEqual(expectedMoveID, actualMoves[0].MoveID);
        Assert.AreEqual(expectedCostCount, actualMoves[2].Costs.Count);
    }

    [TestMethod]
    public void TestAddMoveWithValidMove()
    {
        // arrange
        Move move = new Move()
        {
            MoveID = "new move",
            Damage = 100,
            Description = "This is a new move."
        };
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _moveManager.AddMove(move);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAddMoveThrowsArgumentNullExceptionWithNullMove()
    {
        // arrange
        Move move = null;
        bool actualResult = true;

        // act
        actualResult = _moveManager.AddMove(move);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddMoveThrowsApplicationExceptionWithDuplicateID()
    {
        // arrange
        Move move = new Move()
        {
            MoveID = "test move 1",
            Damage = 100,
            Description = "This is a new move."
        };
        bool actualResult = false;

        // act
        actualResult = _moveManager.AddMove(move);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestAddMoveCostWithValidMove()
    {
        // arrange
        MoveCost moveCost = new MoveCost()
        {
            MoveID = "test move 1",
            ElementType = "new element",
            Quantity = 2
        };
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _moveManager.AddMoveCost(moveCost);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAddMoveCostThrowsArgumentNullExceptionWithNullMoveCost()
    {
        // arrange
        MoveCost moveCost = null;
        bool actualResult = false;

        // act
        actualResult = _moveManager.AddMoveCost(moveCost);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddMoveCostThrowsApplicationExceptionWithInvalidMoveID()
    {
        // arrange
        MoveCost moveCost = new MoveCost()
        {
            MoveID = "Failed",
            ElementType = "element",
            Quantity = 2
        };
        bool actualResult = false;

        // act
        actualResult = _moveManager.AddMoveCost(moveCost);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddMoveCostThrowsApplicationExceptionWithInvalidElementTypeID()
    {
        // arrange
        MoveCost moveCost = new MoveCost()
        {
            MoveID = "test move 1",
            ElementType = "failed",
            Quantity = 2
        };
        bool actualResult = false;

        // act
        actualResult = _moveManager.AddMoveCost(moveCost);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddMoveCostThrowsApplicationExceptionWithDuplicateIDs()
    {
        // arrange
        MoveCost moveCost = new MoveCost()
        {
            MoveID = "test move 1",
            ElementType = "element",
            Quantity = 2
        };
        bool actualResult = false;

        // act
        actualResult = _moveManager.AddMoveCost(moveCost);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestAddMoveVMWithValidMoveVM()
    {
        // arrange
        List<MoveCost> moveCost = new List<MoveCost>();
        moveCost.Add(new MoveCost()
        {
            MoveID = "new move",
            ElementType = "element",
            Quantity = 2
        });
        moveCost.Add(new MoveCost()
        {
            MoveID = "new move",
            ElementType = "new element",
            Quantity = 1
        });

        MoveVM moveVM = new MoveVM()
        {
            MoveID = "new move",
            Damage = 100,
            Description = "This is a new move.",
            Costs = moveCost
        };

        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _moveManager.AddMoveVM(moveVM);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestAddMoveVMWithEmptyMoveCostList()
    {
        // arrange
        List<MoveCost> moveCost = new List<MoveCost>();

        MoveVM moveVM = new MoveVM()
        {
            MoveID = "new move",
            Damage = 100,
            Description = "This is a new move.",
            Costs = moveCost
        };

        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _moveManager.AddMoveVM(moveVM);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddMoveVMThrowsApplicationExceptionWithNullMoveCostList()
    {
        // arrange
        List<MoveCost> moveCost = null;

        MoveVM moveVM = new MoveVM()
        {
            MoveID = "new move",
            Damage = 100,
            Description = "This is a new move.",
            Costs = moveCost
        };

        bool actualResult = false;

        // act
        actualResult = _moveManager.AddMoveVM(moveVM);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddMoveVMThrowsApplicationExecptionWithDuplicateInputs()
    {
        // arrange
        List<MoveCost> moveCost = new List<MoveCost>();

        MoveVM moveVM = new MoveVM()
        {
            MoveID = "new move",
            Damage = 100,
            Description = "This is a new move.",
            Costs = moveCost
        };

        bool actualResult = false;

        // act
        actualResult = _moveManager.AddMoveVM(moveVM);
        actualResult = _moveManager.AddMoveVM(moveVM);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddMoveVMThrowsApplicationExecptionWithMissingElementTypeID()
    {
        // arrange
        List<MoveCost> moveCost = new List<MoveCost>();
        moveCost.Add(new MoveCost()
        {
            MoveID = "new move",
            Quantity = 2
        });

        MoveVM moveVM = new MoveVM()
        {
            MoveID = "new move",
            Damage = 100,
            Description = "This is a new move.",
            Costs = moveCost
        };

        bool actualResult = false;

        // act
        actualResult = _moveManager.AddMoveVM(moveVM);

        // assert
        // do nothing
    }
}