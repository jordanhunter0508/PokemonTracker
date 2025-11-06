using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface IArtistManager
    {
        /// <summary>
        /// Passes parameters to <see href="SelectArtistByArtistID(int)"/><br/>
        /// then returns the results of the query. 
        /// </summary>
        /// <param name="artistID">Used to search the database for the artist</param>
        /// <returns>Returns an Aritst from the database where the artistIDs match</returns>
        /// <exception cref="ApplicationException">Throws if the artistID could not be found</exception>
        public Artist GetArtistByArtistID(int artistID);

        /// <summary>
        /// Passes parameters to <see href="SelectArtistByArtistName(string,string)"/><br/>
        /// then returns the results of the query. 
        /// </summary>
        /// <param name="givenName">Used to search the database for the artist</param>
        /// <param name="surname">Used to search the database for the artist</param>
        /// <returns>Returns an Aritst from the database where the artistIDs match</returns>
        /// <exception cref="ApplicationException">Throws if the give name or surname cannot be found.</exception>
        public Artist GetArtistByName(string givenName, string surname);

        /// <summary>
        /// Calls the <see href="SelectArtists()"/> method to get<br/>
        /// a list of all Artists from the database.
        /// </summary>
        /// <returns>Returns a List of all Artists in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<Artist> GetArtists();

        /// <summary>
        /// Passes parameters to <see href="InsertArtist(string,string)"/><br/>
        /// Then returns true if the record was created successfully
        /// </summary>
        /// <param name="givenName">Given name of the artist wanting to create</param>
        /// <param name="surname">Surname of the artist wanting to create</param>
        /// <returns>Returns true of the Artist was created, false if not</returns>
        /// <exception cref="ApplicationException">Throws if the combination of givenName and 
        /// surname are alredy used.</exception>
        public bool AddArtist(string givenName, string surname);

        /// <summary>
        /// Passes parameters to <see href="UpdateArtistByArtistID(int,string,string)"/><br/>
        /// Then returns true if the record was updated successfully
        /// </summary>
        /// <param name="artistID">Used to find the Artist</param>
        /// <param name="giveName">Used to update the GiveName field</param>
        /// <param name="surname">Used to update the Surname field</param>
        /// <returns>Returns true if the Artist was updated successfully</returns>
        /// <exception cref="ApplicationException">Throws if the combination of the given name and surname is already used.</exception>
        public bool EditArtistByArtistID(int artistID, string givenName, string surname);

        /// <summary>
        /// Passes parameters to <see href="DeleteArtistByArtistID(int)"/><br/>
        /// Then returns true if the record was deleted successfully
        /// </summary>
        /// <param name="artistID">Used to find the Artist</param>
        /// <returns>Returns true if the Artist was deleted successfully</returns>
        /// <exception cref="ApplicationException">Throws if the artist is attached to a card</exception>
        public bool DeleteArtistByArtistID(int artistID);

        /// <summary>
        /// Makes sure the first leter of the given and surname a is capital
        /// then sorts by the id
        /// </summary>
        /// <param name="artists">The IEnumerable that is being sorted</param>
        /// <returns>Returns an IEnumberable of type Artist that is formated for dispaly.</returns>
        public IEnumerable<Artist> FormatArtists(IEnumerable<Artist> artists);
    }
}
