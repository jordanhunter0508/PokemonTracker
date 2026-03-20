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

        public List<Card> SelectAllCards();

        /// <summary>
        /// Inserts the parameters into the stored procedure to try
        /// and create a new record for a Card.
        /// </summary>
        /// <param name="card">New Card object to insert.</param>
        /// <returns>Returns the number of rows affected.</returns>
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

    }
}
