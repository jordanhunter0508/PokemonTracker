using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface ICardManager 
    {

        /// <summary>
        /// Passes parameters to <see href="ICardAccessor.SelectCardByCardID(string)"/><br/>
        /// then returns the results of the query. 
        /// </summary>
        /// <param name="cardID">ID used to find the related Card</param>
        /// <returns>Returns a CardVM with all components initialized</returns>
        /// <exception cref="ApplicationException">Throws if the cardID could not be found</exception>
        public CardVM GetCardVM(int cardID);

        /// <summary>
        /// Calls the <see href="ICardAccessor.SelecAllCards()"/> method to get<br/>
        /// a list of all Cards from the database.
        /// </summary>
        /// <returns>Returns a List of all Cards in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<Card> GetAllCards();

        /// <summary>
        /// Calls the <see href="ICardAccessor.SelectCardsPaginated(FilterOption,int,int)"/> method to get<br/>
        /// a PaginatedResult where the Items are Cards.
        /// </summary>
        /// <param name="filterOption">Optional filters for the query</param>
        /// <param name="pageNumber">Represents how much to offset the records by</param>
        /// <param name="pageSize">Represents how many records to return at most.</param>
        /// <returns>Returns a paginated list of Cards from the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public PaginatedResult<Card> GetCardsPaginated(FilterOption filterOption, int pageNumber = 1, int pageSize = 25);

        /// <summary>
        /// Passes parameters to <see href="ICardAccessor.InsertCard(Card)"/> Then returns the Id
        /// of the newly created card.
        /// </summary>
        /// <param name="card">New Card object to be added to the database.</param>
        /// <returns>Returns true if the Card was created successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the boosterID and boosterNumber are already used.</exception>s
        public int AddCard(Card card);

        /// <summary>
        /// Passes parameters to <see href="ICardAccessor.UpdateCard(Card)"/><br/>
        /// Then returns true if the record was updated successfully.
        /// </summary>
        /// <param name="card">New Card object to update the old field at cardID</param>
        /// <returns>Returns true if the Card was updated successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the boosterId and boosterNumber are already used.</exception>
        public bool EditCard(Card card);

        /// <summary>
        /// Passes parameters to <see href="ICardAccessor.DeleteCard(int)"/><br/>
        /// Then returns true if the record was deleted successfully
        /// </summary>
        /// <param name="cardID">Used to find the Card</param>
        /// <returns>Returns true if the Card was deleted successfully</returns>
        /// <exception cref="ApplicationException">Throws if there was a problem connecting to the database.</exception>
        public bool DeleteCard(int cardID);

        /// <summary>
        /// Filters a set of cards using FilterOption.
        /// Multiple filter options can be inserted into filterOption
        /// </summary>
        /// <param name="cards">Set of cards to be filtered</param>
        /// <param name="filterOption">FilerOptions to apply to cards</param>
        /// <returns>Returns an IEnumerable of Cards where the filterOptions apply</returns>
        /// <exception cref="ArgumentNullException">Throws cards is null</exception>
        IEnumerable<Card> ApplyFilters(IEnumerable<Card> cards, FilterOption filterOption);
    }
}
