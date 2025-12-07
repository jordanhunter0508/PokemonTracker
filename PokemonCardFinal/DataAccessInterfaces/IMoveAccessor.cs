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
        public Move SelectMoveByMoveID(int moveID);

        /// <summary>
        /// Requests all fields from the MoveCost table to
        /// create a MoveCost List of a specific Move.
        /// </summary>
        /// <param name="moveID">Used to search the database for matching moveCosts</param>
        /// <returns>Returns a List of MoveCosts where moveID mathces in the database.</returns>
        public List<MoveCost> SelectMoveCostsByMoveID(int moveID);

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

        /// <summary>
        /// Updates the Move at moveID from the move.
        /// </summary>
        /// <param name="move">New Move object to update the old one.</param>
        /// <returns>Returns 1 if it was successful.</returns>
        public int UpdateMove(Move move);

        /// <summary>
        /// Deletes the row from the database where MoveID matches on in the table.<br/>
        /// Also deletes the rows from MoveCost, CardMove
        /// </summary>
        /// <param name="moveID">MoveID of the row to delete.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int DeleteMove(int moveID);

        /// <summary>
        /// Deletes the rows from the database where MoveID matches in the MoveCost table.
        /// </summary>
        /// <param name="moveID">MoveID of the rows to delete.</param>
        /// <returns>Returns 1 if it was successful.</returns>
        public int DeleteMoveCost(int moveID);
    }
}
