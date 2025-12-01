using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface ICollectionAccessor
    {
        /// <summary>
        /// Requests all fields from the Card table, as well as<br/>
        /// the quanitity and the owned field from CollectionCard.
        /// </summary>
        /// <param name="collectionID">Used to search the database for collection cards that match</param>
        /// <returns>Returns a List of all collection cards in the database that are in the corresponding collection.</returns>
        public List<CollectionCard> SelectCollectionCardsByCollectionID(int collectionID);
    }
}
