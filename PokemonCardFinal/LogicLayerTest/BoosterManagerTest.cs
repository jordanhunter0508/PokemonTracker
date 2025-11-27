using DataAccessFakes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest;

[TestClass]
public class BoosterManagerTest
{
    IBoosterManager _boosterManager;

    [TestInitialize]
    public void TestSetup() 
    {
        _boosterManager = new BoosterManager(new BoosterAccessorFakes());
    }

    [TestMethod]
    public void TestGetBoosterByBoosterIDWithValidInput()
    {
        // arrange
        const string boosterID = "Test Booster 1";
        const string series = "test series";
        DateTime dateTime = new DateTime(2025,11,06);
        const string abbreviation = "test";
        Booster actualBooster;

        // act
        actualBooster = _boosterManager.GetBoosterByBoosterID(boosterID);

        // assert
        Assert.AreEqual(boosterID, actualBooster.BoosterID);
        Assert.AreEqual(series, actualBooster.Series);
        Assert.AreEqual(dateTime, actualBooster.ReleaseDate);
        Assert.AreEqual(abbreviation, actualBooster.Abbreviation);
    }

    [TestMethod]
    public void TestGetBoosterByBoosterIDReturnsNullWithInvalidBoosterID()
    {
        // arrange
        const string boosterID = "Test Fails";
        const Booster expectedResult = null;
        Booster acutalResult;

        // act
        acutalResult = _boosterManager.GetBoosterByBoosterID(boosterID);

        // assert
        Assert.AreEqual(expectedResult, acutalResult);
    }

    [TestMethod]
    public void TestGetBoostersWithValidInput()
    {
        // arrange
        const int count = 3;
        const string boosterID1 = "Test Booster 1";
        const string abbreviation3 = "abv";
        List<Booster> actualBoosters;

        // act
        actualBoosters = _boosterManager.GetBoosters();

        // assert
        Assert.AreEqual(count, actualBoosters.Count);
        Assert.AreEqual(boosterID1, actualBoosters[0].BoosterID);
        Assert.AreEqual(abbreviation3, actualBoosters[2].Abbreviation);
    }

    [TestMethod]
    public void TestAddBoosterReturnsTrueWithValidBooster()
    {
        // arrange
        Booster booster = new Booster()
        {
            BoosterID = "new Booster",
            Series = "test series",
            ReleaseDate = DateTime.Parse("2025-11-06"),
            Abbreviation = "bost",
        };
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _boosterManager.AddBooster(booster);

        // assert
        Assert.AreEqual(expectedResult,actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAddBoosterThrowsArgumentNullExceptionWithNullBooster()
    {
        // arrange
        Booster booster = null;
        bool actualResult = true;

        // act
        actualResult = _boosterManager.AddBooster(booster);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddBoosterThrowsApplicationExcpetionWithDuplicateBoosterID()
    { 
        // arrange
        Booster booster = new Booster()
        {
            BoosterID = "Test Booster 1",
            Series = "test series",
            ReleaseDate = DateTime.Parse("2025-11-06"),
            Abbreviation = "bost",
        };
        bool actualResult = false;

        // act
        actualResult = _boosterManager.AddBooster(booster);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddBoosterThrowsApplicationExcpetionWithDuplicateAbbreviation()
    {
        // arrange
        Booster booster = new Booster()
        {
            BoosterID = "new Booster",
            Series = "test series",
            ReleaseDate = DateTime.Parse("2025-11-06"),
            Abbreviation = "test",
        };
        bool actualResult = false;

        // act
        actualResult = _boosterManager.AddBooster(booster);

        // assert
       // do nothing
    }

    [TestMethod]
    public void TestEditBoosterWithValidInput()
    {
        // arrange
        Booster booster = new Booster()
        {
            BoosterID = "Test Booster 1",
            Series = "updated series",
            ReleaseDate = DateTime.Parse("2025-11-06"),
            Abbreviation = "boss",
        };
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _boosterManager.EditBooster(booster); 

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestEditBoosterThrowsArgumentNullExceptionWithNullBooster() 
    {
        // arrange
        Booster booster = null;
        bool actualResult = false;

        // act
        actualResult = _boosterManager.EditBooster(booster);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditBoosterThrowsApplicationExceptionWithDuplicateAbbreviation()
    {
        Booster booster = new Booster()
        {
            BoosterID = "Test Booster 2",
            Series = "updated series",
            ReleaseDate = DateTime.Parse("2025-11-06"),
            Abbreviation = "test",
        };
        bool actualResult = false;

        // act
        actualResult = _boosterManager.EditBooster(booster);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestDeleteBoosterReturnTrueWithValidBoosterID()
    {
        // arrange
        const string boosterID = "Test Booster 3";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _boosterManager.DeleteBooster(boosterID);    

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeleteBoosterReturnsFalseWithInvalidBoosterID() 
    {
        // arrange
        const string boosterID = "Test Booster 4";
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _boosterManager.DeleteBooster(boosterID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestGetBoosterIDs() 
    {
        // arrange
        const int count = 3;
        const string boosterID1 = "Test Booster 1";
        List<string> actualResult = new List<string>();

        // act
        actualResult = _boosterManager.GetBoosterIDs();

        // assert
        Assert.AreEqual(count, actualResult.Count);
        Assert.AreEqual(boosterID1, actualResult[0]);
    }
}
