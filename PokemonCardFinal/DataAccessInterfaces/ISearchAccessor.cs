using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface ISearchAccessor
    {
        /// <summary>
        /// WARNING OBSOLETE: Use SelectCards(FilterOption.CardName) instead. <br/>
        /// Gets a list of cards for a list view by card name
        /// </summary>
        /// <param name="name">Gets the cards with the matching name</param>
        /// <returns>Returns a list of Cards</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<Card> SelectCardsByName(string name);

        /// <summary>
        /// Gets a list of cards for a list view based on the filter options.
        /// </summary>
        /// <param name="filterOption">Option to get the list of cards by.</param>
        /// <returns>Returns a list of Cards based on the filter option selected.</returns>
        public List<Card> SelectCards(FilterOption filterOption);
    }
}
