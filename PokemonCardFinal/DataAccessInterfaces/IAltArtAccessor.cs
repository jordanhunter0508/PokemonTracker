using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface IAltArtAccessor
    {
        /// <summary>
        /// Requests all fields from the AlternateArt table to create an AlternateArt.
        /// </summary>
        /// <param name="alternateArtID">Used to search the database for an AlternateArt</param>
        /// <returns>Returns a AlternateArt of the specified alternateArtID.</returns>
        public AlternateArt SelectAlternateArtByID(string alternateArtID);

        /// <summary>
        /// Requests all fields from the AlternateArt table that are active to
        /// create a PaginatedResult of AlternateArts.
        /// </summary>
        /// <param name="pageNumber">Represents how much to offset the records by</param>
        /// <param name="pageSize">Represents how many records to return at most.</param>
        /// <returns>Returns a PaginatedResult of active alternate arts in the database.</returns>
        public PaginatedResult<AlternateArt> SelectActiveAlternateArts(int pageNumber = 1, int pageSize = 20);

        /// <summary>
        /// Requests all fields from the AlternateArt table that are deactive to
        /// create a PaginatedResult of AlternateArts.
        /// </summary>
        /// <param name="pageNumber">Represents how much to offset the records by</param>
        /// <param name="pageSize">Represents how many records to return at most.</param>
        /// <returns>Returns a PaginatedResult of deactive alternate arts in the database.</returns>
        public PaginatedResult<AlternateArt> SelectDeactiveAlternateArts(int pageNumber = 1, int pageSize = 20);

        /// <summary>
        /// Inserts the parameters into the stored procedure to try
        /// and create a new record for an AlternateArt.
        /// </summary>
        /// <param name="alternateArt">New AlternateArt object to insert.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int InsertAlternateArt(AlternateArt alternateArt);

        /// <summary>
        /// Updates the fields in the AlternateArt table at the alternateArtID.
        /// </summary>
        /// <param name="alternateArt">New AlternateArt object to update the old field at alternateArtID.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int UpdateAlternateArt(AlternateArt alternateArt);

        /// <summary>
        /// Deletes the row from the database where alternateArtID matches on in the table.
        /// </summary>
        /// <param name="alternateArtID">AlternateArtID of the row to delete.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int DeleteAlternateArt(string alternateArtID);

        /// <summary>
        /// Sets the active field to 0 to deactivate the record.
        /// </summary>
        /// <param name="alternateArtID">AlternateArtID of the row to deactivate.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int DeactivateAlternateArt(string alternateArtID);

        /// <summary>
        /// Sets the active field to 1 to reactivate the record.
        /// </summary>
        /// <param name="alternateArtID">AlternateArtID of the row to reactivate.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int ReactivateAlternateArt(string alternateArtID);

    }
}
