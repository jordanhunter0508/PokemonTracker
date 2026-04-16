using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Card
    {

        public int CardID { get; set; }
        public int ArtistID { get; set; }
        public string AbilityID { get; set; }
        public string BoosterID { get; set; }
        public string PokemonRuleID { get; set; }
        public string ElementTypeID { get; set; }
        public string Name { get; set; }
        public int BoosterNumber { get; set; }
        public string CardType { get; set; }
        public string Rarity { get; set; }
        public string WeaknessType { get; set; }
        public string ResistanceType { get; set; }
        public int WeaknessValue { get; set; }
        public int ResistanceValue { get; set; }
        public int RetreatCost { get; set; }
        public int Health { get; set; }
        public string Stage { get; set; }
        public string ImagePath { get; set; }
    }

    public class CardVM : Card
    {
        public List<MoveVM> Moves { get; set; }

        public List<string> AlternateArts { get; set; }
    }
}
