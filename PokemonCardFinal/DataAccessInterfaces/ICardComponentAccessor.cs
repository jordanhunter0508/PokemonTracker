using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface ICardComponentAccessor
    {
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
        /// <returns>Returns the number of rows affected.</returns>
        public int DeleteCardMoves(int cardID);

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
        /// <returns>Returns the number of rows affected.</returns>
        public int DeleteCardAlternateArts(int cardID);
    }
}
