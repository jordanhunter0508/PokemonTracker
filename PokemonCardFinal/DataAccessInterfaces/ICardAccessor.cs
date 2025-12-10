using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface ICardAccessor
    {
        /// <summary>
        /// Requests all fields from the Card table to create a Card.
        /// </summary>
        /// <param name="cardID">Used to search the database for a card</param>
        /// <returns>Returns a Card of the specified cardID.</returns>
        public Card SelectCardByCardID(int cardID);

        /// <summary>
        /// Requests all fields from the Card table to create a Card List. <br/>
        /// Where the releaseDate matches one in the Booster table.
        /// </summary>
        /// <param name="releaseDate">Date of the newest set</param>
        /// <returns>Returns a Card List of the newest set</returns>
        public List<Card> SelectCardsByReleaseDate(DateTime releaseDate);

        /// <summary>
        /// Requests all fields from the Move and MoveCost tables where <br/>
        /// the cardID matches from the join table.
        /// </summary>
        /// <param name="cardID">Used to search the database for moves that match</param>
        /// <returns>Returns a List of all Moves in the database that match with a specified cardID.</returns>
        public List<MoveVM> SelectMovesByCardID(int cardID);

        /// <summary>
        /// Requests AlternateArtIDs from the CardAlternateArt table <br/>
        /// where cardID matches.
        /// </summary>
        /// <param name="cardID">Used to search the database for alternate art ids that match</param>
        /// <returns>Returns a List of all AlternateArtIDs that are related to a specified cardID</returns>
        public List<string> SelectAlternateArtsByCardID(int cardID);

        /// <summary>
        /// Request all fields from the PokemonCard table
        /// </summary>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        public Dictionary<int, Card> SelectCards();

        /// <summary>
        /// Request all fields from the PokemonCard table
        /// </summary>
        /// <param name="name">Gets the rows with the matching name in the card table</param>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        public Dictionary<int, Card> SelectCardsByCardName(string name);

        /// <summary>
        /// Request all fields from the Move and MoveCost tables
        /// </summary>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        public Dictionary<int, List<MoveVM>> SelectCardMoves();

        /// <summary>
        /// Request all fields from the Move and MoveCost tables
        /// </summary>
        /// <param name="name">Gets the rows with the matching name in the card table</param>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        public Dictionary<int, List<MoveVM>> SelectCardMovesByCardName(string name);

        /// <summary>
        /// Request all Alternate arts from CardAlternateArt table
        /// </summary>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        public Dictionary<int, List<string>> SelectCardAlternateArts();

        /// <summary>
        /// Request all Alternate arts from CardAlternateArt table
        /// </summary>
        /// <param name="name">Gets the rows with the matching name in the card table</param>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        public Dictionary<int, List<string>> SelectCardAlternateArtsByCardName(string name);

        /// <summary>
        /// Inserts the parameters into the stored procedure to try
        /// and create a new record for a Card.
        /// </summary>
        /// <param name="card">New Card object to insert.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int InsertCard(Card card);

        /// <summary>
        /// Updates the fields in the Card table at the cardID.
        /// </summary>
        /// <param name="card">New Card object to update the old field at cardID.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int UpdateCard(Card card);

        /// <summary>
        /// Deletes the row from the database where cardID matches in the table.
        /// </summary>
        /// <param name="cardID">CardID of the row to delete.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int DeleteCard(int cardID);

        /// <summary>
        /// Inserts the parameters into the stored procedure to try
        /// and create a new record for a CardMove.
        /// </summary>
        /// <param name="cardID">CardID of the row to insert.</param>
        /// <param name="moveID">MoveID of the row to insert.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int InsertCardMove(int cardID, int moveID);

        /// <summary>
        /// Deletes the row from the database where cardID <br/>
        /// and the moveID matches in the CardMove table.
        /// </summary>
        /// <param name="cardID">CardID of the row to delete.</param>
        /// <param name="moveID">MoveID of the row to delete.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int DeleteCardMove(int cardID, int moveID);

        /// <summary>
        /// Inserts the parameters into the stored procedure to try
        /// and create a new record for a CardAlternateArt.
        /// </summary>
        /// <param name="cardID">CardID of the row to insert.</param>
        /// <param name="alternateArtID">MoveID of the row to insert.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int InsertCardAlternateArt(int cardID, string alternateArtID);

        /// <summary>
        /// Deletes the row from the database where cardID <br/> 
        /// and the alternateArtID matches in the CardAlternateArt table.
        /// </summary>
        /// <param name="cardID">CardID of the row to delete.</param>
        /// <param name="alternateArtID">AlternateArtID of the row to delete.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int DeleteCardAlternateArt(int cardID, string alternateArtID);
    }
}
