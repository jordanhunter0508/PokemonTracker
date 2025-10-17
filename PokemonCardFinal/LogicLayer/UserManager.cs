using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

using DataAccessInterfaces;
using DataDomain;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        IUserAccessor _userAccessor;

        /// <summary>
        /// General UserManger created for the presentaion layer
        /// </summary>
        public UserManager() 
        {
            _userAccessor = new UserAccessor();
        }

        /// <summary>
        /// Used for testing to pass in fake data
        /// </summary>
        /// <param name="userAccessor">Set the IUserAccessor in the UserManager</param>
        public UserManager(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Implements from IUserManager
        /// </summary>
        public string HashSha256(string password)
        {
            string result = null;

            if (password.Length == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            byte[] data;

            using (SHA256 sha256hasher = SHA256.Create())
            {
                data = sha256hasher.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            { 
                s.Append(data[i].ToString("x2").ToLower());
            }

            result = s.ToString();

            return result;
        }

        /// <summary>
        /// Implements from IUserManager
        /// </summary>
        public bool AuthenticateUser(string email, string password)
        {
            bool result = false;

            try
            {
                password = HashSha256(password);
                result = (1 == _userAccessor.AuthenticateUserByEmailAndPasswordHash(email, password));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Authentication failed.\n\n",ex);
            }

            return result;
        }

        public User GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public List<string> GetRolesForUser(string email)
        {
            throw new NotImplementedException();
        }

        public UserVM LogInUser(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
