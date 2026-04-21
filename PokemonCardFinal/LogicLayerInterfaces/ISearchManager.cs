using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface ISearchManager
    {
        /// <summary>
        /// WARNING OBSOLETE: Use GetCards(FilterOption.CardName) instead. <br/>
        /// Gets a list of cards for a list view, only cards
        /// that contain the parameter
        /// </summary>
        /// <param name="name">Gets the cards with the matching name</param>
        /// <returns>Returns a list of Cards</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<Card> SearchCardsByName(string name);

        
        /// <summary>
        /// Gets a list of cards for a list view based on the filter options.
        /// </summary>
        /// <param name="filterOption">Option to get the list of cards by.</param>
        /// <returns>Returns a list of Cards based on the filter option selected.</returns>
        /// <exception cref="ApplicationException">Throws if ther is an error connecting to the database</exception>
        public List<Card> GetCards(FilterOption filterOption);
    }
}