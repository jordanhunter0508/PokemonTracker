using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class SearchAccessorFakes : ISearchAccessor
    {
        List<Card> _cards;

        /// <summary>
        /// filss _cards with fake data used for testing
        /// </summary>
        public SearchAccessorFakes() 
        {
            _cards = new List<Card>();
            _cards.Add(new Card()
            { 
                CardID = 1,
                BoosterID = "Booster 1",
                ElementTypeID = "Element 1",
                Name = "Test Name 1",
                BoosterNumber = 1,
                CardType = "Card",
                Rarity = "Common",
            });
            _cards.Add(new Card()
            { 
                CardID = 2,
                BoosterID = "Booster 1",
                ElementTypeID = "Element 1",
                Name = "Test Name 2",
                BoosterNumber = 1,
                CardType = "Card",
                Rarity = "Common",
            });
            _cards.Add(new Card()
            { 
                CardID = 3,
                BoosterID = "Booster 1",
                ElementTypeID = "Element 1",
                Name = "Test Name 1",
                BoosterNumber = 1,
                CardType = "Card",
                Rarity = "Common",
            });
            _cards.Add(new Card()
            { 
                CardID = 4,
                BoosterID = "Booster 1",
                ElementTypeID = "Element 1",
                Name = "Different Name 1",
                BoosterNumber = 1,
                CardType = "Card",
                Rarity = "Common",
            });
            _cards.Add(new Card()
            { 
                CardID = 5,
                BoosterID = "Booster 1",
                ElementTypeID = "Element 1",
                Name = "Different Name 2",
                BoosterNumber = 1,
                CardType = "Card",
                Rarity = "Common",
            });
        }

        /// <summary>
        /// Implements from <see cref="ISearchAccessor"/> used for testing
        /// </summary>
        public List<Card> SelectCardsByName(string name)
        {
            List<Card> results = new List<Card>();

            results = _cards.Where(card => card.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                            .OrderBy(card => card.Name)
                            .ToList();

            return results;
        }
    }
}
