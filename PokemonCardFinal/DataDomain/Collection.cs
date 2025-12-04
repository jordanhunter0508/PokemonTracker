using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Collection
    {
        public int CollectionID { get; set; }
        public int UserID { get; set; }
        public string CollectionTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CollectionVM : Collection
    {
        public List<CollectionCard> Cards { get; set; }
        public List<string> ElementTypeIDs { get; set; }
        public int MaxSize { get; set; }

        public int CurrentSize
        {
            get
            {
                int count = 0;
                if (Cards == null && Cards.Count == 0)
                {
                    return count;
                }

                foreach (var card in Cards)
                {
                    count += card.Quantity;
                }

                return count;
            }
        }

        public string Elements
        {
            // Returns all the Element Types from ElementTypeIDs
            // Joins them together using a LINQ
            get
            {
                string result = "none";

                if (ElementTypeIDs != null && ElementTypeIDs.Count > 0)
                {
                    result = string.Join(", ", ElementTypeIDs.Select(c => c));
                }

                return result;
            }
        }
    }
}
