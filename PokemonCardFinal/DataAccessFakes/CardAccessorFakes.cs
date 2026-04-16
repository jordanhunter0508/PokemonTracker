using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class CardAccessorFakes : ICardAccessor
    {
        List<Card> _cards;

        /// <summary>
        /// Fills the _cards list with fake data
        /// </summary>
        public CardAccessorFakes()
        {
            _cards = new List<Card>();
            _cards.Add(new Card()
            {
                CardID = 1,
                ArtistID = 1,
                AbilityID = "test ability 1",
                BoosterID = "test booster 1",
                PokemonRuleID = "test pokemon rule 1",
                ElementTypeID = "test element 1",
                Name = "test 1",
                BoosterNumber = 1,
                CardType = "test type 1",
                Rarity = "test rarity 1",
                WeaknessType = "weakness 1",
                ResistanceType = "resistance 1",
                WeaknessValue = 1,
                ResistanceValue = 1,
                RetreatCost = 1,
                Health = 100,
                Stage = "test stage",
                ImagePath = "default_image.png"
            });
            _cards.Add(new Card()
            {
                CardID = 2,
                ArtistID = 1,
                AbilityID = "test ability 2",
                BoosterID = "test booster 1",
                PokemonRuleID = "test pokemon rule 2",
                ElementTypeID = "test element 1",
                Name = "test 2",
                BoosterNumber = 2,
                CardType = "test type 1",
                Rarity = "test rarity 2",
                WeaknessType = "weakness 2",
                ResistanceType = "resistance 2",
                WeaknessValue = 1,
                ResistanceValue = 1,
                RetreatCost = 1,
                Health = 100,
                Stage = "test stage",
                ImagePath = "default_image.png"
            });
            _cards.Add(new Card()
            {
                CardID = 3,
                ArtistID = 2,
                AbilityID = "test ability 1",
                BoosterID = "test booster 3",
                PokemonRuleID = "test pokemon rule 3",
                ElementTypeID = "test element 2",
                Name = "test 1",
                BoosterNumber = 1,
                CardType = "test type 3",
                Rarity = "test rarity 1",
                WeaknessType = "weakness 1",
                ResistanceType = "resistance 1",
                WeaknessValue = 1,
                ResistanceValue = 1,
                RetreatCost = 1,
                Health = 100,
                Stage = "test stage",
                ImagePath = "default_image.png"
            });
            _cards.Add(new Card()
            {
                CardID = 4,
                ArtistID = 2,
                AbilityID = "test ability 2",
                BoosterID = "test booster 3",
                PokemonRuleID = "test pokemon rule 1",
                ElementTypeID = "test element 2",
                Name = "test 3",
                BoosterNumber = 2,
                CardType = "test type 1",
                Rarity = "test rarity 1",
                WeaknessType = "weakness 2",
                ResistanceType = "resistance 1",
                WeaknessValue = 1,
                ResistanceValue = 1,
                RetreatCost = 1,
                Health = 100,
                Stage = "test stage",
                ImagePath = "default_image.png"
            });
            _cards.Add(new Card()
            {
                CardID = 5,
                ArtistID = 1,
                AbilityID = "test ability 1",
                BoosterID = "test booster 1",
                PokemonRuleID = "test pokemon rule 1",
                ElementTypeID = "test element 2",
                Name = "test 4",
                BoosterNumber = 3,
                CardType = "test type 3",
                Rarity = "test rarity 2",
                WeaknessType = "weakness 2",
                ResistanceType = "resistance 2",
                WeaknessValue = 1,
                ResistanceValue = 1,
                RetreatCost = 1,
                Health = 100,
                Stage = "test stage",
                ImagePath = "default_image.png"
            });
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public Card SelectCardByCardID(int cardID)
        {
            Card resultCard = null;

            foreach (Card card in _cards)
            {
                if (card.CardID == cardID)
                {
                    resultCard = card;
                }
            }

            return resultCard;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public List<Card> SelectAllCards()
        {
            List<Card> results = new List<Card>();
            results = _cards;
            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public PaginatedResult<Card> SelectCardsPaginated(FilterOption filterOption, int pageNumber = 1, int pageSize = 25)
        {
            PaginatedResult<Card> results = new PaginatedResult<Card>();

            results.Items = _cards;

            if (!string.IsNullOrWhiteSpace(filterOption.CardName))
            {
                results.Items = results.Items.Where(card => card.Name.Contains(filterOption.CardName, StringComparison.OrdinalIgnoreCase))
                                             .OrderBy(card => card.Name).ToList();
            }

            if (!string.IsNullOrWhiteSpace(filterOption.Rarity))
            {
                results.Items = results.Items.Where(card => string.Equals(card.Rarity, filterOption.Rarity, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(filterOption.BoosterID))
            {
                results.Items = results.Items.Where(card => string.Equals(card.BoosterID, filterOption.BoosterID, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(filterOption.CardType))
            {
                results.Items = results.Items.Where(card => string.Equals(card.CardType, filterOption.CardType, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(filterOption.ElementTypeID))
            {
                results.Items = results.Items.Where(card => string.Equals(card.ElementTypeID, filterOption.ElementTypeID, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (filterOption.ArtistID != 0)
            {
                results.Items = results.Items.Where(card => int.Equals(card.ArtistID, filterOption.ArtistID)).ToList();
            }

            results.TotalCount = results.Items.Count();
            results.PageNumber = pageNumber;
            results.PageSize = pageSize;
            results.TotalPages = (int)Math.Ceiling((double)results.Items.Count() / pageSize);

            results.Items = results.Items.Skip((pageNumber - 1) * pageSize)
                                          .Take(pageSize)
                                          .ToList();

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public int InsertCard(Card card)
        {
            int count = 0;

            // values used to represent the tables in the database
            // these are to make sure the foreign key is valid
            int[] validArtists = { 1, 2, 3, 4, 5 };
            string[] validAbilities = { "test ability 1", "test ability 2", "test ability 3", "test ability 4" };
            string[] validBoosters = { "test booster 1", "test booster 2", "test booster 3", "test booster 4" };
            string[] validRules = { "test pokemon rule 1", "test pokemon rule 2", "test pokemon rule 3", "test pokemon rule 4" };
            string[] validElements = { "test element 1", "test element 2", "test element 3", "test element 4" };

            if (!validArtists.Contains(card.ArtistID))
            {
                throw new Exception("Invalid ArtistID");
            }
            if (!validAbilities.Contains(card.AbilityID))
            {
                throw new Exception("Invalid AbilityID");
            }
            if (!validBoosters.Contains(card.BoosterID))
            {
                throw new Exception("Invalid BoosterID");
            }
            if (!validRules.Contains(card.PokemonRuleID))
            {
                throw new Exception("Invalid PokemonRuleID");
            }
            if (!validElements.Contains(card.ElementTypeID))
            {
                throw new Exception("Invalid ElementTypeID");
            }
            if (card.CardType == null)
            {
                throw new Exception("Invalid CardType");
            }
            if (card.Name == null)
            {
                throw new Exception("Invalid CardType");
            }
            if (card.Rarity == null)
            {
                throw new Exception("Invalid CardType");
            }
            if (card.ResistanceType == null)
            {
                throw new Exception("Invalid CardType");
            }
            if (card.WeaknessType == null)
            {
                throw new Exception("Invalid CardType");
            }
            if (card.Stage == null)
            {
                throw new Exception("Invalid CardType");
            }

            foreach (Card element in _cards)
            {
                if (element.BoosterID == card.BoosterID &&
                    element.BoosterNumber == card.BoosterNumber &&
                    element.Rarity == card.Rarity)
                {
                    throw new Exception("Unique Constraint (BoosterID, BoosterNumber, Rarity) already used.");
                }
            }

            _cards.Add(card);
            count = card.CardID;

            return count;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public int UpdateCard(Card card)
        {
            int count = 0;
            int index = -1;

            // values used to represent the tables in the database
            // these are to make sure the foreign key is valid
            int[] validArtists = { 1, 2, 3, 4, 5 };
            string[] validAbilities = { "test ability 1", "test ability 2", "test ability 3", "test ability 4" };
            string[] validBoosters = { "test booster 1", "test booster 2", "test booster 3", "test booster 4" };
            string[] validRules = { "test pokemon rule 1", "test pokemon rule 2", "test pokemon rule 3", "test pokemon rule 4" };
            string[] validElements = { "test element 1", "test element 2", "test element 3", "test element 4" };

            if (!validArtists.Contains(card.ArtistID))
            {
                throw new Exception("Invalid ArtistID");
            }
            if (!validAbilities.Contains(card.AbilityID))
            {
                throw new Exception("Invalid AbilityID");
            }
            if (!validBoosters.Contains(card.BoosterID))
            {
                throw new Exception("Invalid BoosterID");
            }
            if (!validRules.Contains(card.PokemonRuleID))
            {
                throw new Exception("Invalid PokemonRuleID");
            }
            if (!validElements.Contains(card.ElementTypeID))
            {
                throw new Exception("Invalid ElementTypeID");
            }
            if (card.CardType == null)
            {
                throw new Exception("Invalid CardType");
            }
            if (card.Name == null)
            {
                throw new Exception("Invalid CardType");
            }
            if (card.Rarity == null)
            {
                throw new Exception("Invalid CardType");
            }
            if (card.ResistanceType == null)
            {
                throw new Exception("Invalid CardType");
            }
            if (card.WeaknessType == null)
            {
                throw new Exception("Invalid CardType");
            }
            if (card.Stage == null)
            {
                throw new Exception("Invalid CardType");
            }

            for (int i = 0; i < _cards.Count; i++)
            {
                if (_cards[i].CardID != card.CardID &&
                    _cards[i].BoosterID == card.BoosterID &&
                    _cards[i].BoosterNumber == card.BoosterNumber &&
                    _cards[i].Rarity == card.Rarity)
                {
                    throw new Exception("Unique Constraint (BoosterID, BoosterNumber, Rarity) already used.");
                }

                if (_cards[i].CardID == card.CardID)
                {
                    index = i;
                }
            }

            if (index != -1)
            {
                _cards[index] = card;
                count = 1;
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public int DeleteCard(int cardID)
        {
            int count = 0;
            Card deletedCard = null;

            foreach (Card card in _cards)
            {
                if (card.CardID == cardID)
                {
                    deletedCard = card;
                    break;
                }
            }

            if (deletedCard != null)
            {
                _cards.Remove(deletedCard);
                count = 1;
            }

            return count;
        }  
    }
}
