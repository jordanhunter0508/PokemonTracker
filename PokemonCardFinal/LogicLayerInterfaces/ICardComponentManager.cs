using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface ICardComponentManager
    {
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
        /// Passes parameters to <see href="InsertCardMove(int,int)"/> Then returns true
        /// if the record was updated successfully.
        /// </summary>
        /// <param name="cardID">Used to find the CardMove.</param>
        /// <param name="moveID">Used to find the CardMove.</param>
        /// <returns>Returns true if the CardMove was created successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the CardMove already exists.</exception>
        public bool AddCardMove(int cardID, int moveID);

        /// <summary>
        /// Passes parameters to <see href="DeleteCardMoves(int)"/><br/>
        /// Then returns true if the record was deleted successfully
        /// </summary>
        /// <param name="cardID">Used to find the CardMove.</param>
        /// <returns>Returns true if the CardMove was delted successfully</returns>
        /// <exception cref="ApplicationException">Throws if there was a problem connecting to the database.</exception>
        public bool DeleteCardMoves(int cardID);

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
        /// <returns>Returns true if the CardAlternateArt was deleted successfully</returns>
        /// <exception cref="ApplicationException">Throws if there was a problem connecting to the database.</exception>
        public bool DeleteCardAlternateArts(int cardID);
    }
}
