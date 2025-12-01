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
            throw new NotImplementedException();
        }
    }
}
