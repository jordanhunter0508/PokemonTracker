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
        /// Calls the <see href="SelectMoveVMs()"/> method to get<br/>
        /// a list of all MoveVMs from the database.
        /// </summary>
        /// <returns>Returns a List of all MoveVMs in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<MoveVM> GetMoveVMs();

        public List<Move> GetMoves();
    }
}
