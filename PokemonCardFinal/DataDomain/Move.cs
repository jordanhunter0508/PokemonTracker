using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Move
    {
        public string MoveID { get; set; }
        public int Damage { get; set; }
        public string Description { get; set; }
    }

    // Vms might include cost
    // List<string> element type
    // List<int> quantity
}
