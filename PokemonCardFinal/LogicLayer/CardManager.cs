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
    public class CardManager : ICardManager
    {
        ICardAccessor _cardAccessor;
        ICardComponentAccessor _componentAccessor;

        /// <summary>
        /// General CardManager created for the presentaion layer
        /// </summary>
        public CardManager()
        {
            _cardAccessor = new CardAccessor();
            _componentAccessor = new CardComponentAccessor();
        }

        /// <summary>
        /// Used for testing to pass in fake data
        /// </summary>
        /// <param name="cardAccessor">Set the ICardAccessor in the CardManager</param>
        /// <param name="componentAccessor">Set the ICardComponentAccessor in the CardManager</param>
        public CardManager(ICardAccessor cardAccessor, ICardComponentAccessor componentAccessor)
        {
            _cardAccessor = cardAccessor;
            _componentAccessor = componentAccessor;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public CardVM GetCardVM(int cardID)
        {
            CardVM result = null;

            try
            {
                Card card = _cardAccessor.SelectCardByCardID(cardID);

                result = ConvertCardToCardVM(card);
                result.AlternateArts = _componentAccessor.SelectAlternateArtsByCardID(cardID);
                result.Moves = _componentAccessor.SelectMovesByCardID(cardID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get CardVM.", ex);
            }


            if (result == null)
            {
                throw new ApplicationException("Failed to get CardVM. CardVM was null.");
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public List<Card> GetAllCards()
        {
            List<Card> results = new List<Card>();

            try
            {
                results = _cardAccessor.SelectAllCards();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get a list of all cards.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public PaginatedResult<Card> GetCardsPaginated(FilterOption filterOption, int pageNumber = 1, int pageSize = 25)
        {
            PaginatedResult<Card> results = new PaginatedResult<Card>();

            try
            {
                results = _cardAccessor.SelectCardsPaginated(filterOption,pageNumber,pageSize);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get a paginated list of cards.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public int AddCard(Card card)
        {
            int newID = 0;

            if (card == null)
            {
                throw new ArgumentNullException("Failed to add a Card. Card was null.");
            }

            try
            {
                // Adds the base card to the database
                newID = _cardAccessor.InsertCard(card);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add a card.", ex);
            }

            return newID;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public bool EditCard(Card card)
        {
            bool isEdited = false;

            try
            {
                isEdited = (1 == _cardAccessor.UpdateCard(card));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to update card to the database.",ex);
            }

            return isEdited;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public bool DeleteCard(int cardID)
        {
            bool isDeleted = false;

            try
            {
                isDeleted = (1 == _cardAccessor.DeleteCard(cardID));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to delete a card.", ex);
            }

            return isDeleted;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public bool ActivateCard(int cardID, bool active)
        {
            bool isDeactivated = false;

            try
            {
                isDeactivated = (1 == _cardAccessor.ActivateCard(cardID,active));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to deactivate a card.", ex);
            }

            return isDeactivated;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public IEnumerable<Card> ApplyFilters(IEnumerable<Card> cards, FilterOption filterOption)
        {
            if (cards == null)
            {
                throw new ArgumentNullException("Failed to get card list. Cards was null.");
            }

            if (filterOption == null)
            {
                return cards.OrderBy(card => card.BoosterID).ThenBy(card => card.BoosterNumber);
            }

            IEnumerable<Card> results = cards;

            if (!string.IsNullOrWhiteSpace(filterOption.CardName))
            {
                results = results.Where(card => card.Name.Contains(filterOption.CardName, StringComparison.OrdinalIgnoreCase)).OrderBy(card => card.Name);
            }

            if (!string.IsNullOrWhiteSpace(filterOption.Rarity))
            {
                results = results.Where(card => string.Equals(card.Rarity, filterOption.Rarity, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filterOption.BoosterID))
            {
                results = results.Where(card => string.Equals(card.BoosterID, filterOption.BoosterID, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filterOption.CardType))
            {
                results = results.Where(card => string.Equals(card.CardType, filterOption.CardType, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filterOption.ElementTypeID))
            {
                results = results.Where(card => string.Equals(card.ElementTypeID, filterOption.ElementTypeID, StringComparison.OrdinalIgnoreCase));
            }

            if (filterOption.ArtistID != 0)
            {
                results = results.Where(card => int.Equals(card.ArtistID,filterOption.ArtistID));
            }

            results = results.OrderBy(card => card.BoosterID).ThenBy(card => card.BoosterNumber);

            return results;
        }

        /// <summary>
        /// Creates a card VM from the inputted Card.
        /// </summary>
        /// <param name="card">Card desired to be a CardVM</param>
        /// <returns>Returns a new CardVM with empty Move and Alt Art lists.</returns>
        private CardVM ConvertCardToCardVM(Card card)
        {
            CardVM result = null;
            result = new CardVM
            {
                CardID = card.CardID,
                ArtistID = card.ArtistID,
                AbilityID = card.AbilityID,
                BoosterID = card.BoosterID,
                PokemonRuleID = card.PokemonRuleID,
                ElementTypeID = card.ElementTypeID,
                Name = card.Name,
                BoosterNumber = card.BoosterNumber,
                CardType = card.CardType,
                Rarity = card.Rarity,
                WeaknessType = card.WeaknessType,
                ResistanceType = card.ResistanceType,
                WeaknessValue = card.WeaknessValue,
                ResistanceValue = card.ResistanceValue,
                RetreatCost = card.RetreatCost,
                Health = card.Health,
                Stage = card.Stage,
                ImagePath = card.ImagePath,
                Active = card.Active,
                Moves = new List<MoveVM>(),
                AlternateArts = new List<string>(),
            };
            return result;
        }    
    }
}
