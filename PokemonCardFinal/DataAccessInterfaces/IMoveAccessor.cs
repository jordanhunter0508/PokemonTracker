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
        /// <returns>Returns a Move of the specified moveID.</returns>
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
        public List<MoveVM> SelectMoveVMsWithMoveCost();

        /// <summary>
        /// Requests all fields from the Move table where
        /// there is no entry in MoveCost with a matching moveID
        /// </summary>
        /// <returns>Returns a List of all Moves in the database that don't have an entry in MoveCost.</returns>
        public List<Move> SelectMovesWithoutMoveCost();

        /// <summary>
        /// Inserts the parameters into the stored procedure to try
        /// and create a new record for a Move.
        /// </summary>
        /// <param name="move">New Move object to insert.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int InsertMove(Move move);

        /// <summary>
        /// Inserts the parameters into the stored procedure to try
        /// and create a new record for a MoveCost.
        /// </summary>
        /// <param name="cost">New MoveCost object to insert.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int InsertMoveCost(MoveCost cost);
    }
}
