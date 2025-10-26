using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface IUserManager
    {
        /// <summary>
        /// Uses <see cref="AuthenticateUser"/> to verify the user has an active account.<br/>
        /// Then uses <see cref="GetUserByEmail"/> to get information about the user in the database.<br/>
        /// Then uses <see cref="GetRolesForUser"/> to get the list of roles the user may have.
        /// </summary>
        /// <param name="email">Used to search the database for a matching email</param>
        /// <param name="password">Converted to a Hash then used to find a match in the database</param>
        /// <returns>Returns a UserVM object created from the user's information from the database.</returns>
        /// <exception cref="ApplicationException">Throws if the Authentication fails or if either of the Get methods fail</exception>
        public UserVM LogInUser(string email, string password);

        /// <summary>
        /// Passes parameters to 
        /// <see href="AuthenticateUserByEmailAndPasswordHash(string, string)"/><br/>
        /// to verify with the database the user is valid.
        /// </summary>
        /// <param name="email">String to be used in AuthenticateUserByEmailAndPasswordHash</param>
        /// <param name="password">String to be hashed then used in AuthenticateUserByEmailAndPasswordHash</param>
        /// <returns>Returns true if the user is active and has input a valid email and password. Returns false otherwise</returns>
        /// <exception cref="ApplicationException">Throws if there is an error with the AuthenticateUserByEmailAndPasswordHash</exception>
        public bool AuthenticateUser(string email, string password);

        /// <summary>
        /// Passes parameters to <see href="SelectUserByEmail(string)"/> then returns <br/>
        /// the user with a matching email.
        /// </summary>
        /// <param name="email">Used to search the database for a matching email</param>
        /// <returns>A User object from the database that has the matching email</returns>
        /// <exception cref="ApplicationException">Throws if the email is not found in the database</exception>
        public User GetUserByEmail(string email);

        /// <summary>
        /// Passes parameters to <see href="SelectRoleByUserEmail(string)"/> then returns <br/>
        /// a list of strings of the user roles.
        /// </summary>
        /// <param name="email">Used to search the database for a matching email</param>
        /// <returns>Returns a list of strings that are roles of a specific user</returns>
        /// <exception cref="ApplicationException">Throws if the email is not found in the database</exception>
        public List<String> GetRolesForUser(string email);

        /// <summary>
        /// Converts the inputed password to a Sha256 string
        /// using SHA256 and a StringBuilder.
        /// </summary>
        /// <param name="password">String turned into the Sha256</param>
        /// <returns>Returns a string of password as a Sha256</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if the inputed string lenght is 0</exception>
        public string HashSha256(string password);

        /// <summary>
        /// GetUserCountByEmail
        /// CreateUserAccount
        /// Passes parameters to <see href="CreateUserAccount(string,string,string,string)"/><br/>
        /// then returns true if the account was successfully created.
        /// </summary>
        /// <param name="givenName">Given name of the user inputing data</param>
        /// <param name="surname">Surname of the user inputing data</param>
        /// <param name="email">Email of the user inputing data</param>
        /// <param name="passwordHash">Hashed password from the users inputted password</param>
        /// <returns>Returns true if the account was successfully created, false otherwise</returns>
        /// <exception cref="ApplicationException">Throws there is an error reaching the database</exception>
        public bool RegisterUserAccount(string givenName, string surname, string email, string passwordHash);

        /// <summary>
        /// Passes parameters to <see href="SelectUserCountByEmail(string)"/> then returns <br/>
        /// the number of users with a specified email
        /// </summary>
        /// <param name="email">Used to search the database for matches</param>
        /// <returns>The number of users with a specified email</returns>
        /// /// <exception cref="ApplicationException">Throws there is an error reaching the database</exception>
        public int GetUserCountByEmail(string email);

        public bool AddRoleToUser(int userID, string roleID = "General");

        //public bool ResetPassword(string currentPassword, string newPassword);
        //public bool DeactivateUser(int userID, string email);
        //public bool ActivateUser(int userID, string email);
    }
}
