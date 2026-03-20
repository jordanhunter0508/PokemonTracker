using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface IFilterCardManager
    {
        /// <summary>
        /// Filters a IEnumberable<CardVM> with a specified name using linq.<br/>
        /// Used when a list of Cards is already created.
        /// </summary>
        /// <param name="cards">IEnumerable of CardVM to filter</param>
        /// <param name="name">Gets the cards with the matching name</param>
        /// <returns>Returns an IOrderedEnumerable of cards with the specified card name</returns>
        public IEnumerable<Card> FilterByCardName(IEnumerable<Card> cards, string name);

        /// <summary>
        /// Filters a IEnumberable<CardVM> with a specified rarity using linq.<br/>
        /// Used when a list of Cards is already created.
        /// </summary>
        /// <param name="cards">IEnumerable of CardVM to filter</param>
        /// <param name="rarity">Gets the cards with the matching rarity</param>
        /// <returns>Returns an IOrderedEnumerable of cards with the specified card rarity</returns>
        public IEnumerable<Card> FilterByRarity(IEnumerable<Card> cards, string rarity);

        /// <summary>
        /// Filters a IEnumberable<CardVM> with a specified boosterID using linq.<br/>
        /// Used when a list of Cards is already created.
        /// </summary>
        /// <param name="cards">IEnumerable of CardVM to filter</param>
        /// <param name="boosterID">Gets the cards with the matching boosterID</param>
        /// <returns>Returns an IOrderedEnumerable of cards with the specified card boosterID</returns>
        public IEnumerable<Card> FilterByBoosterID(IEnumerable<Card> cards, string boosterID);

        /// <summary>
        /// Filters a IEnumberable<CardVM> with a specified cardType using linq.<br/>
        /// Used when a list of Cards is already created.
        /// </summary>
        /// <param name="cards">IEnumerable of CardVM to filter</param>
        /// <param name="cardType">Gets the cards with the matching cardType</param>
        /// <returns>Returns an IOrderedEnumerable of cards with the specified cardType</returns>
        public IEnumerable<Card> FilterByCardType(IEnumerable<Card> cards, string cardType);

        /// <summary>
        /// Filters a IEnumberable<CardVM> with a specified elementTypeID using linq.<br/>
        /// Used when a list of Cards is already created.
        /// </summary>
        /// <param name="cards">IEnumerable of CardVM to filter</param>
        /// <param name="elementTypeID">Gets the cards with the matching elementTypeID</param>
        /// <returns>Returns an IOrderedEnumerable of cards with the specified card elementTypeID</returns>
        public IEnumerable<Card> FilterByElementTypeID(IEnumerable<Card> cards, string elementTypeID);
    }
}
