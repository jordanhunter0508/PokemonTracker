using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface IBoosterManager
    {
        /// <summary>
        /// Passes parameters to <see href="IBoosterAccessor.SelectBoosterByBoosterID(string)"/><br/>
        /// then returns the results of the query. 
        /// </summary>
        /// <param name="boosterID">Used to search the database for the booster</param>
        /// <returns>Returns a Booster from the database where the boosterIDs match</returns>
        /// <exception cref="ApplicationException">Throws if the boosterID could not be found</exception>
        public Booster GetBoosterByBoosterID(string boosterID);

        /// <summary>
        /// Calls the <see href="IBoosterAccessor.SelectBooster()"/> method to get<br/>
        /// a list of all Boosters from the database.
        /// </summary>
        /// <returns>Returns a List of all Boosters in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<Booster> GetBoosters();

        public List<Booster> SelectActiveBoosters();

        /// <summary>
        /// Calls the <see href="IBoosterAccessor.SelectBoosterIDs()"/> method to get<br/>
        /// a list of all active boosterIDs
        /// </summary>
        /// <returns>Returns a List of all active BoosterIDs in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<string> GetBoosterIDs();

        /// <summary>
        /// Passes parameters to <see href="IBoosterAccessor.InsertBooster()"/> Then returns true
        /// if the record was updated successfully.
        /// </summary>
        /// <param name="booster">New Booster object to be added to the database.</param>
        /// <returns>Returns true if the Booster was created successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the abbreviation is already used 
        /// or if the boosterId is already used.</exception>
        public bool AddBooster(Booster booster);

        /// <summary>
        /// Passes parameters to <see href="IBoosterAccessor.UpdateBooster(Booster)"/><br/>
        /// Then returns true if the record was updated successfully.
        /// </summary>
        /// <param name="booster">New Booster object to update the old field at boosterID</param>
        /// <returns>Returns true if the Booster was updated successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the abbreviation is already used.</exception>
        public bool EditBooster(Booster booster);

        /// <summary>
        /// Passes parameters to <see href="IBoosterAccessor.DeleteBooster(string)"/><br/>
        /// Then returns true if the record was deleted successfully
        /// </summary>
        /// <param name="boosterID">Used to find the Booster</param>
        /// <returns>Returns true if the Booster was deleted successfully</returns>
        /// <exception cref="ApplicationException">Throws if the booster is attached to a card</exception>
        public bool DeleteBooster(string boosterID);




        // ======================================== Series Related ======================================================
        // Could get it's own manager if more is needed

        /// <summary>
        /// Calls <see href="IBoosterAccessor.SelectSeriesImagePaths()"/> <br/>
        /// To get all active list of Series with only the id and imagePath
        /// </summary>
        /// <returns>Returns a list of all active Series with only the imagePath and ID</returns>
        /// <exception cref="ApplicationException">Throws if there is an error connecting to the database/exception>
        public List<Series> GetSeriesImagePaths();
    }
}
