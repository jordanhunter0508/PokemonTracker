using DataAccessFakes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest;

[TestClass]
public class AbilityManagerTest
{
    IAbilityManager _abilityManager;
    [TestInitialize]
    public void TestSetup()
    {
        _abilityManager = new AbilityManager(new AbilityAccessorFakes());
    }

    [TestMethod]
    public void TestGetAbilityByAbilityIDWithValidInput()
    {
        // arrange
        const string abilityID = "Ability Test 1";
        const string abilityType = "Ability Type";
        const string description = "This is description 1.";
        Ability actualResult = null;

        // act
        actualResult = _abilityManager.GetAbilityByAbilityID(abilityID);

        // assert
        Assert.AreEqual(abilityID, actualResult.AbilityID);
        Assert.AreEqual(abilityType, actualResult.AbilityType);
        Assert.AreEqual(description, actualResult.Description);

    }

    [TestMethod]
    public void TestGetAbilityByAbilityIDWithInvalidInput()
    {
        // arrange
        const string abilityID = "Test Fails";
        const Ability expectedResult = null;
        Ability actualResult = null;

        // act
        actualResult = _abilityManager.GetAbilityByAbilityID(abilityID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);

    }

    [TestMethod]
    public void TestGetActiveAbilitesReturnsFullList()
    {
        // arrange
        const int count = 4;
        const string abilityID4 = "Ability Test 4";
        const string abilityType = "Ability Type";
        List<Ability> actualResult;

        // act
        actualResult = _abilityManager.GetActiveAbilities();

        // assert
        Assert.AreEqual(count, actualResult.Count);
        Assert.AreEqual(abilityID4, actualResult[3].AbilityID);
        Assert.AreEqual(abilityType, actualResult[2].AbilityType);
    }

    [TestMethod]
    public void TestGetDeactiveAbilitesReturnsFullList()
    {
        // arrange
        const int count = 1;
        const string abilityID4 = "Ability Test 4";
        const string abilityType = "Ability Type";
        List<Ability> actualResult;

        // act
        actualResult = _abilityManager.GetDeactiveAbilities();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    public void TestGetAbilitiesByAbilityTypeWithValidType()
    {
        // arrange
        const string abilityType = "Ability Type";
        const int count = 4;
        List<Ability> actualResult;

        // act
        actualResult = _abilityManager.GetAbilityByAbilityType(abilityType);

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    public void TestGetAbilitiesByAbilityTypeWithInvalidType()
    {
        // arrange
        const string abilityType = "Ability Failed";
        const int count = 0;
        List<Ability> actualResult;

        // act
        actualResult = _abilityManager.GetAbilityByAbilityType(abilityType);

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    public void TestAddAbilityReturnsTrueWithValidAbility()
    {
        // arrange
        Ability ability = new Ability()
        {
            AbilityID = "New AbilityID",
            AbilityType = "Ability Test",
            Description = "Test",
        };
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _abilityManager.AddAbility(ability);

        // assert 
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAddAbilityThrowsArgumentNullExceptionWithNullAbility()
    {
        // arrange
        Ability ability = null;
        bool actualResult = false;

        // act
        actualResult = _abilityManager.AddAbility(ability);

        // assert 
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddAbilityThrowsApplicationExceptionWitDuplicateID()
    {
        // arrange
        Ability ability = new Ability()
        {
            AbilityID = "Ability Test 1",
            AbilityType = "Ability Test",
            Description = "Test",
        };
        bool actualResult = false;

        // act
        actualResult = _abilityManager.AddAbility(ability);

        // assert 
        // do nothing
    }

    [TestMethod]
    public void TestEditAbilityReturnsTrueWithValidAbility()
    {
        // arrange
        Ability ability = new Ability()
        {
            AbilityID = "Ability Test 1",
            AbilityType = "New Test",
            Description = "Test Update",
        };
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _abilityManager.EditAbility(ability);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }
    [TestMethod]
    public void TestEditAbilityReturnsFalseWithInvalidAbilityID()
    {
        // arrange
        Ability ability = new Ability()
        {
            AbilityID = "Ability failes 1",
            AbilityType = "New Test",
            Description = "Test Update",
        };
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _abilityManager.EditAbility(ability);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestEditAbilityThrowsArgumentNullExceptionWithNullAbility()
    {
        // arrange
        Ability ability = null;
        bool actualResult = false;

        // act
        actualResult = _abilityManager.EditAbility(ability);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestDeleteAbilityReturnsTrueWithValidInput()
    {
        // arrange
        const string abilityID = "Ability Test 1";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _abilityManager.DeleteAbility(abilityID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeleteAbilityReturnsFalseWithInvalidInput()
    {
        // arrange
        const string abilityID = "Ability failed";
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _abilityManager.DeleteAbility(abilityID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeactivateAbilityReturnsTrueWithValidInput()
    {
        // arrange
        const string abilityID = "Ability Test 1";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _abilityManager.DeactivateAbility(abilityID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeactivateAbilityReturnsTrueWithAlreadyDeactiveAbility()
    {
        // arrange
        const string abilityID = "Ability Test 5";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _abilityManager.DeactivateAbility(abilityID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeactivateAbilityReturnsFalseWithInvalidInput()
    {
        // arrange
        const string abilityID = "Ability failed";
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _abilityManager.DeactivateAbility(abilityID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestReactivateAbilityReturnsTrueWithValidInput()
    {
        // arrange
        const string abilityID = "Ability Test 5";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _abilityManager.ReactivateAbility(abilityID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestReactivateAbilityReturnsTrueWithAlreadyActiveAbility()
    {
        // arrange
        const string abilityID = "Ability Test 1";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _abilityManager.ReactivateAbility(abilityID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestReactivateAbilityReturnsFalseWithInvalidInput()
    {
        // arrange
        const string abilityID = "Ability failed";
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _abilityManager.ReactivateAbility(abilityID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestFormatAbilityReturnsCorrectOrder()
    {
        // arrange
        List<Ability> inputList = new List<Ability>();
        inputList.Add(new Ability()
        {
            AbilityID = "test",
            AbilityType = "type",
            Description = "description"
        });
        inputList.Add(new Ability()
        {
            AbilityID = "another test",
            AbilityType = "type",
            Description = "description"
        });
        List<Ability> expectedList = new List<Ability>();
        expectedList.Add(new Ability()
        {
            AbilityID = "Another test",
            AbilityType = "type",
            Description = "description"
        });
        expectedList.Add(new Ability()
        {
            AbilityID = "Test",
            AbilityType = "type",
            Description = "description"
        });
        List<Ability> actualList = null;

        // act
        actualList = _abilityManager.FormatAbility(inputList).ToList();

        // assert
        Assert.AreEqual(expectedList[0].AbilityID, actualList[0].AbilityID);
        Assert.AreEqual(expectedList[1].AbilityID, actualList[1].AbilityID);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestFormatElementTypeThrowsArgumentNullExceptionWithNullInput()
    {
        // arrange
        List<Ability> inputList = null;
        List<Ability> outputList = new List<Ability>();

        // act
        outputList = _abilityManager.FormatAbility(inputList).ToList();

        // assert
        // do nothing
    }
}