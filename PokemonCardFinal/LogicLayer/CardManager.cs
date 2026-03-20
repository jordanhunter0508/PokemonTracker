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

                throw new ApplicationException("Failed to get a search for a list of cards by card name.", ex);
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
                Moves = new List<MoveVM>(),
                AlternateArts = new List<string>(),
            };
            return result;
        }
    }
}
