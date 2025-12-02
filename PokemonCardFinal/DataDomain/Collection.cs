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

    }

    public class CollectionCard
    {
        public Card Card { get; set; }
        public int CollectionCardID { get; set; }
        public int CollectionID { get; set; }
        public int Quantity { get; set; }
        public bool Owned { get; set; }
    }
}
