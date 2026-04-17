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
        /// Requests all fields from the Booster table where Active is true to create 
        /// a list of Boosters.
        /// </summary>
        /// <returns>Returns a list of all active boosters in the database.</returns>
        public List<Booster> SelectActiveBoosters();

        /// <summary>
        /// Requests all boosterIDs from the database
        /// </summary>
        /// <returns>Returns a list of all BoosterIDs</returns>
        public List<string> SelectBoosterIDs();

        /// <summary>
        /// Requests all active boosterIDs from the database
        /// </summary>
        /// <returns>Returns a list of all active BoosterIDs</returns>
        public List<string> SelectActiveBoosterIDs();

        /// <summary>
        /// Inserts the parameters into the stored procedure to try
        /// and create a new record for a Booster.
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
        /// Deletes the row from the database where boosterID matches in the table.
        /// </summary>
        /// <param name="boosterID">BoosterID of the row to delete.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int DeleteBooster(string boosterID);







        // ======================================== Series Related ======================================================
        // Could get it's own accessor if more is needed

        /// <summary>
        /// Requests all active image paths from the series table
        /// </summary>
        /// <returns>Returns a list of all active Series with only the imagePath and ID</returns>
        public List<Series> SelectSeriesImagePaths();
    }
}
