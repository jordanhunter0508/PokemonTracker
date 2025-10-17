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
        /// <param name="email">Compared against the email stored in the database</param>
        /// <param name="passwordHash">Compared against the passwordHash stored in the database</param>
        /// <returns>Returns the count of users with the corresponding email and passwordHash</returns>
        public int AuthenticateUserByEmailAndPasswordHash(string email, string passwordHash);
        public User SelectUserByEmail(string email);
        public List<String> SelectRoleByUserEmail(string email);
    }
}
