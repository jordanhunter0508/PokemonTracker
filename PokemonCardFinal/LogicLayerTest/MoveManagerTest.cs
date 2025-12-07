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
        const int inputedMoveID = 1;
        const int expectedMoveID = 1;
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
        const int inputedMoveID = 999;
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
        const int moveID = 1;
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
        const int moveID = 999;
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
        const int moveID = 3;
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
        const int moveID = 1;
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
        const int moveID = 3;
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
        const int moveID = 999;
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
        const int expectedMoveID = 1;
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
        const int expectedMoveID = 3;
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
        const int expectedMoveID = 1;
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
            MoveID = 4,
            Name = "new move",
            Damage = 100,
            Description = "This is a new move."
        };
        const int expectedResult = 4;
        int actualResult = 0;

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
        int actualResult = 0;

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
            MoveID = 1,
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
            MoveID = 999,
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
            MoveID = 1,
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
            MoveID = 1,
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
            MoveID = 4,
            ElementType = "element",
            Quantity = 2
        });
        moveCost.Add(new MoveCost()
        {
            MoveID = 4,
            ElementType = "new element",
            Quantity = 1
        });

        MoveVM moveVM = new MoveVM()
        {
            MoveID = 4,
            Name = "new move",
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
            MoveID = 4,
            Name = "new move",
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
            MoveID = 4,
            Name = "new move",
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
            MoveID = 4,
            Name = "new move",
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
            MoveID = 4,
            Quantity = 2
        });

        MoveVM moveVM = new MoveVM()
        {
            MoveID = 4,
            Name = "new move",
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
    public void TestDeleteMoveReturnsTrueWithValidID() 
    {
        // arrange
        const int moveID = 1;
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _moveManager.DeleteMove(moveID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeleteMoveReturnsFalseWithInvalidID()
    {
        // arrange
        const int moveID = 999;
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _moveManager.DeleteMove(moveID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeleteMoveCostReturnsTrueWithValidID()
    {
        // arrange
        const int moveID = 1;
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _moveManager.DeleteMoveCost(moveID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeleteMoveCostReturnsFalseWithInvalidID()
    {
        // arrange
        const int moveID = 999;
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _moveManager.DeleteMoveCost(moveID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestEditMoveReturnsTrueWithValidID()
    {
        // arrange
        Move move = new Move()
        {
            MoveID = 1,
            Name = "test move 11",
            Damage = 1000,
            Description = "Test EditMove Returns False"
        };
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _moveManager.EditMove(move);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestEditMoveReturnsFalseWithInvalidID()
    {
        // arrange
        Move move = new Move() 
        {
            MoveID = 999,
            Name = "test move 1",
            Damage = 1000,
            Description = "Test EditMove Returns False"
        };
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _moveManager.EditMove(move);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestEditMoveReturnsFalseWithBlankMove()
    {
        // arrange
        Move move = new Move();
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _moveManager.EditMove(move);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestEditMoveThrowsArgumentNullExceptionWithNullMove()
    {
        // arrange
        Move move = null;
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _moveManager.EditMove(move);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestEditMoveVMReturnsTrueWithValidID()
    {
        // arrange
        MoveVM move = new MoveVM()
        {
            MoveID = 1,
            Name = "test move 11",
            Damage = 1000,
            Description = "Test EditMove Returns True",
            Costs = new List<MoveCost>() 
            {
                new MoveCost()
                {
                    MoveID = 1,
                    ElementType = "element",
                    Quantity = 3
                },
                new MoveCost()
                {
                    MoveID = 1,
                    ElementType = "new element",
                    Quantity = 1
                }
            }
        };
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _moveManager.EditMoveVM(move);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestEditMoveVMReturnsTrueWithDuplicateMoveCost()
    {
        // arrange
        MoveVM move = new MoveVM()
        {
            MoveID = 1,
            Name = "test move 11",
            Damage = 1000,
            Description = "Test EditMove Returns True",
            Costs = new List<MoveCost>() 
            {
                new MoveCost()
                {
                    MoveID = 1,
                    ElementType = "element",
                    Quantity = 1,
                },
                new MoveCost()
                {
                    MoveID = 1,
                    ElementType = "test element",
                    Quantity = 2,
                }
            }
        };
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _moveManager.EditMoveVM(move);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditMoveVMThrowsApplicationExceptionWithInvalidID()
    {
        // arrange
        MoveVM move = new MoveVM()
        {
            MoveID = 999,
            Name = "test move 11",
            Damage = 1000,
            Description = "Test EditMove Returns False",
            Costs = new List<MoveCost>()
            {
                new MoveCost()
                {
                    MoveID = 1,
                    ElementType = "element",
                    Quantity = 3
                },
                new MoveCost()
                {
                    MoveID = 1,
                    ElementType = "new element",
                    Quantity = 1
                }
            }
        };
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _moveManager.EditMoveVM(move);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestEditMoveVMReturnsFalseWithBlankMoveVM()
    {
        // arrange
        MoveVM move = new MoveVM()
        {
            Costs = new List<MoveCost>()
        };
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _moveManager.EditMoveVM(move);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestEditMoveVMReturnsTrueWithBlankMoveCost()
    {
        // arrange
        MoveVM move = new MoveVM()
        {
            MoveID = 1,
            Name = "test move 11",
            Damage = 1000,
            Description = "Test EditMove Returns True",
            Costs = new List<MoveCost>()
        };
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _moveManager.EditMoveVM(move);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestEditMoveVMThrowsArgumentNullExceptionWithNullMoveVM()
    {
        // arrange
        MoveVM move = null;
        bool actualResult = false;

        // act
        actualResult = _moveManager.EditMoveVM(move);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditMoveVMThrowsApplicationExceptionWithNullMoveCost()
    {
        // arrange
        MoveVM move = new MoveVM()
        {
            Costs = null
        };
        bool actualResult = false;

        // act
        actualResult = _moveManager.EditMoveVM(move);

        // assert
        // do nothing
    }
}