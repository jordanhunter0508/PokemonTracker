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
        public bool Active { get; set; }
    }
}
