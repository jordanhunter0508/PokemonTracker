using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface ICardAccessor
    {
        /// <summary>
        /// Requests all fields from the Card table to create a Card.
        /// </summary>
        /// <param name="cardID">Used to search the database for a card</param>
        /// <returns>Returns a Card of the specified cardID.</returns>
        public Card SelectCardByCardID(int cardID);

        /// <summary>
        /// Requests all cards from the database.
        /// Not all fields are returned
        /// </summary>
        /// <returns>Returns a List of all Cards for the database</returns>
        public List<Card> SelectAllCards();

        /// <summary>
        /// Requests a set number of cards based on pageSize from the database. <br/>
        /// Offset by pageNumber and filtered by filterOption.<br/>
        /// Saves the Cards in Items of the PaginatedReults.
        /// </summary>
        /// <param name="filterOption">Optional filters for the query</param>
        /// <param name="pageNumber">Represents how much to offset the records by</param>
        /// <param name="pageSize">Represents how many records to return at most.</param>
        /// <returns>Returns a paginated list of Cards from the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public PaginatedResult<Card> SelectCardsPaginated(FilterOption filterOption, int pageNumber = 1, int pageSize = 25);

        /// <summary>
        /// Passes parameters to <see href="InsertBooster()"/> Then returns true
        /// if the record was updated successfully.
        /// </summary>
        /// <param name="booster">New Booster object to be added to the database.</param>
        /// <returns>Returns true if the Booster was created successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the abbreviation is already used 
        /// or if the boosterId is already used.</exception>
        public int InsertCard(Card card);

        /// <summary>
        /// Updates the fields in the Card table at the cardID.
        /// </summary>
        /// <param name="card">New Card object to update the old field at cardID.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int UpdateCard(Card card);

        /// <summary>
        /// Deletes the row from the database where cardID matches in the table.
        /// </summary>
        /// <param name="cardID">CardID of the row to delete.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int DeleteCard(int cardID);

        /// <summary>
        /// Sets the Active field to the active parameter.
        /// </summary>
        /// <param name="cardID">CardID of the row to deactivate.</param>
        /// <param name="active">Used to activate or deactivate the card</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int ActivateCard(int cardID,bool active);
    }
}
