using DataAccessFakes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest;

[TestClass]
public class RuleManagerTest
{
    IRuleManager _ruleManager;

    [TestInitialize]
    public void TestSetup()
    {
       _ruleManager = new RuleManager(new RuleAccessorFakes());
    }

    [TestMethod]
    public void TestGetRuleByRuleIDWithValidRuleID()
    {
        // arrange
        const string ruleID = "Test Rule 1";
        const string expectedDescription = "This is a test.";
        PokemonRule actualResult = null;

        // act
        actualResult = _ruleManager.GetRuleByRuleID(ruleID);

        // assert
        Assert.AreEqual(ruleID, actualResult.RuleID);
        Assert.AreEqual(expectedDescription, actualResult.Description);
    }

    [TestMethod]
    public void TestGetRuleByRuleIDReturnsNullWithInvalidID()
    {
        // arrange
        const string ruleID = "1";
        const PokemonRule expectedResult = null;
        PokemonRule actualResult = null;

        // act
        actualResult = _ruleManager.GetRuleByRuleID(ruleID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestGetAllRules()
    {
        // arrange
        const int listCount = 5;
        const string ruleID2 = "Test Rule 2";
        const string description3 = "This is really not a test.";

        List<PokemonRule> actualResults = null;

        // act
        actualResults = _ruleManager.GetAllRules();

        // assert
        Assert.AreEqual(listCount, actualResults.Count);
        Assert.AreEqual(ruleID2, actualResults[1].RuleID);
        Assert.AreEqual(description3, actualResults[2].Description);
    }

    [TestMethod]
    public void TestAddRuleReturnsTrueWithValidInput() 
    {
        // arrange
        PokemonRule newRule = new PokemonRule()
        {
            RuleID = "Rule 4",
            Description = "Description 4",
        };
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _ruleManager.AddRule(newRule);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAddRuleThrowsArgumentNullExceptionWithNullPokemonRule() 
    {
        // arrange
        PokemonRule newRule = null;
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _ruleManager.AddRule(newRule);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddRuleThrowsApplicationExceptionWithDuplicateRuleID() 
    {
        // arrange
        PokemonRule newRule = new PokemonRule()
        {
            RuleID = "Test Rule 1",
            Description = "Description 4",
        };
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _ruleManager.AddRule(newRule);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestEditRuleReturnsTrueWithValidInput()
    {
        // arrange
        PokemonRule rule = new PokemonRule()
        {
            RuleID = "Test Rule 1",
            Description = "New description."
        };
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _ruleManager.EditRule(rule);

        // assert
        Assert.AreEqual(expectedResult,actualResult);
    }

    [TestMethod]
    public void TestEditRuleReturnsFalseWithInvalidRuleID()
    {
        // arrange
        PokemonRule rule = new PokemonRule()
        {
            RuleID = "failed",
            Description = "New description."
        };
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _ruleManager.EditRule(rule);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestEditRuleThrowsArgumentNullExceptionWithNullPokemonRule()
    {
        // arrange
        PokemonRule rule = null;
        bool actualResult = true;

        // act
        actualResult = _ruleManager.EditRule(rule);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestDeleteRuleReturnsTrueWithValidRuleID()
    {
        // arrange
        const string ruleID = "Test Rule 1";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _ruleManager.DeleteRule(ruleID);

        // assert
        Assert.AreEqual(expectedResult,actualResult);
    }

    [TestMethod]
    public void TestDeleteRuleReturnsFalseWithValidRuleID()
    {
        // arrange
        const string ruleID = "Failed";
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _ruleManager.DeleteRule(ruleID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeactivateRuleReturnsTrueWithValidID()
    {
        // arrange
        const string ruleID = "Test Rule 1";
        const bool expected = true;
        bool actual = false;

        // act
        actual = _ruleManager.DeactivateRule(ruleID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestDeactivateRuleReturnsTrueWithAlreadyActiveID()
    {
        // arrange
        const string ruleID = "Test Rule 3";
        const bool expected = true;
        bool actual = false;

        // act
        actual = _ruleManager.DeactivateRule(ruleID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestDeactivateRuleReturnsFalseWithInvalidID()
    {
        // arrange
        const string ruleID = "fails";
        const bool expected = false;
        bool actual = true;

        // act
        actual = _ruleManager.DeactivateRule(ruleID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestReactivateRuleReturnsTrueWithValidID()
    {
        // arrange
        const string ruleID = "Test Rule 3";
        const bool expected = true;
        bool actual = false;

        // act
        actual = _ruleManager.ReactivateRule(ruleID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestReactivateRuleReturnsTrueWithAlreadyActiveID()
    {
        // arrange
        const string ruleID = "Test Rule 1";
        const bool expected = true;
        bool actual = false;

        // act
        actual = _ruleManager.ReactivateRule(ruleID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestReactivateRuleReturnsFalseWithInvalidID()
    {
        // arrange
        const string ruleID = "fails";
        const bool expected = false;
        bool actual = true;

        // act
        actual = _ruleManager.ReactivateRule(ruleID);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestGetActiveRulesReturnsFullList()
    {
        // arrange
        const int count = 3;
        PaginatedResult<PokemonRule> actualResult;

        // act
        actualResult = _ruleManager.GetActiveRules();

        // assert
        Assert.AreEqual(count, actualResult.Items.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetActiveRulesThrowsArgumentExceptionWithInvalidPageNumber()
    {
        // arrange
        const int pageNumber = -1;
        PaginatedResult<PokemonRule> actualResult;

        // act
        actualResult = _ruleManager.GetActiveRules(pageNumber: pageNumber);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetActiveRulesThrowsArgumentExceptionWithInvalidPageSize()
    {
        // arrange
        const int pageSize = 0;
        PaginatedResult<PokemonRule> actualResult;

        // act
        actualResult = _ruleManager.GetActiveRules(pageSize: pageSize);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetDeactiveRulesReturnsFullList()
    {
        // arrange
        const int count = 2;
        PaginatedResult<PokemonRule> actualResult;

        // act
        actualResult = _ruleManager.GetDeactiveRules();

        // assert
        Assert.AreEqual(count, actualResult.Items.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetDeactiveRulesThrowsArgumentExceptionWithInvalidPageNumber()
    {
        // arrange
        const int pageNumber = -1;
        PaginatedResult<PokemonRule> actualResult;

        // act
        actualResult = _ruleManager.GetDeactiveRules(pageNumber: pageNumber);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetDeactiveRulesThrowsArgumentExceptionWithInvalidPageSize()
    {
        // arrange
        const int pageSize = 0;
        PaginatedResult<PokemonRule> actualResult;

        // act
        actualResult = _ruleManager.GetDeactiveRules(pageSize: pageSize);

        // assert
        // do nothing
    }
}
