using Azure;
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
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetAbilityByAbilityIDThrowsArgumentNullExceptionWithNullAbilityID()
    {
        // arrange
        const string abilityID = null;
        Ability actualResult = null;

        // act
        actualResult = _abilityManager.GetAbilityByAbilityID(abilityID);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetAbilityByAbilityIDThrowsArgumentNullExceptionWithBlankAbilityID()
    {
        // arrange
        const string abilityID = "";
        Ability actualResult = null;

        // act
        actualResult = _abilityManager.GetAbilityByAbilityID(abilityID);

        // assert
        // do nothing
    }


    [TestMethod]
    public void TestGetActiveAbilitesReturnsFullList()
    {
        // arrange
        const int count = 4;
        const string abilityID4 = "Ability Test 4";
        const string abilityType = "Ability Type";
        PaginatedResult<Ability> actualResult;

        // act
        actualResult = _abilityManager.GetActiveAbilities();

        // assert
        Assert.AreEqual(count, actualResult.Items.Count);
        Assert.AreEqual(abilityID4, actualResult.Items[3].AbilityID);
        Assert.AreEqual(abilityType, actualResult.Items[2].AbilityType);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetActiveAbilitesThrowsArgumentExceptionWithInvalidPageNumber()
    {
        // arrange
        const int pageNumber = -1;
        PaginatedResult<Ability> actualResult;

        // act
        actualResult = _abilityManager.GetActiveAbilities(pageNumber: pageNumber);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetActiveAbilitesThrowsArgumentExceptionWithInvalidPageSize()
    {
        // arrange
        const int pageSize = 0;
        PaginatedResult<Ability> actualResult;

        // act
        actualResult = _abilityManager.GetActiveAbilities(pageSize: pageSize);

        // assert
        // do nothing
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
        actualResult = _abilityManager.GetDeactiveAbilities().Items.ToList();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetDeactiveAbilitiesThrowsArgumentExceptionWithInvalidPageNumber()
    {
        // arrange
        const int pageNumber = -1;
        PaginatedResult<Ability> actualResult;

        // act
        actualResult = _abilityManager.GetDeactiveAbilities(pageNumber: pageNumber);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetDeactiveAbilitiesThrowsArgumentExceptionWithInvalidPageSize()
    {
        // arrange
        const int pageSize = 0;
        PaginatedResult<Ability> actualResult;

        // act
        actualResult = _abilityManager.GetDeactiveAbilities(pageSize: pageSize);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetAbilitiesByAbilityTypeWithValidType()
    {
        // arrange
        const string abilityType = "Ability Type";
        const int count = 4;
        PaginatedResult<Ability> actualResult;

        // act
        actualResult = _abilityManager.GetAbilitiesByAbilityType(abilityType);

        // assert
        Assert.AreEqual(count, actualResult.Items.Count);
    }

    [TestMethod]
    public void TestGetAbilitiesByAbilityTypeWithInvalidType()
    {
        // arrange
        const string abilityType = "Ability Failed";
        const int count = 0;
        PaginatedResult<Ability> actualResult;

        // act
        actualResult = _abilityManager.GetAbilitiesByAbilityType(abilityType);

        // assert
        Assert.AreEqual(count, actualResult.Items.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetAbilityByAbilityTypeThrowsArgumentExceptionWithInvalidPageNumber()
    {
        // arrange
        const string abilityType = "Ability Type";
        const int pageNumber = -1;
        PaginatedResult<Ability> actualResult;

        // act
        actualResult = _abilityManager.GetAbilitiesByAbilityType(abilityType,pageNumber: pageNumber);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetAbilityByAbilityTypeThrowsArgumentExceptionWithInvalidPageSize()
    {
        // arrange
        const string abilityType = "Ability Type";
        const int pageSize = 0;
        PaginatedResult<Ability> actualResult;

        // act
        actualResult = _abilityManager.GetAbilitiesByAbilityType(abilityType,pageSize: pageSize);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetAbilityByAbilityTypeThrowsArgumentNullExceptionWithNullAbilityType()
    {
        // arrange
        const string abilityType = null;
        const int pageSize = 0;
        PaginatedResult<Ability> actualResult;

        // act
        actualResult = _abilityManager.GetAbilitiesByAbilityType(abilityType,pageSize: pageSize);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestGetAbilityByAbilityTypeThrowsArgumentNullExceptionWithBlankAbilityType()
    {
        // arrange
        const string abilityType = "";
        const int pageSize = 0;
        PaginatedResult<Ability> actualResult;

        // act
        actualResult = _abilityManager.GetAbilitiesByAbilityType(abilityType,pageSize: pageSize);

        // assert
        // do nothing
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
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestDeleteAbilityThrowsArgumentNullExceptionWithNullAbilityID()
    {
        // arrange
        const string abilityID = null;
        bool actualResult = true;

        // act
        actualResult = _abilityManager.DeleteAbility(abilityID);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestDeleteAbilityThrowsArgumentNullExceptionWithBlankAbilityID()
    {
        // arrange
        const string abilityID = "";
        bool actualResult = true;

        // act
        actualResult = _abilityManager.DeleteAbility(abilityID);

        // assert
        // do nothing
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
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestDeactivateAbilityThrowsArgumentNullExceptionWithNullAbilityID()
    {
        // arrange
        const string abilityID = null;
        bool actualResult = true;

        // act
        actualResult = _abilityManager.DeactivateAbility(abilityID);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestDeactivateAbilityThrowsArgumentNullExceptionWithBlankAbilityID()
    {
        // arrange
        const string abilityID = "";
        bool actualResult = true;

        // act
        actualResult = _abilityManager.DeactivateAbility(abilityID);

        // assert
        // do nothing
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
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestReactivateAbilityThrowsArgumentNullExceptionWithNullAbilityID()
    {
        // arrange
        const string abilityID = null;
        bool actualResult = true;

        // act
        actualResult = _abilityManager.ReactivateAbility(abilityID);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestReactivateAbilityThrowsArgumentNullExceptionWithBlankAbilityID()
    {
        // arrange
        const string abilityID = "";
        bool actualResult = true;

        // act
        actualResult = _abilityManager.ReactivateAbility(abilityID);

        // assert
        // do nothing
    }
}