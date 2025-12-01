using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccess
{
    public class CollectionAccessor : ICollectionAccessor
    {
        /// <summary>
        /// Implements from <see cref="IElementAccessor"/>. Access the database
        /// using sp_select_collection_cards_by_collection_id
        /// </summary>
        public List<CollectionCard> SelectCollectionCardsByCollectionID(int collectionID)
        {
            throw new NotImplementedException();
        }
    }
}
