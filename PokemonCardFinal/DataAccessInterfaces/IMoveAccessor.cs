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
        /// Requests from the data access layer a Move with the moveID
        /// of the parameter
        /// </summary>
        /// <param name="moveID">Move to be searched for</param>
        /// <returns>Returns a Move with the same moveID</returns>
        /// <exception cref="ApplicationException">Throws if the moveID is not found in the database</exception>
        public Move SelectMoveByMoveID(string moveID);
    }
}
