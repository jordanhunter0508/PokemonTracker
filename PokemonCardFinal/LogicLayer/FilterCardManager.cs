using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class FilterCardManager : IFilterCardManager
    {
        /// <summary>
        /// Implements from <see cref="IFilterCardManager"/>
        /// </summary>
        public IEnumerable<Card> FilterByCardName(IEnumerable<Card> cards, string name)
        {
            IEnumerable<Card> results = null;

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Failed to get card list by name. Name was null or blank.");
            }
            if (cards == null)
            {
                throw new ArgumentNullException("Failed to get card list by name. Cards was null.");
            }

            results = cards.Where(card => card.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                           .OrderBy(card => card.Name);

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IFilterCardManager"/>
        /// </summary>
        public IEnumerable<Card> FilterByRarity(IEnumerable<Card> cards, string rarity)
        {
            IEnumerable<Card> results = null;

            if (String.IsNullOrWhiteSpace(rarity))
            {
                throw new ArgumentNullException("Failed to get card list by rarity. Rarity was null.");
            }
            if (cards == null)
            {
                throw new ArgumentNullException("Failed to get card list by rarity. Cards was null.");
            }

            results = cards.Where(card => String.Equals(card.Rarity, rarity, StringComparison.OrdinalIgnoreCase))
                           .OrderBy(card => card.BoosterID)
                           .ThenBy(card => card.BoosterNumber);

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IFilterCardManager"/>
        /// </summary>
        public IEnumerable<Card> FilterByBoosterID(IEnumerable<Card> cards, string boosterID)
        {
            IEnumerable<Card> results = null;

            if (String.IsNullOrWhiteSpace(boosterID))
            {
                throw new ArgumentNullException("Failed to get card list by booster id. BoosterID was null.");
            }
            if (cards == null)
            {
                throw new ArgumentNullException("Failed to get card list by booster id. Cards was null.");
            }


            results = cards.Where(card => String.Equals(card.BoosterID, boosterID, StringComparison.OrdinalIgnoreCase))
                           .OrderBy(card => card.BoosterID)
                           .ThenBy(card => card.BoosterNumber);

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IFilterCardManager"/>
        /// </summary>
        public IEnumerable<Card> FilterByCardType(IEnumerable<Card> cards, string cardType)
        {
            IEnumerable<Card> results = null;

            if (String.IsNullOrWhiteSpace(cardType))
            {
                throw new ArgumentNullException("Failed to get card list by card type. CardType was null.");
            }
            if (cards == null)
            {
                throw new ArgumentNullException("Failed to get card list by card type. Cards was null.");
            }

            results = cards.Where(card => String.Equals(card.CardType, cardType, StringComparison.OrdinalIgnoreCase))
                           .OrderBy(card => card.BoosterID)
                           .ThenBy(card => card.BoosterNumber);

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IFilterCardManager"/>
        /// </summary>
        public IEnumerable<Card> FilterByElementTypeID(IEnumerable<Card> cards, string elementTypeID)
        {
            IEnumerable<Card> results = null;

            if (String.IsNullOrWhiteSpace(elementTypeID))
            {
                throw new ArgumentNullException("Failed to get card list by element type id. ElementTypeID was null.");
            }
            if (cards == null)
            {
                throw new ArgumentNullException("Failed to get card list by element type id. Cards was null.");
            }

            results = cards.Where(card => String.Equals(card.ElementTypeID, elementTypeID, StringComparison.OrdinalIgnoreCase))
                           .OrderBy(card => card.BoosterID)
                           .ThenBy(card => card.BoosterNumber);

            return results;
        }

    }
}
