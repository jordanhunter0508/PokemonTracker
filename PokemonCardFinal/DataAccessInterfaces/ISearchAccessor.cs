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
        /// Gets a list of cards for a list view
        /// That only contain the properties
        ///     PokemonCardID
        ///     CardType
        ///     CardName
        ///     BoosterID
        ///     BoosterNumber
        ///     ElementTypeID
        ///     Rarity
        /// </summary>
        /// <param name="name">Gets the cards with the matching name</param>
        /// <returns>Returns a list of Cards</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<Card> SelectCardsByName(string name);
    }
}
