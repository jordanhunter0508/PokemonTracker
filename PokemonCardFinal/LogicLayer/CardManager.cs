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
using Microsoft.VisualBasic;

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
        public List<Card> GetCardsByReleaseDate(DateTime releaseDate)
        {
            List<Card> results = new List<Card>();

            try
            {
                results = _cardAccessor.SelectCardsByReleaseDate(releaseDate);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get cards by release date.", ex);
            }

            return results;
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

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
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
                    CardVM cardVM = ConvertCardToCardVM(entry.Value);

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
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get a list of cards.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public List<CardVM> GetCardVMsByCardName(string name)
        {
            List<CardVM> results = new List<CardVM>();

            if (name == null) 
            {
                throw new ArgumentNullException("Failed to get list of cards by name. Name was null.");
            }

            try
            {
                Dictionary<int, Card> cards = GetCardsByCardName(name);
                Dictionary<int, List<MoveVM>> moves = GetCardMovesByCardName(name);
                Dictionary<int, List<string>> altArts = GetCardAlternateArtsByCardName(name);

                results = SaveCards(cards, moves, altArts);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get a list of cards by name.",ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
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

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public Dictionary<int, Card> GetCardsByCardName(string name)
        {
            Dictionary<int, Card> results = new Dictionary<int, Card>();

            if (name == null)
            {
                throw new ArgumentNullException("Failed to get cards by name. Name was null");
            }

            try
            {
                results = _cardAccessor.SelectCardsByCardName(name);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get cards by name.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
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

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public Dictionary<int, List<MoveVM>> GetCardMovesByCardName(string name)
        {
            Dictionary<int, List<MoveVM>> results = new Dictionary<int, List<MoveVM>>();


            if (name == null)
            {
                throw new ArgumentNullException("Failed to get moves by name. Name was null");
            }

            try
            {
                results = _cardAccessor.SelectCardMovesByCardName(name);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get moves by name.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
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

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public Dictionary<int, List<string>> GetCardAlternateArtsByCardName(string name)
        {
            Dictionary<int, List<string>> results = new Dictionary<int, List<string>>();

            if (name == null)
            {
                throw new ArgumentNullException("Failed to get alternate arts by name. Name was null");
            }

            try
            {
                results = _cardAccessor.SelectCardAlternateArtsByCardName(name);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get alternate arts by name.", ex);
            }

            return results;
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
        public IEnumerable<CardVM> GetCardVMsByCardName(IEnumerable<CardVM> cards, string name)
        {
            IEnumerable<CardVM> results = null;

            if (name == null)
            {
                throw new ArgumentNullException("Failed to get card list by name. Name was null.");
            }
            if (cards == null)
            {
                throw new ArgumentNullException("Failed to get card list by name. Cards was null.");
            }

            results = from card in cards
                      where card.Name.ToLower().Contains(name.ToLower())
                      orderby card.Name
                      select card;

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public IEnumerable<CardVM> GetCardVMsByRarity(IEnumerable<CardVM> cards, string rarity)
        {
            IEnumerable<CardVM> results = null;

            if (rarity == null)
            {
                throw new ArgumentNullException("Failed to get card list by rarity. Rarity was null.");
            }
            if (cards == null)
            {
                throw new ArgumentNullException("Failed to get card list by rarity. Cards was null.");
            }

            results = from card in cards
                      where card.Rarity.ToLower() == rarity.ToLower()
                      orderby card.BoosterID, card.BoosterNumber
                      select card;

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public IEnumerable<CardVM> GetCardVMsByBoosterID(IEnumerable<CardVM> cards, string boosterID)
        {
            IEnumerable<CardVM> results = null;

            if (boosterID == null)
            {
                throw new ArgumentNullException("Failed to get card list by booster id. BoosterID was null.");
            }
            if (cards == null)
            {
                throw new ArgumentNullException("Failed to get card list by booster id. Cards was null.");
            }

            results = from card in cards
                      where card.BoosterID.ToLower() == boosterID.ToLower()
                      orderby card.BoosterID, card.BoosterNumber
                      select card;

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public IEnumerable<CardVM> GetCardVMsByCardType(IEnumerable<CardVM> cards, string cardType)
        {
            IEnumerable<CardVM> results = null;

            if (cardType == null)
            {
                throw new ArgumentNullException("Failed to get card list by card type. CardType was null.");
            }
            if (cards == null)
            {
                throw new ArgumentNullException("Failed to get card list by card type. Cards was null.");
            }

            results = from card in cards
                      where card.CardType.ToLower() == cardType.ToLower()
                      orderby card.BoosterID, card.BoosterNumber
                      select card;

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public IEnumerable<CardVM> GetCardVMsByElementTypeID(IEnumerable<CardVM> cards, string elementTypeID)
        {
            IEnumerable<CardVM> results = null;

            if (elementTypeID == null)
            {
                throw new ArgumentNullException("Failed to get card list by element type id. ElementTypeID was null.");
            }
            if (cards == null)
            {
                throw new ArgumentNullException("Failed to get card list by element type id. Cards was null.");
            }

            results = from card in cards
                      where card.ElementTypeID.ToLower() == elementTypeID.ToLower()
                      orderby card.BoosterID, card.BoosterNumber
                      select card;

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public bool AddCard(Card card)
        {
            bool isAdded = false;

            try
            {
                ValidateCard(card);

                isAdded = (1 == _cardAccessor.InsertCard(card));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to add card to the database.");
            }

            return isAdded;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public bool EditCard(Card card)
        {
            bool isEdited = false;

            try
            {
                ValidateCard(card);

                isEdited = (1 == _cardAccessor.UpdateCard(card));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to update card to the database.");
            }

            return isEdited;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public bool AddCardMove(int cardID, int moveID)
        {
            bool isAdded = false;

            try
            {
                isAdded = (1 == _cardAccessor.InsertCardMove(cardID, moveID));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to add a card's move.", ex);
            }

            return isAdded;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public bool DeleteCardMove(int cardID, int moveID)
        {
            bool isDeleted = false;

            try
            {
                isDeleted = (1 == _cardAccessor.DeleteCardMove(cardID, moveID));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to delete a card's alternate art.", ex);
            }

            return isDeleted;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public bool AddCardAlternateArt(int cardID, string alternateArtID)
        {
            bool isAdded = false;

            if (alternateArtID == null || alternateArtID == "")
            {
                throw new ArgumentNullException("Failed to add a card's alternate art. AlternateArtID was null.");
            }

            try
            {
                isAdded = (1 == _cardAccessor.InsertCardAlternateArt(cardID, alternateArtID));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to add a card's alternate art.", ex);
            }

            return isAdded;
        }

        /// <summary>
        /// Implements from <see cref="ICardManager"/>
        /// </summary>
        public bool DeleteCardAlternateArt(int cardID, string alternateArtID)
        {
            bool isDeleted = false;

            if (alternateArtID == null || alternateArtID == "")
            {
                throw new ArgumentNullException("Failed to delete a card's alternate art. AlternateArtID was null.");
            }

            try
            {
                isDeleted = (1 == _cardAccessor.DeleteCardAlternateArt(cardID,alternateArtID));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to delete a card's alternate art.", ex);
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

        /// <summary>
        /// Combines the Dictionaries to a list of CardVMs
        /// </summary>
        /// <param name="cards">Dictionary where the key is the cardID and the value is the Card</param>
        /// <param name="moves">Dictionary where the key is the cardID and the value is the List of MoveVM for a card</param>
        /// <param name="altArts">Dictionary where the key is the cardID and the value is the List of alternate arts for a card</param>
        /// <returns>Returns a List of CardVMs from a combination of dictionaries</returns>
        private List<CardVM> SaveCards(Dictionary<int, Card> cards, Dictionary<int, List<MoveVM>> moves, Dictionary<int, List<string>> altArts) 
        {
            List<CardVM> results = new List<CardVM>();
            foreach (var entry in cards)
            {
                int cardID = entry.Key;
                CardVM cardVM = ConvertCardToCardVM(entry.Value);

                if (moves.ContainsKey(cardID))
                {
                    cardVM.Moves = moves[cardID];
                }
                if (altArts.ContainsKey(cardID))
                {
                    cardVM.AlternateArts = altArts[cardID];
                }

                results.Add(cardVM);
            }
            return results;
        }

        /// <summary>
        /// Throws an error if the card has any null string inside
        /// </summary>
        private void ValidateCard(Card card) 
        {
            if (card == null)
            {
                throw new ArgumentNullException("Failed to add card. Card was null.");
            }
            if (card.AbilityID == null || card.AbilityID.Replace(" ", "").Length == 0)
            {
                throw new ArgumentNullException("Failed to add card. Card's AbilityID was null.");
            }
            if (card.BoosterID == null || card.BoosterID.Replace(" ", "").Length == 0)
            {
                throw new ArgumentNullException("Failed to add card. Card's BoosterID was null.");
            }
            if (card.PokemonRuleID == null || card.PokemonRuleID.Replace(" ", "").Length == 0)
            {
                throw new ArgumentNullException("Failed to add card. Card's PokemonRuleID was null.");
            }
            if (card.ElementTypeID == null || card.ElementTypeID.Replace(" ", "").Length == 0)
            {
                throw new ArgumentNullException("Failed to add card. Card's ElementTypeID was null.");
            }
            if (card.Name == null || card.Name.Replace(" ", "").Length == 0)
            {
                throw new ArgumentNullException("Failed to add card. Card's Name was null.");
            }
            if (card.CardType == null || card.CardType.Replace(" ", "").Length == 0)
            {
                throw new ArgumentNullException("Failed to add card. Card's CardType was null.");
            }
            if (card.Rarity == null || card.Rarity.Replace(" ", "").Length == 0)
            {
                throw new ArgumentNullException("Failed to add card. Card's Rarity was null.");
            }
            if (card.WeaknessType == null || card.WeaknessType.Replace(" ", "").Length == 0)
            {
                throw new ArgumentNullException("Failed to add card. Card's WeaknessType was null.");
            }
            if (card.ResistanceType == null || card.ResistanceType.Replace(" ", "").Length == 0)
            {
                throw new ArgumentNullException("Failed to add card. Card's ResistanceType was null.");
            }
            if (card.Stage == null || card.Stage.Replace(" ", "").Length == 0)
            {
                throw new ArgumentNullException("Failed to add card. Card's Stage was null.");
            }
        }
    }
}
