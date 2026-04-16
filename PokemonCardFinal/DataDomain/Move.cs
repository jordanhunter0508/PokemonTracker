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
        public int MoveID { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }

    public class MoveVM : Move
    {
        public List<MoveCost> Costs { get; set; }
        public string ElementTypes 
        { 
            // Returns all the Element Types from Costs
            // Joins them together using a LINQ
            get 
            {
                string result = "none";

                if (Costs != null &&  Costs.Count > 0)
                {
                    result = string.Join(", ", Costs.Select(c => c.ElementType));
                }

                return result;
            } 
        }

        public int TotalCost
        {
            // Returns the sum of all Quantities
            // from the Costs using LINQ
            get
            {
                int result = 0;

                if (Costs != null && Costs.Count > 0)
                {
                    result = Costs.Sum(c => c.Quantity);
                }

                return result;
            }
        }
    }

    public class MoveCost
    {
        public int MoveID { get; set; }
        public string ElementType { get; set; }
        public int Quantity { get; set; }
    }
}