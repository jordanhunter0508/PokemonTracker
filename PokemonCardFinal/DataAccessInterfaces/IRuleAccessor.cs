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
