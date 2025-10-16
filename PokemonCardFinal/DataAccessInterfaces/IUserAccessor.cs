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
        public int AuthenticateUserByEmailAndPasswordHash(string email, string passwordHash);
        public User SelectUserByEmail(string email);
        public List<String> SelectRoleByUserEmail(string email);
    }
}
