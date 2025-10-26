using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface IUserAccessor
    {
        /// <summary>
        /// Requests from the database the number of users with the specified email,the specified password hash<br/>
        /// and an active account. The database check if the user's account is active.
        /// </summary>
        /// <param name="email">Compared against the emails stored in the database</param>
        /// <param name="passwordHash">Compared against the passwordHashs stored in the database</param>
        /// <returns>Returns the count of users with the corresponding email and passwordHash</returns>
        public int AuthenticateUserByEmailAndPasswordHash(string email, string passwordHash);

        /// <summary>
        /// Request from the database a UserID,GivenName,Surname,Email, and Active field.<br/>
        /// Where the the email parameter matches one stored in the database.
        /// </summary>
        /// <param name="email">Compared against the emails stored in the database</param>
        /// <returns>Returns a User object that was found with the matching email</returns>
        public User SelectUserByEmail(string email);

        /// <summary>
        /// Request from the database the RoleID. 
        /// Where the email parameter matches one stored in the database.
        /// </summary>
        /// <param name="email">Compared against the emails stored in the database</param>
        /// <returns>Returns a list of strings that are roles of a specific user</returns>
        public List<String> SelectRoleByUserEmail(string email);

        /// <summary>
        /// Stores the user inputed data into the database
        /// to create a user.
        /// </summary>
        /// <param name="givenName">Given name of the user inputing data</param>
        /// <param name="surname">Surname of the user inputing data</param>
        /// <param name="email">Email of the user inputing data</param>
        /// <param name="passwordHash">Hashed password from the users inputted password</param>
        /// <returns>Returns the number of rows affected. 1 if the account was created succefully.</returns>
        public int CreateUserAccount(string givenName, string surname, string email, string passwordHash);

        /// <summary>
        /// Requests from the database the count of users.
        /// Where the email parameter matches the ones stored in the User table.
        /// </summary>
        /// <param name="email">Compared against the emails in the user table</param>
        /// <returns>Returns the number of users with the specific email</returns>
        public int SelectUserCountByEmail(string email);

        public int AddUserRole(string roleID, string userID);
    }
}
