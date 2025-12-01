using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class CollectionAccessorFakes : ICollectionAccessor
    {
        List<CollectionCard> _collectionCards;

        /// <summary>
        /// Fills the _collectionCards list with fake data
        /// </summary>
        public CollectionAccessorFakes() 
        {
            //
        }

        /// <summary>
        /// Implements from <see cref="ICollectionAccessor"/> used for testing
        /// </summary>
        public List<CollectionCard> SelectCollectionCardsByCollectionID(int collectionID)
        {
            throw new NotImplementedException();
        }
    }
}
