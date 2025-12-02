using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using DataAccess;
using DataAccessInterfaces;
using DataDomain;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class CollectionManager : ICollectionManager
    {
        ICollectionAccessor _collectionAccessor;

        /// <summary>
        /// General CollectionManager created for the presentaion layer
        /// </summary>
        public CollectionManager()
        {
            _collectionAccessor = new CollectionAccessor();
        }

        /// <summary>
        /// Used for testing to pass in fake data
        /// </summary>
        /// <param name="collectionAccessor">Set the ICollectionAccessor in the CollectionManager</param>
        public CollectionManager(ICollectionAccessor collectionAccessor)
        {
            _collectionAccessor = collectionAccessor;
        }

        /// <summary>
        /// Implements from <see cref="ICollectionManager"/>
        /// </summary>
        public List<CollectionCard> GetCollectionCardsByCollectionID(int collectionID)
        {
            List<CollectionCard> results = new List<CollectionCard>();

            try
            {
                results = _collectionAccessor.SelectCollectionCardsByCollectionID(collectionID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get a list of collection cards.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICollectionManager"/>
        /// </summary>
        public List<string> GetCollectionElementsByCollectionID(int collectionID)
        {
            List<string> results = new List<string>();

            try
            {
                results = _collectionAccessor.SelectCollectionElementsByCollectionID(collectionID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get a list of collection elements.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICollectionManager"/>
        /// </summary>
        public int GetCollectionTypeMaxSize(string collectionTypeID)
        {
            int result = -1;

            if (collectionTypeID == null || collectionTypeID.Replace(" ","") == "")
            {
                throw new ArgumentNullException("CollectionTypeID was invalid.");
            }

            try
            {
                result = _collectionAccessor.SelectCollectionTypeMaxSize(collectionTypeID);

                if (result == -1)
                {
                    throw new ApplicationException("Collection Type returned an invalid max size.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get max size of a collection type.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="ICollectionManager"/>
        /// </summary>
        public Collection GetCollectionByCollectionID(int collectionID)
        {
            Collection result = null;

            try
            {
                result = _collectionAccessor.SelectCollectionByCollectionID(collectionID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get collection.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="ICollectionManager"/>
        /// </summary>
        public CollectionVM GetCollectionVMByCollectionID(int collectionID)
        {
            CollectionVM result = null;

            try
            {
                Collection collection = GetCollectionByCollectionID(collectionID);

                result = new CollectionVM()
                {
                    CollectionID = collection.CollectionID,
                    UserID = collection.UserID,
                    CollectionTypeID = collection.CollectionTypeID,
                    Name = collection.Name,
                    Description = collection.Description,
                    Cards = GetCollectionCardsByCollectionID(collectionID),
                    ElementTypeIDs = GetCollectionElementsByCollectionID(collectionID),
                    MaxSize = GetCollectionTypeMaxSize(collection.CollectionTypeID)
                };
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get collection view model.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="ICollectionManager"/>
        /// </summary>
        public int GetCollectionIDByCollectionType(UserVM user,string collectionTypeID)
        {
            int collectionID = -1;

            foreach (var collection in user.Collections)
            {
                if (collection.CollectionTypeID.ToLower() == collectionTypeID.ToLower())
                {
                    collectionID = collection.CollectionID;
                }
            }

            if (collectionID == -1)
            {
                throw new ApplicationException("Failed to get Collection ID by Collection Type.");
            }

            return collectionID;
        }

        /// <summary>
        /// Implements from <see cref="ICollectionManager"/>
        /// </summary>
        public List<CollectionCardVM> ConvertCollectionCardToVM(List<CollectionCard> collectionCards)
        {
            List<CollectionCardVM> results = new List<CollectionCardVM>();

            if (collectionCards == null)
            {
                throw new ApplicationException("Faild to convert CollectionCards to a VM. CollectionCard was null.");
            }

            foreach (var collectionCard in collectionCards)
            {
                results.Add(new CollectionCardVM() 
                {
                    CardType = collectionCard.Card.CardType,
                    Name = collectionCard.Card.Name,
                    BoosterID = collectionCard.Card.BoosterID,
                    BoosterNumber = collectionCard.Card.BoosterNumber,
                    Rarity = collectionCard.Card.Rarity,
                    CollectionCardID = collectionCard.CollectionCardID,
                    CollectionID = collectionCard.CollectionCardID,
                    Quantity = collectionCard.Quantity,
                    Owned = collectionCard.Owned,
                });
            }

            return results;
        }
    }
}
