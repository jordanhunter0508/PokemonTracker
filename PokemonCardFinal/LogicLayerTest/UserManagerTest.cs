using DataAccessFakes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest;

[TestClass]
public class UserManagerTest
{
    IUserManager _userManager = null;

    [TestInitialize]
    public void TestSetup()
    {
        _userManager = new UserManager(new UserAccessorFakes());
    }

    [TestMethod]
    public void TestHashSha256ReturnsCorrectHashValue()
    {
        // arrange
        const string password = "newuser";
        const string expectedValue = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
        string actualValue = null;

        // act
        actualValue = _userManager.HashSha256(password);

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
        actualValue = _userManager.HashSha256(password);

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
        actualValue = _userManager.HashSha256(password);

        // assert
        // nothing to do
    }

    [TestMethod]
    public void TestAuthenticateUserReturnsCorrectBool()
    {
        // arrange
        const string email = "testuser1@test.com";
        const string password = "newuser";
        const bool expectedValue = true;
        bool actualValue = false;

        // act
        actualValue = _userManager.AuthenticateUser(email, password);

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
        actualValue = _userManager.AuthenticateUser(email, password);

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
        actualValue = _userManager.AuthenticateUser(email, password);

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
        actualValue = _userManager.AuthenticateUser(email, password);

        // assert
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void TestGetUserByEmailReturnsCorrectUser()
    {
        // arrange
        const string email = "testuser1@test.com";
        User expectedUser = new User()
        {
            UserID = 1,
            GivenName = "test",
            Surname = "user",
            Email = "testuser1@test.com",
            Active = true,
        };
        User actualUser;

        // act
        actualUser = _userManager.GetUserByEmail(email);

        // assert
        Assert.AreEqual(expectedUser.UserID, actualUser.UserID);
        Assert.AreEqual(expectedUser.GivenName, actualUser.GivenName);
        Assert.AreEqual(expectedUser.Surname, actualUser.Surname);
        Assert.AreEqual(expectedUser.Email, actualUser.Email);
        Assert.AreEqual(expectedUser.Active, actualUser.Active);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestGetUserByEmailThrowsApplicationExceptionForInvalidEmail()
    {
        // arrange
        const string email = "testloser1@test.com";
        User actualUser;

        // act
        actualUser = _userManager.GetUserByEmail(email);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetRolesForUserReturnsCorrectListOfRoles()
    {
        // arrange
        const string email = "testuser1@test.com";
        const int listSize = 2;
        const string role1 = "testRole1";
        const string role2 = "testRole2";
        List<string> actualList;

        // act
        actualList = _userManager.GetRolesForUser(email);

        // assert
        Assert.AreEqual(listSize, actualList.Count);
        Assert.AreEqual(role1, actualList[0]);
        Assert.AreEqual(role2, actualList[1]);
    }

    [TestMethod]
    public void TestGetRolesForUserWithNoRoles()
    {
        // arrange
        const string email = "testuser3@test.com";
        const int listSize = 0;
        List<string> actualList;

        // act
        actualList = _userManager.GetRolesForUser(email);

        // assert
        Assert.AreEqual(listSize, actualList.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestGetRolesForUserThrowsApplicationExceptionForInvalidEmail()
    {
        // arrange
        const string email = "testloser1@test.com";
        List<string> actualList;

        // act
        actualList = _userManager.GetRolesForUser(email);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestLogInUserReturnsCorrectUserVM()
    {
        // arrange
        const string email = "testuser1@test.com";
        const string password = "newuser";
        const int expectedID = 1;
        const int expectedRoleCount = 2;
        const string role1 = "testRole1";
        const string role2 = "testRole2";
        UserVM actualUserVM = null;

        // act
        actualUserVM = _userManager.LogInUser(email, password);

        // assert
        Assert.AreEqual(expectedID, actualUserVM.UserID);
        Assert.AreEqual(expectedRoleCount, actualUserVM.Roles.Count);
        Assert.AreEqual(role1, actualUserVM.Roles[0]);
        Assert.AreEqual(role2, actualUserVM.Roles[1]);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void TestLogInUserThowsNullReferenceExceptionWithInvalidEmail()
    {
        // arrange
        const string email = "testloser1@test.com";
        const string password = "newuser";
        const int expectedID = 1;
        UserVM actualUserVM = null;

        // act
        actualUserVM = _userManager.LogInUser(email, password);

        // assert
        Assert.AreEqual(expectedID, actualUserVM.UserID);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void TestLogInUserThowsNullReferenceExceptionWithInvalidPassword()
    {
        // arrange
        const string email = "testuser1@test.com";
        const string password = "newloser";
        const int expectedID = 1;
        UserVM actualUserVM = null;

        // act
        actualUserVM = _userManager.LogInUser(email, password);

        // assert
        Assert.AreEqual(expectedID, actualUserVM.UserID);
    }

    [TestMethod]
    public void TestCreateUserAccountWithValidInput()
    {
        // arrange
        const string givenName = "Jim";
        const string surname = "John";
        const string email = "JimJohn@mail.com";
        const string password = "newuser";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _userManager.CreateUserAccount(givenName, surname, email, password);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestCreateUserAccountReturnsFalseWithDuplicateEmail()
    {
        // arrange
        const string givenName1 = "Jimmmy";
        const string surname1 = "Johns";
        const string email1 = "JimmmyJohn@mail.com";
        const string password1 = "newuser";
        bool user1Result = false;
        const string givenName2 = "Jimmmy";
        const string surname2 = "Johns";
        const string email2 = "JimmmyJohn@mail.com";
        const string password2 = "newuser";
        bool user2Result = false;

        const bool expectedResult = false;
        bool actualResult = true;

        // act
        user1Result = _userManager.CreateUserAccount(givenName1, surname1, email1, password1);
        user2Result = _userManager.CreateUserAccount(givenName2, surname2, email2, password2);
        actualResult = (user1Result == user2Result);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestGetUserCountByEmailWithValidEmail() 
    {
        // arrange
        const string email = "testuser1@test.com";
        const int expectedCount = 1;
        int actualCount = 0;

        // act
        actualCount = _userManager.GetUserCountByEmail(email);

        // assert
        Assert.AreEqual(expectedCount, actualCount);
    }

    [TestMethod]
    public void TestGetUserCountByEmailWithInvalidEmail()
    {
        // arrange
        const string email = "testLoser@test.com";
        const int expectedCount = 0;
        int actualCount = 1;

        // act
        actualCount = _userManager.GetUserCountByEmail(email);

        // assert
        Assert.AreEqual(expectedCount, actualCount);
    }

    [TestMethod]
    public void TestAddUserToRoleWithValidInput() 
    {
        // arrange
        const string roleID = "testRole6";
        const int userID = 1;
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _userManager.AddUserToRole(userID, roleID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestAddUserToRoleThrowsArgumentExceptionWithEmptyRoleID()
    {
        // arrange
        const string roleID = "";
        const int userID = 1;
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _userManager.AddUserToRole(userID, roleID);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestAddUserToRoleReturnsFalseWithInvalidUserID()
    {
        // arrange
        const string roleID = "testRole6";
        const int userID = -1;
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _userManager.AddUserToRole(userID, roleID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestResetPasswordSuccedsWithCorrectEmailAndPassword()
    {
        // arrange
        const string email = "testuser1@test.com";
        const string oldPassword = "newuser";
        const string newPassword = "Password!";
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _userManager.ResetPassword(email, oldPassword, newPassword);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestResetPasswordSuccedsWithInCorrectOldPassword()
    {
        // arrange
        const string email = "testuser1@test.com";
        const string oldPassword = "t";
        const string newPassword = "Password!";
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _userManager.ResetPassword(email, oldPassword, newPassword);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }
}
