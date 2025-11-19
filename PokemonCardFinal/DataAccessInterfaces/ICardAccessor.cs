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
        /// Request all dields from the PokemonCard table
        /// </summary>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        public Dictionary<int, Card> SelectCards();

        /// <summary>
        /// Request all fields from the Move and MoveCost tables
        /// </summary>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        public Dictionary<int, List<MoveVM>> SelectCardMoves();

        /// <summary>
        /// Request all Alternate arts from CardAlternateArt table
        /// </summary>
        /// <returns>Returns a Dictionary where the cardID is the key and the Card is the value</returns>
        public Dictionary<int, List<string>> SelectCardAlternateArts();
    }
}
