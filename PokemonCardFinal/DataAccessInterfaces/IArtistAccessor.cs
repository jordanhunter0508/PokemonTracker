using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface IArtistAccessor
    {
        /// <summary>
        /// Requests all fields from the Artist table to create an Artist.
        /// </summary>
        /// <param name="artistID">Used to search the database for an artist</param>
        /// <returns>Returns an Artist of the specified artistID.</returns>
        public Artist SelectArtistByArtistID(int artistID);

        /// <summary>
        /// Requests all fields from the Artist table to create an Artist.
        /// Both givenName and surname must match one from the database.
        /// </summary>
        /// <param name="givenName">Used to search the database for a match</param>
        /// <param name="surname">Used to search the database for a match</param>
        /// <returns>Returns an Artist of the specified givenName and surname.</returns>
        public Artist SelectArtistByArtistName(string givenName, string surname);

        /// <summary>
        /// Requests a list of records from the Artist table that are active
        /// and fall with in the range of pageNumber and pageSize to
        /// create an Artist List.
        /// </summary>
        /// <param name="pageNumber">Represents how much to offset the records by</param>
        /// <param name="pageSize">Represents how many records to return at most.</param>
        /// <returns>Returns a PaginatedResult of active artists in the database.</returns>
        public PaginatedResult<Artist> SelectActiveArtists(int pageNumber = 1, int pageSize = 20);

        /// <summary>
        /// Requests a list of records from the Artist table that are not active
        /// and fall with in the range of pageNumber and pageSize to
        /// create an Artist List.
        /// </summary>
        /// <param name="pageNumber">Represents how much to offset the records by</param>
        /// <param name="pageSize">Represents how many records to return at most.</param>
        /// <returns>Returns a PaginatedResult of deactive artists in the database.</returns>
        public PaginatedResult<Artist> SelectDeactiveArtists(int pageNumber = 1, int pageSize = 20);

        /// <summary>
        /// Requests all data from the Artist table to
        /// create an Artist List.
        /// </summary>
        /// <returns>Returns a List of all artists in the database.</returns>
        public List<Artist> SelectAllArtists();

        /// <summary>
        /// Inserts the parameters into the stored procedure to try
        /// and create a new record for an Artist
        /// </summary>
        /// <param name="giveName">Given name of the artist wanting to create</param>
        /// <param name="surname">Surname of the artist wanting to create</param>
        /// <returns>Returns the artist id of the newly created artist</returns>
        public int InsertArtist(string givenName, string surname);

        /// <summary>
        /// Updates the givenName nad surname of a specified artist at artistID.<br/>
        /// Changes givenName and surname in the table to the parameters.
        /// </summary>
        /// <param name="artistID">Used to search the table Artist for a match</param>
        /// <param name="giveName">Used to change the give name of an artist at artistID</param>
        /// <param name="surname">Used to changed the surname of an artist at artistID</param>
        /// <returns>Returns 1 if the record at artistID updated successfully</returns>
        public int UpdateArtist(int artistID,string givenName, string surname);

        /// <summary>
        /// Deletes the record at artistID
        /// </summary>
        /// <param name="artistID">Used to search the table Artist for a match</param>
        /// <returns>Returns 1 if the record at artistID was deleted successfully</returns>
        public int DeleteArtist(int artistID);

        /// <summary>
        /// Sets the active field to 0 to deactivate the record.
        /// </summary>
        /// <param name="artistID">ArtistID of the row to deactivate.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int DeactivateArtist(int artistID);

        /// <summary>
        /// Sets the active field to 1 to reactivate the record.
        /// </summary>
        /// <param name="artistID">ArtistID of the row to reactivate.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int ReactivateArtist(int artistID);

    }
}
