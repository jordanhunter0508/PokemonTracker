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
        /// Uses GetCards(), GetCardMoves(), and GetCardAlternateArts() <br/>
        /// to create a list of CardVMs
        /// </summary>
        /// <returns>Returns a list of cardVMs</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<CardVM> GetCardVMs();

        /// <summary>
        /// Calls the <see href="SelectCards()"/> method to get<br/>
        /// a list of all Cards from the database.
        /// </summary>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public Dictionary<int,Card> GetCards();

        /// <summary>
        /// Calls the <see href="SelectCardMoves()"/> method to get<br/>
        /// a list of all Moves related to cards from the database.
        /// </summary>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public Dictionary<int,List<MoveVM>> GetCardMoves();

        /// <summary>
        /// Calls the <see href="SelectCardAlternateArts()"/> method to get<br/>
        /// a list of all Alternate Arts related to cards from the database.
        /// </summary>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public Dictionary<int,List<string>> GetCardAlternateArts();

        /// <summary>
        /// Passes parameters to <see href="DeleteCard(int)"/><br/>
        /// Then returns true if the record was deleted successfully
        /// </summary>
        /// <param name="cardID">Used to find the Card</param>
        /// <returns>Returns true if the Card, and it's components was deleted successfully</returns>
        /// <exception cref="ApplicationException">Throws if there was a problem connecting to the database.</exception>
        public bool DeleteCard(int cardID);
    }
}