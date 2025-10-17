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
        string _passwordHash;

        /// <summary>
        /// Fills the _users list with fake data
        /// </summary>
        public UserAccessorFakes() 
        {
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

            _passwordHash = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
        }

        /// <summary>
        /// Implements from IUserAccessor used for testing
        /// </summary>
        public int AuthenticateUserByEmailAndPasswordHash(string email, string passwordHash)
        {
            int result = 0;

            foreach (var user in _users)
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
        /// Implements from IUserAccessor used for testing
        /// </summary>
        public List<string> SelectRoleByUserEmail(string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implements from IUserAccessor used for testing
        /// </summary>
        public User SelectUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
