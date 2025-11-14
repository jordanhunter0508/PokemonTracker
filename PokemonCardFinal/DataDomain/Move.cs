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
        public List<MoveCost> Costs { get; set; }
    }

    public class MoveCost
    {
        public string MoveID { get; set; }
        public string ElementType { get; set; }     // Could use a ElementType object if description is needed
        public int Quantity { get; set; }
    }
}