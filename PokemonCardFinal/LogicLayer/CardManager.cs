using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /// <summary>
        /// General CardManager created for the presentaion layer
        /// </summary>
        public CardManager()
        {
            _cardAccessor = new CardAccessor();
        }

        /// <summary>
        /// Used for testing to pass in fake data
        /// </summary>
        /// <param name="cardAccessor">Set the ICardAccessor in the CardManager</param>
        public CardManager(ICardAccessor cardAccessor)
        {
            _cardAccessor = cardAccessor;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public CardVM GetCardVMByCardID(int cardID)
        {
            CardVM result = null;

            try
            {
                Card card = _cardAccessor.SelectCardByCardID(cardID);

                result = ConvertCardToCardVM(card);
                result.Moves = GetMovesByCardID(cardID);
                result.AlternateArts = GetAlternateArtsByCardID(cardID);
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
        public Card GetCardByCardID(int cardID)
        {
            Card resultCard = null;

            try
            {
                resultCard = _cardAccessor.SelectCardByCardID(cardID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get card.", ex);
            }

            return resultCard;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public List<MoveVM> GetMovesByCardID(int cardID)
        {
            List<MoveVM> results = new List<MoveVM>();

            try
            {
                results = _cardAccessor.SelectMovesByCardID(cardID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get moves from a cardID.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public List<string> GetAlternateArtsByCardID(int cardID)
        {
            List<string> results = new List<string>();

            try
            {
                results = _cardAccessor.SelectAlternateArtsByCardID(cardID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get alternate arts from a cardID.", ex);
            }

            return results;
        }

        public List<CardVM> GetCardVMs()
        {
            List<CardVM> results = new List<CardVM>();

            try
            {            
                Dictionary<int, Card> cards = GetCards();
                Dictionary<int, List<MoveVM>> moves = GetCardMoves();
                Dictionary<int, List<string>> altArts = GetCardAlternateArts();

                foreach (var entry in cards)
                {
                    int cardID = entry.Key;
                    Card card = entry.Value;

                    CardVM cardVM = ConvertCardToCardVM(card);

                    if (altArts.ContainsKey(cardID))
                    {
                        cardVM.AlternateArts = altArts[cardID];
                    }
                    if (moves.ContainsKey(cardID))
                    {
                        cardVM.Moves = moves[cardID];
                    }

                    results.Add(cardVM);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return results;
        }

        public Dictionary<int, Card> GetCards()
        {
            Dictionary<int, Card> results = new Dictionary<int, Card>();

            try
            {
                results = _cardAccessor.SelectCards();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get cards.", ex);
            }
            return results;
        }

        public Dictionary<int, List<MoveVM>> GetCardMoves()
        {
            Dictionary<int, List<MoveVM>> results = new Dictionary<int, List<MoveVM>>();

            try
            {
                results = _cardAccessor.SelectCardMoves();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get a list of moves for cards.", ex);
            }
            return results;
        }

        public Dictionary<int, List<string>> GetCardAlternateArts()
        {
            Dictionary<int, List<string>> results = new Dictionary<int, List<string>>();

            try
            {
                results = _cardAccessor.SelectCardAlternateArts();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get a list of alternate arts for cards.", ex);
            }
            return results;
        }

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
