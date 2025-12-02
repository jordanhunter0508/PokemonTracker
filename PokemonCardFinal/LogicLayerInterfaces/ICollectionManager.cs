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
        /// <exception cref="ApplicationException">Throws if there was an error connecting</exception>
        public List<CollectionCard> GetCollectionCardsByCollectionID(int collectionID);

        /// <summary>
        /// Passes parameters to <see href="SelectCollectionElementsByCollectionID(int)"/><br/>
        /// then returns the results of the query. 
        /// </summary>
        /// <param name="collectionID">Used to search the database for the element types of a collection</param>
        /// <returns>Returns a list of strings from the database where the collectionIDs match</returns>
        /// <exception cref="ApplicationException">Throws if there was an error connecting</exception>
        public List<string> GetCollectionElementsByCollectionID(int collectionID);

        /// <summary>
        /// Passes parameters to <see href="SelectCollectionTypeMaxSize(string)"/><br/>
        /// then returns the results of the query. 
        /// </summary>
        /// <param name="collectionTypeID">Used to search for the max size</param>
        /// <returns>Returns a the max size of the collection type</returns>
        /// <exception cref="ApplicationException">Throws if there was an error connecting</exception>
        public int GetCollectionTypeMaxSize(string collectionTypeID);

        /// <summary>
        /// Passes parameters to <see href="SelectCollectionByCollectionID(int)"/><br/>
        /// then returns the results of the query. 
        /// </summary>
        /// <param name="collectionID">Used to search the database for the collection</param>
        /// <returns>Returns a Collection from the database where the collectionIDs match</returns>
        /// <exception cref="ApplicationException">Throws if there was an error connecting</exception>
        public Collection GetCollectionByCollectionID(int collectionID);

        /// <summary>
        /// Uses GetCollectionCardsByCollectionID, GetCollectionElementsByCollectionID,<br/>
        /// GetCollectionTypeMaxSize and GetCollectionByCollectionID to create a CollectionVM.
        /// </summary>
        /// <param name="collectionID">Used to search the database for the Collection and it's components</param>
        /// <returns>Returns a CollectionVM from the database where the collectionID matchs in the <br/>
        /// Collection, CollectionType, and CollectionCard tables.</returns>
        /// <exception cref="ApplicationException">Throws if there was an error connecting</exception>
        public CollectionVM GetCollectionVMByCollectionID(int collectionID);

        /// <summary>
        /// Gets the CollectionID from the user's Collection list. Where the collectionTypeID matches.
        /// </summary>
        /// <param name="user">User to search through the collectuions for</param>
        /// <param name="collectionTypeID">Used to search the collectionTypeID of the user's collections</param>
        /// <returns>Returns the CollectionID of where the collectionTypeID matches a type in user.Collections</returns>
        /// <exception cref="ApplicationException">Throws if there was an error matching the type to the user's collection</exception>
        public int GetCollectionIDByCollectionType(UserVM user,string collectionTypeID);

        /// <summary>
        /// Uses the Card within the CollectionCard to create a CollectionCardVM
        /// </summary>
        /// <param name="collectionCards">List of CollectionCards to convert</param>
        /// <returns>Returns a list of CollectionCardVMs from the original CollectionCard list</returns>
        /// <exception cref="ApplicationException">Throws if there was an error converting the card</exception>
        public List<CollectionCardVM> ConvertCollectionCardToVM(List<CollectionCard> collectionCards);
    }
}
