using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class User
    {
        int UserID { get; set; }
        string GivenName { get; set; }
        string Surname { get; set; }
        string Email { get; set; }
        bool Active { get; set; }
    }

    public class UserVM : User 
    {
        List<string> RoleID { get; set; }
    }
}
