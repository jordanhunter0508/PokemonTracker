using DataDomain;

namespace LogicLayerInterfaces
{
    public interface IRuleManager
    {
        /// <summary>
        /// Passes parameters to <see href="SelectRuleByRuleID(string)"/><br/>
        /// then returns the results of the query. 
        /// </summary>
        /// <param name="ruleID">Used to search the database for the pokemon ruls</param>
        /// <returns>Returns a PokemonRule from the database where the ruleIDs match</returns>
        /// <exception cref="ApplicationException">Throws if the ruleID could not be found</exception>
        public PokemonRule GetRuleByRuleID(string ruleID);

        /// <summary>
        /// Calls the <see href="SelectRules()"/> method to get<br/>
        /// a list of all PokemonRules from the database.
        /// </summary>
        /// <returns>Returns a List of all PokemonRules in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<PokemonRule> GetAllRules();

        /// <summary>
        /// Calls the <see href="IRuleAccessor.SelectActiveRules(int,int)"/> method to get<br/>
        /// a list of PokemonRules from the database.
        /// </summary>
        /// <param name="pageNumber">Represents what page to pull from.</param>
        /// <param name="pageSize">Represents how many items are on the page.</param>
        /// <returns>Returns a PaginatedResult where the Items is a list of PokemonRules that are active</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public PaginatedResult<PokemonRule> GetActiveRules(int pageNumber = 1, int pageSize = 20);

        /// <summary>
        /// Calls the <see href="IRuleAccessor.SelectDeactiveRules(int,int)"/> method to get<br/>
        /// a list of PokemonRules from the database.
        /// </summary>
        /// <param name="pageNumber">Represents what page to pull from.</param>
        /// <param name="pageSize">Represents how many items are on the page.</param>
        /// <returns>Returns a PaginatedResult where the Items is a list of PokemonRules that are deactive</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public PaginatedResult<PokemonRule> GetDeactiveRules(int pageNumber = 1, int pageSize = 20);

        /// <summary>
        /// Passes parameters to <see href="InsertRule(PokemonRule)"/> Then returns true
        /// if the record was updated successfully.
        /// </summary>
        /// <param name="rule">New PokemonRule object to be added to the database.</param>
        /// <returns>Returns true if the Pokemon Rule was created successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the ruleID is already used.</exception>
        public bool AddRule(PokemonRule rule);

        /// <summary>
        /// Passes parameters to <see href="UpdateRule(PokemonRule)"/><br/>
        /// Then returns true if the record was updated successfully.
        /// </summary>
        /// <param name="rule">New PokemonRule object to update the old field at ruleID</param>
        /// <returns>Returns true if the PokemonRule was updated successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the ruleID is already used.</exception>
        public bool EditRule(PokemonRule rule);

        /// <summary>
        /// Passes parameters to <see href="DeleteRule(string)"/><br/>
        /// Then returns true if the record was deleted successfully
        /// </summary>
        /// <param name="ruleID">Used to find the PokemonRule</param>
        /// <returns>Returns true if the Pokemon Rule was deleted successfully</returns>
        /// <exception cref="ApplicationException">Throws if the PokemonRule is attached to a card</exception>
        public bool DeleteRule(string ruleID);

        /// <summary>
        /// Passes parameters to <see href="IRuleAccessor.DeactivateRule(string)"/><br/>
        /// Then returns true if the record was deactivated successfully
        /// </summary>
        /// <param name="ruleID">RuleID of the row to deactivate.</param>
        /// <returns>Returns the number of rows affected.</returns>
        /// <exception cref="ApplicationException">Throws if there is an error connection to the database</exception>
        public bool DeactivateRule(string ruleID);

        /// <summary>
        /// Passes parameters to <see href="IRuleAccessor.ReactivateRule(string)"/><br/>
        /// Then returns true if the record was reactivated successfully
        /// </summary>
        /// <param name="ruleID">RuleID of the row to reactivate.</param>
        /// <returns>Returns the number of rows affected.</returns>
        /// <exception cref="ApplicationException">Throws if there is an error connection to the database</exception>
        public bool ReactivateRule(string ruleID);
    }
}
