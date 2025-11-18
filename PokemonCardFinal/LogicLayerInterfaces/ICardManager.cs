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
        /// <returns>Returns a List of moves in the database related to the cardID</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<MoveVM> GetMovesByCardID(int cardID);

        /// <summary>
        /// Calls the <see href="SelectAlternateArtsByCardID(int)"/> method to get<br/>
        /// a list of related AlternateArtIDs from the database.
        /// </summary>
        /// <returns>Returns a list of AlternateArtIDs realted to cardID in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<string> GetAlternateArtsByCardID(int cardID);
    }
}
