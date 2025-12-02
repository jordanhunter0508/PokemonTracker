using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class CollectionCard
    {
        public Card Card { get; set; }
        public int CollectionCardID { get; set; }
        public int CollectionID { get; set; }
        public int Quantity { get; set; }
        public bool Owned { get; set; }
    }

    public class CollectionCardVM : CollectionCard
    {
        public string CardType { get; set; }
        public string Name { get; set; }
        public string BoosterID { get; set; }
        public int BoosterNumber { get; set; }
        public string Rarity { get; set; }
    }
}
