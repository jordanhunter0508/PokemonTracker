using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
                CardType = "Trainer",
                Rarity = "Common",
                ArtistID = 2,
            });
            _cards.Add(new Card()
            {
                CardID = 2,
                BoosterID = "Booster 2",
                ElementTypeID = "Element 2",
                Name = "Test Name 2",
                BoosterNumber = 1,
                CardType = "Card",
                Rarity = "Common",
                ArtistID = 2,
            });
            _cards.Add(new Card()
            {
                CardID = 3,
                BoosterID = "Booster 3",
                ElementTypeID = "Element 1",
                Name = "Test Name 1",
                BoosterNumber = 1,
                CardType = "Card",
                Rarity = "Common",
                ArtistID = 1,
            });
            _cards.Add(new Card()
            {
                CardID = 4,
                BoosterID = "Booster 2",
                ElementTypeID = "Element 1",
                Name = "Different Name 1",
                BoosterNumber = 1,
                CardType = "Card",
                Rarity = "Uncommon",
                ArtistID = 1,
            });
            _cards.Add(new Card()
            {
                CardID = 5,
                BoosterID = "Booster 2",
                ElementTypeID = "Element 1",
                Name = "Different Name 2",
                BoosterNumber = 1,
                CardType = "Trainer",
                Rarity = "Uncommon",
                ArtistID = 1,
            });
        }

        /// <summary>
        /// Implements from <see cref="ISearchAccessor"/> used for testing
        /// </summary>
        [Obsolete(message: "Use GetCards(FilterOption.CardName) instead.", false)]
        public List<Card> SelectCardsByName(string name)
        {
            List<Card> results = new List<Card>();

            results = _cards.Where(card => card.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                            .OrderBy(card => card.Name)
                            .ToList();

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ISearchAccessor"/> used for testing
        /// </summary>
        public List<Card> SelectCards(FilterOption filterOption)
        {
            IEnumerable<Card> results = _cards;

            if (!string.IsNullOrWhiteSpace(filterOption.CardName))
            {
                results = results.Where(card => card.Name.Contains(filterOption.CardName, StringComparison.OrdinalIgnoreCase))
                                             .OrderBy(card => card.Name).ToList();
            }

            if (!string.IsNullOrWhiteSpace(filterOption.Rarity))
            {
                results = results.Where(card => string.Equals(card.Rarity, filterOption.Rarity, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filterOption.BoosterID))
            {
                results = results.Where(card => string.Equals(card.BoosterID, filterOption.BoosterID, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filterOption.CardType))
            {
                results = results.Where(card => string.Equals(card.CardType, filterOption.CardType, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filterOption.ElementTypeID))
            {
                results = results.Where(card => string.Equals(card.ElementTypeID, filterOption.ElementTypeID, StringComparison.OrdinalIgnoreCase));
            }

            if (filterOption.ArtistID != 0)
            {
                results = results.Where(card => int.Equals(card.ArtistID, filterOption.ArtistID)).ToList();
            }

            return results.ToList();
        }
    }
}
