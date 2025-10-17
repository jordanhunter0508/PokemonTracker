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
        //
        public UserVM LogInUser(string email, string password);

        /// <summary>
        /// Passes parameters to 
        /// <see href="AuthenticateUserByEmailAndPasswordHash(email, passwordHash)"/><br/>
        /// to verify with the database the user is valid.
        /// </summary>
        /// <param name="email">String to be used in AuthenticateUserByEmailAndPasswordHash</param>
        /// <param name="password">String to be hashed then used in AuthenticateUserByEmailAndPasswordHash</param>
        /// <returns>Returns true if the user is active and has input a valid email and password. Returns false otherwise</returns>
        /// <exception cref="ApplicationException">Throws if there is an error with the AuthenticateUserByEmailAndPasswordHash</exception>
        public bool AuthenticateUser(string email, string password);

        /// <summary>
        /// Passes parameters to <see href="SelectUserByEmail(email)"/> then retuns <br/>
        /// the user with a matching email.
        /// </summary>
        /// <param name="email">Used to search the database for a matching email</param>
        /// <returns>A User object from the database that has the matching email</returns>
        /// <exception cref="ApplicationException">Throws if the email is not found in the database</exception>
        public User GetUserByEmail(string email);
        
        //
        public List<String> GetRolesForUser(string email);

        /// <summary>
        /// Converts the inputed password to a Sha256 string
        /// using SHA256 and a StringBuilder.
        /// </summary>
        /// <param name="password">String turned into the Sha256</param>
        /// <returns>Returns a string of password as a Sha256</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if the inputed string lenght is 0</exception>
        public string HashSha256(string password);
    }
}
