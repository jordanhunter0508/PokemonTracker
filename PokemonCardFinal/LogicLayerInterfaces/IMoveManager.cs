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
        public MoveVM GetMoveVMByMoveID(string moveID);

        /// <summary>
        /// Request from the database all fields in the Move table<br/>
        /// Where moveId input matches another moveID
        /// </summary>
        /// <param name="moveID">Name of the move being searched for</param>
        /// <returns>Returns a Move object created from the database</returns>
        /// <exception cref="ApplicationException">Throws if the moveID is not found in the database</exception>
        public Move GetMovseByMoveID(string moveID);
    }
}
