using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class UserAccessorFakes : IUserAccessor
    {
        List<User> _users = new List<User>();
        List<UserVM> _userVMs = new List<UserVM>();
        string _passwordHash;

        /// <summary>
        /// Fills the _users list with fake data
        /// </summary>
        public UserAccessorFakes() 
        {
            // Fake User objects
            _users.Add(new User()
            {
                UserID = 1,
                GivenName = "test",
                Surname = "user",
                Email = "testuser1@test.com",
                Active = true,
            });
            _users.Add(new User()
            {
                UserID = 2,
                GivenName = "john",
                Surname = "doe",
                Email = "testuser2@test.com",
                Active = true,
            });
            _users.Add(new User()
            {
                UserID = 3,
                GivenName = "frank",
                Surname = "smith",
                Email = "testuser3@test.com",
                Active = true,
            });
            _users.Add(new User()
            {
                UserID = 4,
                GivenName = "thomas",
                Surname = "tank",
                Email = "testuser4@test.com",
                Active = true,
            });
            _users.Add(new User()
            {
                UserID = 5,
                GivenName = "leonardo",
                Surname = "turtle",
                Email = "testuser5@test.com",
                Active = false,
            });

            // Fake UserVM objects
            _userVMs.Add(new UserVM()
            {
                UserID = _users[0].UserID,
                GivenName = _users[0].GivenName,
                Surname = _users[0].Surname,
                Email = _users[0].Email,
                Active = _users[0].Active,
                Roles = new List<String>() { "testRole1", "testRole2" }
            });
            _userVMs.Add(new UserVM()
            {
                UserID = _users[1].UserID,
                GivenName = _users[1].GivenName,
                Surname = _users[1].Surname,
                Email = _users[1].Email,
                Active = _users[1].Active,
                Roles = new List<String>() { "testRole3", "testRole4" }
            });
            _userVMs.Add(new UserVM()
            {
                UserID = _users[2].UserID,
                GivenName = _users[2].GivenName,
                Surname = _users[2].Surname,
                Email = _users[2].Email,
                Active = _users[2].Active,
                Roles = new List<String>() { }
            });

            _passwordHash = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
        }

        /// <summary>
        /// Implements from <see cref="IUserAccessor"/> used for testing
        /// </summary>
        public int AuthenticateUserByEmailAndPasswordHash(string email, string passwordHash)
        {
            int result = 0;
            foreach (User user in _users)
            {
                if ((user.Email.Equals(email) && _passwordHash.Equals(passwordHash))
                    && user.Active)
                { 
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IUserAccessor"/> used for testing
        /// </summary>
        public User SelectUserByEmail(string email)
        {
            User result = null;

            foreach (User user in _users)
            {
                if (user.Email == email)
                {
                    result = user;
                }

            }
            if (result == null)
            {
                throw new ArgumentException("Email not found.");
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IUserAccessor"/>  used for testing
        /// </summary>
        public List<string> SelectRoleByUserEmail(string email)
        {
            List<string> results = null;

            foreach (UserVM userVM in _userVMs)
            {
                if (userVM.Email == email)
                {
                    results = userVM.Roles;
                }
            }

            if (results == null)
            {
                throw new ArgumentException("Email not found.");
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IUserAccessor"/>  used for testing
        /// </summary>
        public int InsertUserIntoUser(string givenName, string surname, string email, string passwordHash)
        {
            int userID = 0;

            User newuser = new User()
            {
                UserID = 100,
                GivenName = givenName,
                Surname = surname,
                Email = email,
                Active = true,
            };
            
            _users.Add(newuser);

            userID = newuser.UserID;

            return userID;
        }

        /// <summary>
        /// Implements from <see cref="IUserAccessor"/>  used for testing
        /// </summary>
        public int SelectUserCountByEmail(string email)
        {
            int count = 0;
            foreach (User user in _users)
            {
                if (email == user.Email)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IUserAccessor"/>  used for testing
        /// </summary>
        public int InsertUserIntoRole(int userID, string roleID = "General")
        {
            // Initialized to 1 because a sql error calling this
            // stored procedure returns 1 
            // 0 when inputed successfuly
            int count = 0;

            foreach (UserVM user in _userVMs)
            {
                if (user.UserID == userID)
                {
                    user.Roles.Add(roleID);
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IUserAccessor"/>  used for testing
        /// </summary>
        public int UpdatePasswordHashByEmail(string email, string currentPassword, string newPassword)
        {
            int rows = 0;

            try
            {
                rows = AuthenticateUserByEmailAndPasswordHash(email, currentPassword);
                if (rows == 0)
                {
                    _passwordHash = newPassword;
                }
            }
            catch (Exception)
            { 
                throw new ArgumentException("Invalid email or password.");
            }

            return rows;
        }

    }
}
