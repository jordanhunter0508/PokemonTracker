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
        public UserVM LogInUser(string email, string password);
        public bool AuthenticateUser(string email, string password);
        public User GetUserByEmail(string email);
        public List<String> GetRolesForUser(string email);

        public string HashSha256(string password);
    }
}
