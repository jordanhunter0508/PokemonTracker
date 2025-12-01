using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface ICollectionManager
    {
        /// <summary>
        /// Passes parameters to <see href="SelectCollectionCardsByCollectionID(int)"/><br/>
        /// then returns the results of the query. 
        /// </summary>
        /// <param name="collectionID">Used to search the database for the Collection of cards</param>
        /// <returns>Returns a list of Collection cards from the database where the collectionIDs match</returns>
        /// <exception cref="ApplicationException">Throws if the collectionID could not be found</exception>
        public List<CollectionCard> GetCollectionCardsByCollectionID(int collectionID);
    }
}
