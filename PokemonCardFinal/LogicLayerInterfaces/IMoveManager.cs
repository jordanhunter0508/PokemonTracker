using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface IMoveManager
    {
        /// <summary>
        /// Uses GetMoveByMoveID and GetMoveCostsByMoveID to create a
        /// MoveVM.
        /// </summary>
        /// <param name="moveID">Used to search the database for the Move and MoveCost</param>
        /// <returns>Returns a MoveVM from the database where the moveID match in MoveCost, and Move.</returns>
        /// <exception cref="ApplicationException">Throws if there was an error retrieving the data</exception>
        public MoveVM GetMoveVMByMoveID(string moveID);

        /// <summary>
        /// Passes parameters to <see href="SelectMoveByMoveID(string)"/><br/>
        /// then returns the results of the query. 
        /// </summary>
        /// <param name="moveID">Used to search the database for the move</param>
        /// <returns>Returns a Move from the database where the moveIDs match</returns>
        /// <exception cref="ApplicationException">Throws if there was an error retrieving the data</exception>
        public Move GetMoveByMoveID(string moveID);

        /// <summary>
        /// Passes parameters to <see href="SelectMoveCostsByMoveID(string)"/><br/>
        /// then returns the results of the query. 
        /// </summary>
        /// <param name="moveID">Used to search the database for the MoveCost</param>
        /// <returns>Returns a MoveCost from the database where the moveIDs match</returns>
        /// <exception cref="ApplicationException">Throws if there was an error retrieving the data</exception>
        public List<MoveCost> GetMoveCostsByMoveID(string moveID);

        /// <summary>
        /// Uses GetMoveVMsWithMoveCost and GetMovesWithoutMoveCost to create
        /// a new list of MoveVMs that has all Moves from the database.
        /// </summary>
        /// <returns>Returns a List of all Moves and MoveVMs in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<MoveVM> GetMoveVMs();

        /// <summary>
        /// Calls the <see href="SelectMoveVMsWithMoveCost()"/> method to get<br/>
        /// a list of all MoveVMs from the database.
        /// </summary>
        /// <returns>Returns a List of all MoveVMs in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<MoveVM> GetMoveVMsWithMoveCost();

        /// <summary>
        /// Calls the <see href="SelectMovesWithoutMoveCost()"/> method to get<br/>
        /// a list of all Moves from the database.
        /// </summary>
        /// <returns>Returns a List of all Moves in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<Move> GetMovesWithoutMoveCost();

        /// <summary>
        /// Uses AddMove and AddMoveCost to insert a new MoveVM inside the database
        /// </summary>
        /// <param name="moveVM">New MoveVM object to be added to the database.</param>
        /// <returns>Returns true if the Move and MoveCosts were created successfully.</returns>
        /// <exception cref="ApplicationException">Throws if there was an error using AddMove or AddMoveCost.</exception>
        public bool AddMoveVM(MoveVM moveVM);

        /// <summary>
        /// Passes parameters to <see href="InsertMove()"/> Then returns true
        /// if the record was updated successfully.
        /// </summary>
        /// <param name="move">New Move object to be added to the database.</param>
        /// <returns>Returns true if the Move was created successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the moveID is already used</exception>
        public bool AddMove(Move move);

        /// <summary>
        /// Passes parameters to <see href="InsertMoveCost()"/> Then returns true
        /// if the record was updated successfully.
        /// </summary>
        /// <param name="cost">New MoveCost object to be added to the database.</param>
        /// <returns>Returns true if the MoveCost was created successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the moveID and elementyType is already used, 
        /// MoveID or ELementType couldn't be found.</exception>
        public bool AddMoveCost(MoveCost cost);
    }
}
