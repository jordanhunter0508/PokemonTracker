using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface IRuleAccessor
    {
        /// <summary>
        /// Requests all fields from the PokemonRule table to create a Rule.
        /// </summary>
        /// <param name="ruleID">Used to search the database for a Pokemon Rule</param>
        /// <returns>Returns a PokemonRule of the specified ruleID.</returns>
        public PokemonRule SelectRuleByRuleID(string ruleID);

        /// <summary>
        /// Requests all data from the PokemonRule table to
        /// create a Rule List.
        /// </summary>
        /// <returns>Returns a List of all pokemon rules in the database.</returns>
        public List<PokemonRule> SelectAllRules();

        /// <summary>
        /// Requests a list of records from the PokemonRule table that are active
        /// and fall with in the range of pageNumber and pageSize to
        /// create a PokemonRule List.
        /// </summary>
        /// <param name="pageNumber">Represents how much to offset the records by</param>
        /// <param name="pageSize">Represents how many records to return at most.</param>
        /// <returns>Returns a PaginatedResult of active rules in the database.</returns>
        public PaginatedResult<PokemonRule> SelectActiveRules(int pageNumber = 1, int pageSize = 20);

        /// <summary>
        /// Requests a list of records from the PokemonRule table that are deactive
        /// and fall with in the range of pageNumber and pageSize to
        /// create a PokemonRule List.
        /// </summary>
        /// <param name="pageNumber">Represents how much to offset the records by</param>
        /// <param name="pageSize">Represents how many records to return at most.</param>
        /// <returns>Returns a PaginatedResult of deactive rules in the database.</returns>
        public PaginatedResult<PokemonRule> SelectDeactiveRules(int pageNumber = 1, int pageSize = 20);

        /// <summary>
        /// Inserts the parameters into the stored procedure to try
        /// and create a new record for an Pokemon Rule.
        /// </summary>
        /// <param name="rule">New PokemonRule object to insert.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int InsertRule(PokemonRule rule);

        /// <summary>
        /// Updates the fields in the PokemonRule table at the ruleID.
        /// </summary>
        /// <param name="rule">New PokemonRule object to update the old field at ruleID.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int UpdateRule(PokemonRule rule);

        /// <summary>
        /// Deletes the row from the database where RuleID matches on in the table.
        /// </summary>
        /// <param name="ruleID">RuleID of the row to delete.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int DeleteRule(string ruleID);

        /// <summary>
        /// Sets the active field to 0 to deactivate the record.
        /// </summary>
        /// <param name="ruleID">RuleID of the row to deactivate.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int DeactivateRule(string ruleID);

        /// <summary>
        /// Sets the active field to 1 to reactivate the record.
        /// </summary>
        /// <param name="ruleID">RuleID of the row to reactivate.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int ReactivateRule(string ruleID);
    }
}
