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

        //
        public List<String> SelectRoleByUserEmail(string email);
    }
}
