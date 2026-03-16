using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface IAltArtManager
    {
        /// <summary>
        /// Passes parameters to <see href="SelectAlternateArtByID(string)"/><br/>
        /// then returns the results of the query. 
        /// </summary>
        /// <param name="abilityID">Used to search the database for the alternate art.</param>
        /// <returns>Returns an AlternateArt from the database where the alternateArtID match.</returns>
        /// <exception cref="ApplicationException">Throws if the alternateArtID could not be found.</exception>
        public AlternateArt GetAlternateArtByID(string alternateArtID);

        /// <summary>
        /// Calls the <see href="IAltArtAccessor.SelectAllAlternateArt()"/> method to get<br/>
        /// a list of all AlternateArt from the database.
        /// </summary>
        /// <returns>Returns a list of all Alternate Arts.</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data.</exception>
        public List<AlternateArt> GetAllAlternateArt();

        /// <summary>
        /// Calls the <see href="IAltArtAccessor.SelectActiveAlternateArts(int,int)"/> method to get<br/>
        /// a list of active AlternateArt from the database.
        /// </summary>
        /// <param name="pageNumber">Represents what page to pull from.</param>
        /// <param name="pageSize">Represents how many items are on the page.</param>
        /// <returns>Returns paginated results where the Items is a list of AlternateArts.</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data.</exception>
        public PaginatedResult<AlternateArt> GetActiveAlternateArts(int pageNumber = 1, int pageSize = 20);

        /// <summary>
        /// Calls the <see href="IAltArtAccessor.SelectDeactiveAlternateArts(int,int)"/> method to get<br/>
        /// a list of deactive AlternateArt from the database.
        /// </summary>
        /// <param name="pageNumber">Represents what page to pull from.</param>
        /// <param name="pageSize">Represents how many items are on the page.</param>
        /// <returns>Returns paginated results where the Items is a list of AlternateArts.</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data.</exception>
        public PaginatedResult<AlternateArt> GetDeactiveAlternateArts(int pageNumber = 1, int pageSize = 20);

        /// <summary>
        /// Passes parameters to <see href="IAltArtAccessor.InsertAlternateArt(AlternateArt)"/><br/>
        /// Then returns true if the record was created successfully
        /// </summary>
        /// <param name="alternateArt">New AlternateArt object to be added to the database.</param>
        /// <returns>Returns true if the AlternateArt was created successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the AlternateArtID is already used.</exception>
        public bool AddAlternateArt(AlternateArt alternateArt);

        /// <summary>
        /// Passes parameters to <see href="IAltArtAccessor.UpdateAlternateArt(AlternateArt)"/><br/>
        /// Then returns true if the record was updated successfully.
        /// </summary>
        /// <param name="alternateArt">New AlternateArt object to update the old field at alternateArtID.</param>
        /// <returns>Returns true if the AlternateArt was updated successfully.</returns>
        /// <exception cref="ApplicationException">Throws if there is an error storing the data.</exception>
        public bool EditAlternateArt(AlternateArt alternateArt);

        /// <summary>
        /// Passes parameters to <see href="IAltArtAccessor.DeleteAlternateArt(string)"/><br/>
        /// Then returns true if the record was deleted successfully
        /// </summary>
        /// <param name="alternateArtID">Used to find the AlternateArt.</param>
        /// <returns>Returns true if the AlternateArt was deleted successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the AlternateArt is attached to a card.</exception>
        public bool DeleteAlternateArt(string alternateArtID);

        /// <summary>
        /// Passes parameters to <see href="IAltArtAccessor.DeactivateAbility(string)"/><br/>
        /// Then returns true if the record was deactivated successfully
        /// </summary>
        /// <param name="alternateArtID">AlternateArtID of the row to deactivate.</param>
        /// <returns>Returns the number of rows affected.</returns>
        /// <exception cref="ApplicationException">Throws if there is an error connection to the database</exception>
        public bool DeactivateAlternateArt(string alternateArtID);

        /// <summary>
        /// Passes parameters to <see href="IAltArtAccessor.DeactivateAbility(string)"/><br/>
        /// Then returns true if the record was deactivated successfully
        /// </summary>
        /// <param name="alternateArtID">AlternateArtID of the row to reactivate.</param>
        /// <returns>Returns the number of rows affected.</returns>
        /// <exception cref="ApplicationException">Throws if there is an error connection to the database</exception>
        public bool ReactivateAlternateArt(string alternateArtID);
    }
}