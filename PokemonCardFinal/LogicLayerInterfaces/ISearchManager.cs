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
        /// Gets a list of cards for a list view, only cards
        /// that contain the parameter
        /// </summary>
        /// <param name="name">Gets the cards with the matching name</param>
        /// <returns>Returns a list of Cards</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<Card> SearchCardsByName(string name);
    }
}
