using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    /// <summary>
    /// Used to specify which Filter Option was selected
    /// Mulitple can have values
    /// </summary>
    public class FilterOption
    {
        public string CardName { get; set; } = string.Empty;
        public string BoosterID { get; set; } = string.Empty;
        public string Rarity { get; set; } = string.Empty;
        public string CardType { get; set; } = string.Empty;
        public string ElementTypeID { get; set; } = string.Empty;
        public int ArtistID { get; set; } = 0;
    }
}
