using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataDomain
{
    public class Move
    {
        public string MoveID { get; set; }
        public int Damage { get; set; }
        public string Description { get; set; }
    }

    public class MoveVM : Move
    {
        public List<string> ElementTypeIDs { get; set; }
        public List<int> Quantity {  get; set; }
    }
}