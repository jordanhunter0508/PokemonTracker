using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccessInterfaces;
using DataDomain;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class SearchManager : ISearchManager
    {
        public ISearchAccessor _searchAccessor;

        /// <summary>
        /// General SearchManager created for the presentaion layer
        /// </summary>
        public SearchManager() 
        {
            _searchAccessor = new SearchAccessor();
        }

        /// <summary>
        /// Used for testing to pass in fake data
        /// </summary>
        /// <param name="searchAccessor">Set the ISearchManager in the SearchManager</param>
        public SearchManager(ISearchAccessor searchAccessor) 
        {
            _searchAccessor = searchAccessor;
        }

        /// <summary>
        /// Implements from <see cref="ISearchManager"/>
        /// </summary>
        [Obsolete(message: "Use GetCards(FilterOption.CardName) instead.", false)]
        public List<Card> SearchCardsByName(string name)
        {
            List<Card> results = new List<Card>();

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Failed to get list of cards by name. Name was null or blank.");
            }

            try
            {
                results = _searchAccessor.SelectCardsByName(name);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get a search for a list of cards by card name.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ISearchManager"/>
        /// </summary>
        public List<Card> GetCards(FilterOption filter)
        {
            List<Card> results = new List<Card> ();

            try
            {
                results = _searchAccessor.SelectCards(filter);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get a search for a list of cards.", ex);
            }

            return results;
        }
    }
}
