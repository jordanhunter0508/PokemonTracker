using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface IBoosterAccessor
    {
        /// <summary>
        /// Requests all fields from the Booster table to create an Booster.
        /// </summary>
        /// <param name="boosterID">Used to search the database for a booster</param>
        /// <returns>Returns a Booster of the specified boosterID.</returns>
        public Booster SelectBoosterByBoosterID(string boosterID);

        /// <summary>
        /// Requests all data from the Booster table to
        /// create an Booster List.
        /// </summary>
        /// <returns>Returns a List of all boosters in the database.</returns>
        public List<Booster> SelectBoosters();

        /// <summary>
        /// Inserts the parameters into the stored procedure to try
        /// and create a new record for an Booster.
        /// </summary>
        /// <param name="booster">New booster object to insert.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int InsertBooster(Booster booster);

        /// <summary>
        /// Updates the fields in the Booster table at the boosterID.
        /// </summary>
        /// <param name="booster">New Booster object to update the old field at boosterID.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int UpdateBooster(Booster booster);

        /// <summary>
        /// Deletes the row from the database where boosterID matches on in the table.
        /// </summary>
        /// <param name="boosterID">BoosterID of the row to delete.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int DeleteBooster(string boosterID);
    }
}
