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
        /// Passes parameters to <see href="SelectAbilityByAbilityID(string)"/><br/>
        /// then returns the results of the query. 
        /// </summary>
        /// <param name="abilityID">Used to search the database for the ability</param>
        /// <returns>Returns an Abiltiy from the database where the abilityID match</returns>
        /// <exception cref="ApplicationException">Throws if the abilityID could not be found</exception>
        public Ability GetAbilityByAbilityID(string abilityID);

        /// <summary>
        /// Calls the <see href="SelectAbilties()"/> method to get<br/>
        /// a list of all Abilities from the database.
        /// </summary>
        /// <returns>Returns a List of all Abilities in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<Ability>GetAbilities();

        /// <summary>
        /// Calls the <see href="SelectAbilityByAbilityType(string)"/> method to get<br/>
        /// a list of all Abilites from the database that hava a matching abilityType.
        /// </summary>
        /// <param name="abilityType">Used to search the database for abilites with the same type.</param>
        /// <returns>Returns a List of abilities in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<Ability> GetAbilityByAbilityType(string abilityType);

        // Maybe used instead of requerying a full list can query the old list with a type
        //public IEnumerable<Ability> GetAbilityByAbilityType(string abilityType, IEnumerable<Ability> abilites);

        /// <summary>
        /// Passes parameters to <see href="InsertAbility(Ability)"/><br/>
        /// Then returns true if the record was created successfully
        /// </summary>
        /// <param name="ability">New Ability object to be added to the database.</param>
        /// <returns>Returns true if the Ability was created successfully.</returns>
        /// <exception cref="ApplicationException">Throws if the abilityId is already used.</exception>
        public bool AddAbility(Ability ability);

        /// <summary>
        /// Passes parameters to <see href="UpdateAbiliity(Ability)"/><br/>
        /// Then returns true if the record was updated successfully.
        /// </summary>
        /// <param name="ability">New Ability object to update the old field at abilityID</param>
        /// <returns>Returns true if the Ability was updated successfully.</returns>
        /// <exception cref="ApplicationException">Throws if there is an error storing the data.</exception>
        public bool EditAbility(Ability ability);

        /// <summary>
        /// Passes parameters to <see href="DeleteAbility(string)"/><br/>
        /// Then returns true if the record was deleted successfully
        /// </summary>
        /// <param name="abilityID">Used to find the Ability</param>
        /// <returns>Returns true if the Ability was deleted successfully</returns>
        /// <exception cref="ApplicationException">Throws if the ability is attached to a card</exception>
        public bool DeleteAbility(string abilityID);

        /// <summary>
        /// Makes sure the first letter of the ID is capitalized and then sorts them by
        /// AbilityID
        /// </summary>
        /// <param name="abilities">The IEnumerable that is being sorted</param>
        /// <returns>Returns an IEnumberable of type Ability that is formated for dispaly.</returns>
        public IEnumerable<Ability> FormatAbility(IEnumerable<Ability> abilities);
    }
}
