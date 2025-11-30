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
        public int InsertUserIntoUser(string givenName, string surname, string email, string passwordHash);

        /// <summary>
        /// Requests from the database the count of users.
        /// Where the email parameter matches the ones stored in the User table.
        /// </summary>
        /// <param name="email">Compared against the emails in the user table</param>
        /// <returns>Returns the number of users with the specific email</returns>
        public int SelectUserCountByEmail(string email);

        /// <summary>
        /// Updates the password hash of the user with a matching email.
        /// </summary>
        /// <param name="email">Email of the user to update</param>
        /// <param name="currentPassword">Users current password hash</param>
        /// <param name="newPassword">Users new password hash</param>
        /// <returns>Returns the number of rows affected</returns>
        public int UpdatePasswordHashByEmail(string email, string currentPassword, string newPassword);

        /// <summary>
        /// Inserts a user and role to the UserRole table
        /// Used when creating an account.
        /// </summary>
        /// <param name="userID">Specified user to add to roles</param>
        /// <param name="roleID">Role the user is being assigned</param>
        /// <returns>Returns 0 if the data got inserted correctly</returns>
        public int InsertUserIntoRole(int userID, string roleID = "General");

        /// <summary>
        /// Insert a user collection for all cards,favorites, and wishlist.
        /// </summary>
        /// <param name="userID">Specified user to add the collections to</param>
        /// <returns>Returns true if the collections were created successfully.</returns>
        /// <exception cref="ApplicationException">Throws if failed to add to the collection.</exception>
        public int InsertDefaultUserCollections(int userID);
    }
}
