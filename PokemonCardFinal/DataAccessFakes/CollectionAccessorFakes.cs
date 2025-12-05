using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                CollectionID = 4,
                UserID = 1,
                CollectionTypeID = "type2",
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
                            Name = "test1",
                            Rarity = "rarity1"
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
                            Name= "test2",
                            Rarity = "rarity2"

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
                            Name = "test3",
                            Rarity = "rarity3"
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

        /// <summary>
        /// Implements from <see cref="ICollectionAccessor"/> used for testing
        /// </summary>
        public int DeleteCollection(int collectionID)
        {
            int count = 0;
            Collection deletedCollection = null;

            foreach (var collection in _collections)
            {
                if (collection.CollectionID == collectionID)
                {
                    deletedCollection = collection;
                }
            }

            if (deletedCollection != null)
            {
                _collections.Remove(deletedCollection);
                count = 1;
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="ICollectionAccessor"/> used for testing
        /// </summary>
        public int DeleteCollectionCard(int collectionCardID)
        {
            int count = 0;
            CollectionCard deletedCard = null;
            int index = -1;

            foreach (var collectionVM in _collectionVMs)
            {
                foreach (var card in collectionVM.Cards)
                {
                    if (card.CollectionCardID == collectionCardID)
                    {
                        deletedCard = card;
                    }
                }
            }

            if (deletedCard != null || index > -1)
            {
                _collectionVMs[0].Cards.Remove(deletedCard);
                count = 1;
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="ICollectionAccessor"/> used for testing
        /// </summary>
        public int InsertCollectionCard(CollectionCard collectionCard)
        {
            int count = 0;
            int index = 0;

            // Represents the PokemonCard table's IDs
            int[] cardIDs = { 1, 2, 3, 4, 5, 6, 7, 9, 10 };

            // Checks the CollectionID is valid
            if (SelectCollectionByCollectionID(collectionCard.CollectionID) == null)
            {
                throw new Exception("MoveID does not have a corresponding Move.");
            }

            // Checks if the card has a valid value or not
            if (!cardIDs.Contains(collectionCard.Card.CardID))
            {
                throw new Exception("CardID is not valid.");
            }

            // used for the CollectionVMs list
            for (int i = 0; i < _collectionVMs.Count; i++)
            {
                // used for the List of CollectionCards inside the CollectionVM
                for (int j = 0; j < _collectionVMs.Count; j++)
                {
                    if (_collectionVMs[i].CollectionID == collectionCard.CollectionID &&
                    _collectionVMs[i].Cards[j].Card.CardID == collectionCard.Card.CardID)
                    {
                        throw new Exception("Both CollectionID and CardID are duplicated.");
                    }
                }
            }

            _collectionVMs[0].Cards.Add(collectionCard);
            count = 1;
            return count;
        }
    }

    internal class CollectionType
    {
        public string CollectionTypeID { get; set; }
        public string Description { get; set; }
        public int MaxSize { get; set; }
    }
}
