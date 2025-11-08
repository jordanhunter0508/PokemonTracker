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
    public void TestGetRules()
    {
        // arrange
        const int listCount = 3;
        const string ruleID2 = "Test Rule 2";
        const string description3 = "This is really not a test.";

        List<PokemonRule> actualResults = null;

        // act
        actualResults = _ruleManager.GetRules();

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
}
