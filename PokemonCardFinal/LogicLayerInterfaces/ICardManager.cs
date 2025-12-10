using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface ICardManager
    {
        /// <summary>
        /// Uses GetCardByCardID, GetMovesByCardID, GetAlternateArtsByCardID <br/>
        /// to create a CardVM.
        /// </summary>
        /// <param name="cardID">Used to search the database for the Card, Move and AlternateArts</param>
        /// <returns>Returns a MoveVM from the database where the cardID matchs in the <br/>
        /// PokemonCard, CardMove, and CardAlteranteArt tables.</returns>
        /// <exception cref="ApplicationException">Throws if there was an error retrieving the data</exception>
        public CardVM GetCardVMByCardID(int cardID);

        /// <summary>
        /// Passes parameters to <see href="SelectCardByCardID(int)"/><br/>
        /// then returns the results of the query. 
        /// </summary>
        /// <param name="cardID">Used to search the database for the card</param>
        /// <returns>Returns a Card from the database where the cardIDs match</returns>
        /// <exception cref="ApplicationException">Throws if there was an error retrieving the data</exception>
        public Card GetCardByCardID(int cardID);

        /// <summary>
        /// Passes parameters to <see href="SelectCardsByReleaseDate(DateTime)"/><br/>
        /// then returns the results of the query. 
        /// </summary>
        /// <param name="releaseDate">Used to search the database for the cards with this date</param>
        /// <returns>Returns a Card List where the releaseDate matches in the booster table.</returns>
        /// <exception cref="ApplicationException">Throws if there was an error retrieving the data</exception>
        public List<Card> GetCardsByReleaseDate(DateTime releaseDate);

        /// <summary>
        /// Calls the <see href="SelectMovesByCardID(int)"/> method to get<br/>
        /// a list of moves from the database where the cardIDs match.
        /// </summary>
        /// <param name="cardID">Used to search the database for the card</param>
        /// <returns>Returns a List of moves in the database related to the cardID</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<MoveVM> GetMovesByCardID(int cardID);

        /// <summary>
        /// Calls the <see href="SelectAlternateArtsByCardID(int)"/> method to get<br/>
        /// a list of related AlternateArtIDs from the database.
        /// </summary>
        /// <param name="cardID">Used to search the database for the card</param>
        /// <returns>Returns a list of AlternateArtIDs realted to cardID in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<string> GetAlternateArtsByCardID(int cardID);

        /// <summary>
        /// Uses methods that returns dictionaries to join them all together
        /// to make a List of CardVMs.
        /// </summary>
        /// <returns>Returns a list of cardVMs</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<CardVM> GetCardVMs();

        /// <summary>
        /// Uses methods that returns dictionaries to join them all together
        /// to make a List of CardVMs.
        /// </summary>
        /// <param name="name">Gets the cards with the matching name</param>
        /// <returns>Returns a list of cardVMs</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<CardVM> GetCardVMsByCardName(string name);

        /// <summary>
        /// Calls the <see href="SelectCards()"/> method to get<br/>
        /// a list of all Cards from the database.
        /// </summary>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public Dictionary<int,Card> GetCards();

        /// <summary>
        /// Calls the <see href="SelectCardsByCardName(string)"/> method to get<br/>
        /// a list of all Cards from the database.
        /// </summary>
        /// <param name="name">Gets the cards with the matching name</param>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public Dictionary<int, Card> GetCardsByCardName(string name);

        /// <summary>
        /// Calls the <see href="SelectCardMoves()"/> method to get<br/>
        /// a list of all Moves related to cards from the database.
        /// </summary>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public Dictionary<int,List<MoveVM>> GetCardMoves();

        /// <summary>
        /// Calls the <see href="SelectCardMovesByCardName(string)"/> method to get<br/>
        /// a list of all Moves related to cards from the database.
        /// </summary>
        /// <param name="name">Gets the cards with the matching name</param>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public Dictionary<int, List<MoveVM>> GetCardMovesByCardName(string name);

        /// <summary>
        /// Calls the <see href="SelectCardAlternateArts()"/> method to get<br/>
        /// a list of all Alternate Arts related to cards from the database.
        /// </summary>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public Dictionary<int,List<string>> GetCardAlternateArts();

        /// <summary>
        /// Calls the <see href="SelectCardAlternateArtsByCardName(string)"/> method to get<br/>
        /// a list of all Alternate Arts related to cards from the database.
        /// </summary>
        /// <param name="name">Gets the cards with the matching name</param>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public Dictionary<int, List<string>> GetCardAlternateArtsByCardName(string name);

        /// <summary>
        /// Uses other Add and Delete methods to insert the new CardVM
        /// </summary>
        /// <param name="cardVM">New CardVM object to be added to the database.</param>
        /// <returns>Returns true if the CardVM was created successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the boosterID boosterNumber and Rarity are already used.</exception>
        public bool AddCardVM(CardVM cardVM);

        /// <summary>
        /// Passes parameters to <see href="InsertCard(Card)"/> Then returns true
        /// if the record was updated successfully.
        /// </summary>
        /// <param name="card">New Card object to be added to the database.</param>
        /// <returns>Returns true if the Card was created successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the boosterID boosterNumber and Rarity are already used.</exception>
        public int AddCard(Card card);

        /// <summary>
        /// Passes parameters to <see href="UpdateCard(Card)"/><br/>
        /// Then returns true if the record was updated successfully.
        /// </summary>
        /// <param name="card">New Card object to update the old field at cardID</param>
        /// <returns>Returns true if the Card was updated successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the boosterId, boosterNumber and rarity are already used.</exception>
        public bool EditCard(Card card);

        /// <summary>
        /// Passes parameters to <see href="DeleteCard(int)"/><br/>
        /// Then returns true if the record was deleted successfully
        /// </summary>
        /// <param name="cardID">Used to find the Card</param>
        /// <returns>Returns true if the Card, and it's components was deleted successfully</returns>
        /// <exception cref="ApplicationException">Throws if there was a problem connecting to the database.</exception>
        public bool DeleteCard(int cardID);

        /// <summary>
        /// Passes parameters to <see href="InsertCardMove(int,int)"/> Then returns true
        /// if the record was updated successfully.
        /// </summary>
        /// <param name="cardID">Used to find the CardMove.</param>
        /// <param name="moveID">Used to find the CardMove.</param>
        /// <returns>Returns true if the CardMove was created successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the CardMove already exists.</exception>
        public bool AddCardMove(int cardID, int moveID);

        /// <summary>
        /// Passes parameters to <see href="DeleteCardMove(int,int)"/><br/>
        /// Then returns true if the record was deleted successfully
        /// </summary>
        /// <param name="cardID">Used to find the CardMove.</param>
        /// <param name="moveID">Used to find the CardMove.</param>
        /// <returns>Returns true if the CardMove was delted successfully</returns>
        /// <exception cref="ApplicationException">Throws if there was a problem connecting to the database.</exception>
        public bool DeleteCardMove(int cardID, int moveID);

        /// <summary>
        /// Passes parameters to <see href="InsertCardAlternateArt(int,string)"/> Then returns true
        /// if the record was updated successfully.
        /// </summary>
        /// <param name="cardID">Used to find the CardMove.</param>
        /// <param name="alternateArtID">Used to find the CardMove.</param>
        /// <returns>Returns true if the CardAlternateArt was created successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the CardAlternateArt already exists.</exception>
        public bool AddCardAlternateArt(int cardID, string alternateArtID);

        /// <summary>
        /// Passes parameters to <see href="DeleteCardAlternateArt(int,string)"/><br/>
        /// Then returns true if the record was deleted successfully
        /// </summary>
        /// <param name="cardID">Used to find the CardAlternateArt</param>
        /// <param name="alternateArtID">Used to find the CardAlternateArt</param>
        /// <returns>Returns true if the CardAlternateArt was deleted successfully</returns>
        /// <exception cref="ApplicationException">Throws if there was a problem connecting to the database.</exception>
        public bool DeleteCardAlternateArt(int cardID, string alternateArtID);

        /// <summary>
        /// Filters a IEnumberable<CardVM> with a specified name using linq.<br/>
        /// Used when a list of Cards is already created.
        /// </summary>
        /// <param name="cards">IEnumerable of CardVM to filter</param>
        /// <param name="name">Gets the cards with the matching name</param>
        /// <returns>Returns an IOrderedEnumerable of cards with the specified card name</returns>
        public IEnumerable<CardVM> GetCardVMsByCardName(IEnumerable<CardVM> cards, string name);

        /// <summary>
        /// Filters a IEnumberable<CardVM> with a specified rarity using linq.<br/>
        /// Used when a list of Cards is already created.
        /// </summary>
        /// <param name="cards">IEnumerable of CardVM to filter</param>
        /// <param name="rarity">Gets the cards with the matching rarity</param>
        /// <returns>Returns an IOrderedEnumerable of cards with the specified card rarity</returns>
        public IEnumerable<CardVM> GetCardVMsByRarity(IEnumerable<CardVM> cards, string rarity);

        /// <summary>
        /// Filters a IEnumberable<CardVM> with a specified boosterID using linq.<br/>
        /// Used when a list of Cards is already created.
        /// </summary>
        /// <param name="cards">IEnumerable of CardVM to filter</param>
        /// <param name="boosterID">Gets the cards with the matching boosterID</param>
        /// <returns>Returns an IOrderedEnumerable of cards with the specified card boosterID</returns>
        public IEnumerable<CardVM> GetCardVMsByBoosterID(IEnumerable<CardVM> cards, string boosterID);

        /// <summary>
        /// Filters a IEnumberable<CardVM> with a specified cardType using linq.<br/>
        /// Used when a list of Cards is already created.
        /// </summary>
        /// <param name="cards">IEnumerable of CardVM to filter</param>
        /// <param name="cardType">Gets the cards with the matching cardType</param>
        /// <returns>Returns an IOrderedEnumerable of cards with the specified cardType</returns>
        public IEnumerable<CardVM> GetCardVMsByCardType(IEnumerable<CardVM> cards, string cardType);

        /// <summary>
        /// Filters a IEnumberable<CardVM> with a specified elementTypeID using linq.<br/>
        /// Used when a list of Cards is already created.
        /// </summary>
        /// <param name="cards">IEnumerable of CardVM to filter</param>
        /// <param name="elementTypeID">Gets the cards with the matching elementTypeID</param>
        /// <returns>Returns an IOrderedEnumerable of cards with the specified card elementTypeID</returns>
        public IEnumerable<CardVM> GetCardVMsByElementTypeID(IEnumerable<CardVM> cards, string elementTypeID);
    }
}