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

        /// <summary>
        /// Requests all ElementTypeIDs relateed to the collection.
        /// </summary>
        /// <param name="collectionID">Used to search the database for the element types of a collection</param>
        /// <returns>Returns a list of strings from the database where the collectionIDs match</returns>
        public List<string> SelectCollectionElementsByCollectionID(int collectionID);

        /// <summary>
        /// Requests the MaxSize field from the CollectionType table.
        /// </summary>
        /// <param name="collectionTypeID">Used to search for the max size</param>
        /// <returns>Returns a the max size of the collection type</returns>
        public int SelectCollectionTypeMaxSize(string collectionTypeID);

        /// <summary>
        /// Requests al fields from the Collection table.
        /// </summary>
        /// <param name="collectionID">Used to search the database for the collection</param>
        /// <returns>Returns a Collection from the database where the collectionIDs match</returns>
        public Collection SelectCollectionByCollectionID(int collectionID);

        /// <summary>
        /// Deletes the row from the database where collectionID matches on in the table.<br/>
        /// Also deletes the rows from CollectionCard, and CollectionElement
        /// </summary>
        /// <param name="collectionID">CollectionID of the row to delete.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int DeleteCollection(int collectionID);

        /// <summary>
        /// Deletes the row from the database where CollectionCardID matches on in the table.<br/>
        /// </summary>
        /// <param name="collectionCardID">CollectionCardID of the row to delete.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int DeleteCollectionCard(int collectionCardID);

        /// <summary>
        /// Inserts the parameters into the stored procedure to try
        /// and create a new record for a CollectionCard.
        /// </summary>
        /// <param name="card">New CollectionCard object to insert.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int InsertCollectionCard(CollectionCard collectionCard);
    }
}
