using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface IAbilityManager
    {
        /// <summary>
        /// Passes parameters to <see href="IAbilityAccessor.SelectAbilityByAbilityID(string)"/><br/>
        /// then returns the results of the query. 
        /// </summary>
        /// <param name="abilityID">Used to search the database for the ability</param>
        /// <returns>Returns an Abiltiy from the database where the abilityID match</returns>
        /// <exception cref="ApplicationException">Throws if the abilityID could not be found</exception>
        public Ability GetAbilityByAbilityID(string abilityID);

        /// <summary>
        /// Calls the <see href="IAbilityAccessor.SelectActiveAbilties()"/> method to get<br/>
        /// a list of Abilities from the database.
        /// </summary>
        /// <returns>Returns a List of all Abilities that are active in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public PaginatedResult<Ability> GetActiveAbilities(int pageNumber = 1, int pageSize = 20);

        /// <summary>
        /// Calls the <see href="IAbilityAccessor.SelectDeactiveAbilites()"/> method to get<br/>
        /// a list of Abilities from the database.
        /// </summary>
        /// <returns>Returns a List of all Abilities that are deactive in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public PaginatedResult<Ability> GetDeactiveAbilities(int pageNumber = 1, int pageSize = 20);

        /// <summary>
        /// Calls the <see href="IAbilityAccessor.SelectAbilityByAbilityType(string)"/> method to get<br/>
        /// a list of Abilites from the database that hava a matching abilityType.
        /// </summary>
        /// <param name="abilityType">Used to search the database for abilites with the same type.</param>
        /// <returns>Returns a List of abilities in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public PaginatedResult<Ability> GetAbilitiesByAbilityType(string abilityType, int pageNumber = 1, int pageSize = 20);

        /// <summary>
        /// Passes parameters to <see href="IAbilityAccessor.InsertAbility(Ability)"/><br/>
        /// Then returns true if the record was created successfully
        /// </summary>
        /// <param name="ability">New Ability object to be added to the database.</param>
        /// <returns>Returns true if the Ability was created successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the abilityId is already used.</exception>
        public bool AddAbility(Ability ability);

        /// <summary>
        /// Passes parameters to <see href="IAbilityAccessor.UpdateAbiliity(Ability)"/><br/>
        /// Then returns true if the record was updated successfully.
        /// </summary>
        /// <param name="ability">New Ability object to update the old field at abilityID</param>
        /// <returns>Returns true if the Ability was updated successfully.</returns>
        /// <exception cref="ApplicationException">Throws if there is an error storing the data.</exception>
        public bool EditAbility(Ability ability);

        /// <summary>
        /// Passes parameters to <see href="IAbilityAccessor.DeleteAbility(string)"/><br/>
        /// Then returns true if the record was deleted successfully
        /// </summary>
        /// <param name="abilityID">Used to find the Ability</param>
        /// <returns>Returns true if the Ability was deleted successfully</returns>
        /// <exception cref="ApplicationException">Throws if the ability is attached to a card</exception>
        public bool DeleteAbility(string abilityID);

        /// <summary>
        /// Passes parameters to <see href="IAbilityAccessor.DeactivateAbility(string)"/><br/>
        /// Then returns true if the record was deactivated successfully
        /// </summary>
        /// <param name="abilityID">AbilityID of the row to deactivate.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public bool DeactivateAbility(string abilityID);

        /// <summary>
        /// Passes parameters to <see href="IAbilityAccessor.DeactivateAbility(string)"/><br/>
        /// Then returns true if the record was deactivated successfully
        /// </summary>
        /// <param name="abilityID">AbilityID of the row to reactivate.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public bool ReactivateAbility(string abilityID);
    }
}
