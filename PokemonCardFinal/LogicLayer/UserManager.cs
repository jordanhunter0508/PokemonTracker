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
        /// Implements from <see cref="IUserManager"/>
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
        /// Implements from <see cref="IUserManager"/>
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
                throw new ApplicationException("Authentication failed.\n\n", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IUserManager"/>
        /// </summary>
        public User GetUserByEmail(string email)
        {
            User result = null;

            try
            {
                result = _userAccessor.SelectUserByEmail(email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User not found.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IUserManager"/>
        /// </summary>
        public List<string> GetRolesForUser(string email)
        {
            List<string> results = null;

            try
            {
                results = _userAccessor.SelectRoleByUserEmail(email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User not found.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IUserManager"/>
        /// </summary>
        public UserVM LogInUser(string email, string password)
        {
            UserVM result = null;
            try
            {
                if (AuthenticateUser(email, password))
                {
                    User user = GetUserByEmail(email);
                    result = new UserVM()
                    {
                        UserID = user.UserID,
                        GivenName = user.GivenName,
                        Surname = user.Surname,
                        Email = user.Email,
                        Active = user.Active,
                        Roles = GetRolesForUser(email)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Faild to log in.", ex);
            }
            return result;
        }

        /// <summary>
        /// Implements from <see cref="IUserManager"/>
        /// </summary>
        public bool RegisterUserAccount(string givenName, string surname, string email, string password)
        {
            bool isRegistered = false;

            try
            {
                string passwordHash = HashSha256(password);

                // Checks if email is already used to prevent sql errors
                if (GetUserCountByEmail(email) == 0)
                {
                    isRegistered = (1 == _userAccessor.CreateUserAccount(givenName, surname, email, passwordHash));
                }
                else
                { 
                    isRegistered = false ;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error creating user account.",ex);
            }

            return isRegistered;
        }

        /// <summary>
        /// Implements from <see cref="IUserManager"/>
        /// </summary>
        public int GetUserCountByEmail(string email)
        {
            int result = 0;

            try
            {
                result = _userAccessor.SelectUserCountByEmail(email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get a count of users by email.");
            }

            return result;
        }

        public bool ResetPassword(string email, string currentPassword, string newPassword)
        {
            bool isUpdated = false;

            try
            {
                currentPassword = HashSha256(currentPassword);
                newPassword = HashSha256(newPassword);
                isUpdated = (1 == _userAccessor.UpdatePasswordHashByEmail(email,currentPassword,newPassword));
                if (!isUpdated) 
                {
                    throw new ApplicationException("Failed to reset password.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Authentication failed.",ex);
            }

            return isUpdated;
        }

        /// <summary>
        /// Implements from <see cref="IUserManager"/>
        /// </summary>
        public bool AddRoleToUser(int userID, string roleID = "General")
        {
            bool result = false;

            if (roleID == "" || roleID == null)
            {
                throw new ArgumentException("RoleID is not valid.");
            }


            try
            {
                result = (1 == _userAccessor.AddUserRole(userID,roleID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to assign user a role.");
            }


            return result;
        }
    }
}
