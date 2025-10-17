using DataAccessFakes;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest;

[TestClass]
public class UserManagerTest
{
    IUserManager _manager = null;

    [TestInitialize]
    public void TestSetup()
    {
        _manager = new UserManager(new UserAccessorFakes());
    }

    [TestMethod]
    public void TestHashSha256ReturnsCorrectHashValue()
    {
        // arrange
        const string password = "newuser";
        const string expectedValue = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
        string actualValue = null;

        // act
        actualValue = _manager.HashSha256(password);

        // assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void TestHashSha256ThrowsNullReferenceExceptionForMissingInput()
    {
        // arrange
        const string password = null;
        string actualValue = null;

        // act
        actualValue = _manager.HashSha256(password);

        // assert
        // nothing to do
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestHashSha256ThrowsArgumentOutOfRangeExceptionForEmptyString()
    {
        // arrange
        const string password = "";
        string actualValue = null;

        // act
        actualValue = _manager.HashSha256(password);

        // assert
        // nothing to do
    }

    [TestMethod]
    public void TestAuthenticateUserWithCorrectInput()
    {
        // arrange
        const string email = "testuser1@test.com";
        const string password = "newuser";
        const bool expectedValue = true;
        bool actualValue = false;

        // act
        actualValue = _manager.AuthenticateUser(email, password);

        // assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void TestAuthenticateUserWithIncorrectEmail()
    {
        // arrange
        const string email = "testloser1@test.com";
        const string password = "newuser";
        const bool expectedValue = false;
        bool actualValue = true;

        // act
        actualValue = _manager.AuthenticateUser(email, password);

        // assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void TestAuthenticateUserWithIncorrectPassword()
    {
        // arrange
        const string email = "testuser@test.com";
        const string password = "newloser";
        const bool expectedValue = false;
        bool actualValue = true;

        // act
        actualValue = _manager.AuthenticateUser(email, password);

        // assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void TestAuthenticateUserWithInactiveUser()
    {
        // arrange
        const string email = "testuser5@test.com";
        const string password = "newuser";
        const bool expectedValue = false;
        bool actualValue = true;

        // act
        actualValue = _manager.AuthenticateUser(email, password);

        // assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void TestGetUserByEmail()
    { 
        //
    }
}
