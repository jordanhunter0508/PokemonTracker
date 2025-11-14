using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface IMoveAccessor
    {
        /// <summary>
        /// Requests all fields from the Move table to create a Move.
        /// </summary>
        /// <param name="moveID">Used to search the database for a move</param>
        /// <returns>Returns an Move of the specified moveID.</returns>
        public Move SelectMoveByMoveID(string moveID);

        /// <summary>
        /// Requests all fields from the MoveCost table to
        /// create a MoveCost List of a specific Move.
        /// </summary>
        /// <param name="moveID">Used to search the database for matching moveCosts</param>
        /// <returns>Returns a List of MoveCosts where moveID mathces in the database.</returns>
        public List<MoveCost> SelectMoveCostsByMoveID(string moveID);

        /// <summary>
        /// Requests all fields from the Move and MoveCost tables to
        /// create a List of MoveVMs.
        /// </summary>
        /// <returns>Returns a List of all MoveVMs in the database.</returns>
        public List<MoveVM> SelectMoveVMs();

        public List<Move> SelectMoves();
    }
}
