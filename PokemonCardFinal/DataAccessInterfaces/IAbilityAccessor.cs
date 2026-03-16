using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface IAbilityAccessor
    {
        /// <summary>
        /// Requests all fields from the Ability table to create an Ability.
        /// </summary>
        /// <param name="abilityID">Used to search the database for an Ability</param>
        /// <returns>Returns a Ability of the specified abilityID.</returns>
        public Ability SelectAbilityByAbilityID(string abilityID);

        /// <summary>
        /// Requests all records from the Ability table to create a list of Abilities
        /// </summary>
        /// <returns>Returns all abilites from the database</returns>
        public List<Ability> SelectAllAbilities();

        /// <summary>
        /// Requests a list of records from the Ability table that are active
        /// and fall with in the range of pageNumber and pageSize to
        /// create an Ability List.
        /// </summary>
        /// <param name="pageNumber">Represents how much to offset the records by</param>
        /// <param name="pageSize">Represents how many records to return at most.</param>
        /// <returns>Returns a PaginatedResult of active abilities in the database.</returns>
        public PaginatedResult<Ability> SelectActiveAbilities(int pageNumber = 1, int pageSize = 20);

        /// <summary>
        /// Requests a list of records from the Ability table that are not active
        /// and fall with in the range of pageNumber and pageSize to
        /// create an Ability List.
        /// </summary>
        /// <param name="pageNumber">Represents how much to offset the records by</param>
        /// <param name="pageSize">Represents how many records to return at most.</param>
        /// <returns>Returns a PaginatedResult of deactive abilities in the database.</returns>
        public PaginatedResult<Ability> SelectDeactiveAbilities(int pageNumber = 1, int pageSize = 20);

        /// <summary>
        /// Requests all fields from the Ability table where the abilityType match and
        /// fall with in the range of pageNumber and pageSize.
        /// </summary>
        /// <param name="abilityType">Used to search the database for am Ability</param>
        /// <param name="pageNumber">Represents how much to offset the records by</param>
        /// <param name="pageSize">Represents how many records to return at most.</param>
        /// <returns>Returns a PaginatedResult of abilities where the abilityType matches.</returns>
        public PaginatedResult<Ability> SelectAbilitiesByAbilityType(string abilityType, int pageNumber = 1, int pageSize = 20);

        /// <summary>
        /// Inserts the parameters into the stored procedure to try
        /// and create a new record for an Ability.
        /// </summary>
        /// <param name="ability">New Abiility object to insert.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int InsertAbility(Ability ability);

        /// <summary>
        /// Updates the fields in the Ability table at the abilityID.
        /// </summary>
        /// <param name="ability">New Ability object to update the old field at abilityID.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int UpdateAbility(Ability ability);

        /// <summary>
        /// Deletes the row from the database where abilityID matches on in the table.
        /// </summary>
        /// <param name="abilityID">AbilityID of the row to delete.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int DeleteAbility(string abilityID);

        /// <summary>
        /// Sets the active field to 0 to deactivate the record.
        /// </summary>
        /// <param name="abilityID">AbilityID of the row to deactivate.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int DeactivateAbility(string abilityID);

        /// <summary>
        /// Sets the active field to 1 to reactivate the record.
        /// </summary>
        /// <param name="abilityID">AbilityID of the row to reactivate.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public int ReactivateAbility(string abilityID);


    }
}
