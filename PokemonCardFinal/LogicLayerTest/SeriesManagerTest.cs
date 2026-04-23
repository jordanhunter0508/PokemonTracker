using DataAccessFakes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest;

[TestClass]
public class SeriesManagerTest
{
    private ISeriesManager _seriesManager;

    [TestInitialize]
    public void TestSetup()
    {
        _seriesManager = new SeriesManager(new SeriesAccessorFakes());
    }

    [TestMethod]
    public void TestGetAllSeries()
    {
        // arrange
        const int count = 4;
        List<Series> actualResult = new List<Series>();

        // act
        actualResult = _seriesManager.GetAllSeries();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    public void TestGetSeriesImagePaths()
    {
        // arrange
        const int count = 3;
        List<Series> actualResult = new List<Series>();

        // act
        actualResult = _seriesManager.GetSeriesImagePaths();

        // assert
        Assert.AreEqual(count, actualResult.Count);
    }

    [TestMethod]
    public void TestActivateBoosterReactivatesWithValidIDReturnsTrue()
    {
        // arrange
        const string boosterID = "Series 4";
        const bool active = true;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _seriesManager.ActivateSeries(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestActivateSeriesReactivatesWithAlreadyActiveIDReturnsTrue()
    {
        // arrange
        const string boosterID = "Series 1";
        const bool active = true;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _seriesManager.ActivateSeries(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestActivateSeriesReactivatesWithInvalidIDReturnsFalse()
    {
        // arrange
        const string boosterID = "fails";
        const bool active = true;
        const bool expected = false;
        bool actual = true;

        // act
        actual = _seriesManager.ActivateSeries(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestActivateSeriesDeactivatesWithValidIDReturnsTrue()
    {
        // arrange
        const string boosterID = "Series 1";
        const bool active = false;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _seriesManager.ActivateSeries(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestActivateSeriesDeactivatesWithAlreadyDeactiveIDReturnsTrue()
    {
        // arrange
        const string boosterID = "Series 4";
        const bool active = false;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _seriesManager.ActivateSeries(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestActivateSeriesDeactivatesWithInvalidIDReturnsFalse()
    {
        // arrange
        const string boosterID = "fails";
        const bool active = true;
        const bool expected = false;
        bool actual = true;

        // act
        actual = _seriesManager.ActivateSeries(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestActivateSeriesThrowsArgumentExceptionWithNullID()
    {
        // arrange
        const string boosterID = null;
        const bool active = true;
        bool actual = true;

        // act
        actual = _seriesManager.ActivateSeries(boosterID, active);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestActivateSeriesThrowsArgumentExceptionWithBlankID()
    {
        // arrange
        const string boosterID = "";
        const bool active = true;
        bool actual = true;

        // act
        actual = _seriesManager.ActivateSeries(boosterID, active);

        // assert
        // do nothing
    }




    [TestMethod]
    public void TestActivateBoostersBySeriesIDReactivatesWithValidIDReturnsTrue()
    {
        // arrange
        const string boosterID = "Series 4";
        const bool active = true;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _seriesManager.ActivateBoostersBySeriesID(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestActivateBoostersBySeriesIDReactivatesWithAlreadyActiveIDReturnsTrue()
    {
        // arrange
        const string boosterID = "Series 1";
        const bool active = true;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _seriesManager.ActivateBoostersBySeriesID(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestActivateBoostersBySeriesIDReactivatesWithInvalidIDReturnsFalse()
    {
        // arrange
        const string boosterID = "fails";
        const bool active = true;
        const bool expected = false;
        bool actual = true;

        // act
        actual = _seriesManager.ActivateBoostersBySeriesID(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestActivateBoostersBySeriesIDDeactivatesWithValidIDReturnsTrue()
    {
        // arrange
        const string boosterID = "Series 1";
        const bool active = false;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _seriesManager.ActivateBoostersBySeriesID(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestActivateBoostersBySeriesIDDeactivatesWithAlreadyDeactiveIDReturnsTrue()
    {
        // arrange
        const string boosterID = "Series 4";
        const bool active = false;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _seriesManager.ActivateBoostersBySeriesID(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestActivateBoostersBySeriesIDDeactivatesWithInvalidIDReturnsFalse()
    {
        // arrange
        const string boosterID = "fails";
        const bool active = true;
        const bool expected = false;
        bool actual = true;

        // act
        actual = _seriesManager.ActivateBoostersBySeriesID(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestActivateBoostersBySeriesIDThrowsArgumentExceptionWithNullID()
    {
        // arrange
        const string boosterID = null;
        const bool active = true;
        bool actual = true;

        // act
        actual = _seriesManager.ActivateBoostersBySeriesID(boosterID, active);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestActivateBoostersBySeriesIDThrowsArgumentExceptionWithBlankID()
    {
        // arrange
        const string boosterID = "";
        const bool active = true;
        bool actual = true;

        // act
        actual = _seriesManager.ActivateBoostersBySeriesID(boosterID, active);

        // assert
        // do nothing
    }




    [TestMethod]
    public void TestActivateCardsBySeriesIDReactivatesWithValidIDReturnsTrue()
    {
        // arrange
        const string boosterID = "Series 4";
        const bool active = true;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _seriesManager.ActivateCardsBySeriesID(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestActivateCardsBySeriesIDReactivatesWithAlreadyActiveIDReturnsTrue()
    {
        // arrange
        const string boosterID = "Series 1";
        const bool active = true;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _seriesManager.ActivateCardsBySeriesID(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestActivateCardsBySeriesIDReactivatesWithInvalidIDReturnsFalse()
    {
        // arrange
        const string boosterID = "fails";
        const bool active = true;
        const bool expected = false;
        bool actual = true;

        // act
        actual = _seriesManager.ActivateCardsBySeriesID(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestActivateCardsBySeriesIDDeactivatesWithValidIDReturnsTrue()
    {
        // arrange
        const string boosterID = "Series 1";
        const bool active = false;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _seriesManager.ActivateCardsBySeriesID(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestActivateCardsBySeriesIDDeactivatesWithAlreadyDeactiveIDReturnsTrue()
    {
        // arrange
        const string boosterID = "Series 4";
        const bool active = false;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _seriesManager.ActivateCardsBySeriesID(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestActivateCardsBySeriesIDDeactivatesWithInvalidIDReturnsFalse()
    {
        // arrange
        const string boosterID = "fails";
        const bool active = true;
        const bool expected = false;
        bool actual = true;

        // act
        actual = _seriesManager.ActivateCardsBySeriesID(boosterID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestActivateCardsBySeriesIDThrowsArgumentExceptionWithNullID()
    {
        // arrange
        const string boosterID = null;
        const bool active = true;
        bool actual = true;

        // act
        actual = _seriesManager.ActivateCardsBySeriesID(boosterID, active);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestActivateCardsBySeriesIDThrowsArgumentExceptionWithBlankID()
    {
        // arrange
        const string boosterID = "";
        const bool active = true;
        bool actual = true;

        // act
        actual = _seriesManager.ActivateCardsBySeriesID(boosterID, active);

        // assert
        // do nothing
    }
}
