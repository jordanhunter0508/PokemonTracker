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
        List<Collection> _collections;
        List<CollectionVM> _collectionVMs;
        List<CollectionType> _collectionTypes;

        /// <summary>
        /// Fills the _collectionCards list with fake data
        /// </summary>
        public CollectionAccessorFakes()
        {
            _collections = new List<Collection>();
            _collections.Add(new Collection()
            {
                CollectionID = 1,
                UserID = 1,
                CollectionTypeID = "type1",
                Name = "test",
                Description = "test description.",

            });
            _collections.Add(new Collection()
            {
                CollectionID = 2,
                UserID = 1,
                CollectionTypeID = "type1",
                Name = "test",
                Description = "test description.",

            });
            _collections.Add(new Collection()
            {
                CollectionID = 3,
                UserID = 2,
                CollectionTypeID = "type2",
                Name = "test",
                Description = "test description.",

            });

            _collectionVMs = new List<CollectionVM>();
            _collectionVMs.Add(new CollectionVM()
            {
                CollectionID = _collections[0].CollectionID,
                UserID = _collections[0].UserID,
                CollectionTypeID = _collections[0].CollectionTypeID,
                Name = _collections[0].Name,
                Description = _collections[0].Description,
                Cards = new List<CollectionCard>()
                {
                    new CollectionCard()
                    {
                        Card = new Card()
                        {
                            CardID = 1,
                        },
                        CollectionCardID = 1,
                        Quantity = 1,
                        Owned = true,
                    },
                    new CollectionCard()
                    {
                        Card = new Card()
                        {
                            CardID = 2,
                        },
                        Quantity = 4,
                        CollectionCardID = 1,
                        Owned = true,
                    }
                },
                ElementTypeIDs = new List<string> { "testType" },
            });
            _collectionVMs.Add(new CollectionVM()
            {
                CollectionID = _collections[1].CollectionID,
                UserID = _collections[1].UserID,
                CollectionTypeID = _collections[1].CollectionTypeID,
                Name = _collections[1].Name,
                Description = _collections[1].Description,
                Cards = new List<CollectionCard>()
                {
                    new CollectionCard()
                    {
                        Card = new Card()
                        {
                            CardID = 3,
                        },
                        Quantity = 1,
                        CollectionCardID = 2,
                        Owned = true,
                    }
                },
                ElementTypeIDs = new List<string> { },
            });

            _collectionTypes = new List<CollectionType>();
            _collectionTypes.Add(new CollectionType() 
            {
                CollectionTypeID = "type1",
                Description = "Test collection type 1",
                MaxSize = 100,
            });
            _collectionTypes.Add(new CollectionType() 
            {
                CollectionTypeID = "type2",
                Description = "Test collection type 2",
                MaxSize = 200,
            });
        }

        /// <summary>
        /// Implements from <see cref="ICollectionAccessor"/> used for testing
        /// </summary>
        public List<CollectionCard> SelectCollectionCardsByCollectionID(int collectionID)
        {
            List<CollectionCard> results = new List<CollectionCard>();

            foreach (var collection in _collectionVMs)
            {
                if (collection.CollectionID == collectionID)
                {
                    results = collection.Cards;
                }
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICollectionAccessor"/> used for testing
        /// </summary>
        public List<string> SelectCollectionElementsByCollectionID(int collectionID)
        {
            List<string> results = new List<string>();

            foreach (var collection in _collectionVMs)
            {
                if (collection.CollectionID == collectionID)
                { 
                    results = collection.ElementTypeIDs;
                }
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICollectionAccessor"/> used for testing
        /// </summary>
        public int SelectCollectionTypeMaxSize(string collectionTypeID)
        {
            int result = -1;

            foreach (var collectionType in _collectionTypes)
            {
                if (collectionType.CollectionTypeID == collectionTypeID)
                { 
                    result = collectionType.MaxSize;
                }
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="ICollectionAccessor"/> used for testing
        /// </summary>
        public Collection SelectCollectionByCollectionID(int collectionID)
        {
            Collection result = null;

            foreach (var collection in _collections)
            {
                if (collection.CollectionID == collectionID)
                { 
                    result = collection;
                }
            }

            return result;
        }
    }

    internal class CollectionType
    { 
        public string CollectionTypeID { get; set; }
        public string Description { get; set; }
        public int MaxSize { get; set; }
    }
}
