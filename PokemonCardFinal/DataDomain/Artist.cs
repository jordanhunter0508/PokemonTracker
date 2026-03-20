using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Artist
    {
        public int ArtistID { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }

        public string Name
        {
            get
            {
                string name = GivenName;
                if (!String.IsNullOrWhiteSpace(Surname))
                {
                    name += ", " + Surname;
                }
                return name;
            }
        }

        public bool Active { get; set; }
    }
}
