using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccessInterfaces;
using DataDomain;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class AbilityManager : IAbilityManager
    {
        IAbilityAccessor _abilityAccessor;

        /// <summary>
        /// General AbilityManager created for the presentaion layer
        /// </summary>
        public AbilityManager()
        {
            _abilityAccessor = new AbilityAccessor();
        }

        /// <summary>
        /// Used for testing to pass in fake data
        /// </summary>
        /// <param name="abilityAccessor">Set the IAbilityAccessor in the AbilityManager</param>
        public AbilityManager(IAbilityAccessor abilityAccessor)
        {
            _abilityAccessor = abilityAccessor;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityManager"/>
        /// </summary>
        public Ability GetAbilityByAbilityID(string abilityID)
        {
            Ability result = null;

            if (String.IsNullOrWhiteSpace(abilityID))
            {
                throw new ArgumentNullException("AbilityID cannot empty or null.");
            }

            try
            {
                result = _abilityAccessor.SelectAbilityByAbilityID(abilityID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get an ability.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityManager"/>
        /// </summary>
        public List<Ability> GetAllAbilities()
        {
            List<Ability> results = new List<Ability>();

            try
            {
                results  = _abilityAccessor.SelectAllAbilities();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve a list of all abilites.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityManager"/>
        /// </summary>
        public PaginatedResult<Ability> GetActiveAbilities(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<Ability> results = new PaginatedResult<Ability>();

            if (pageNumber <= 0)
            {
                throw new ArgumentException("Page number must be greater than 0.");
            }
            if (pageSize <= 0)
            {
                throw new ArgumentException("Page size must be greater than 0.");
            }

            try
            {
                results = _abilityAccessor.SelectActiveAbilities(pageNumber, pageSize);
                results.Items = FormatAbility(results.Items);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve a list of active abilities.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityManager"/>
        /// </summary>
        public PaginatedResult<Ability> GetDeactiveAbilities(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<Ability> results = new PaginatedResult<Ability>();

            if (pageNumber <= 0)
            {
                throw new ArgumentException("Page number must be greater than 0.");
            }
            if (pageSize <= 0)
            {
                throw new ArgumentException("Page size must be greater than 0.");
            }

            try
            {
                results = _abilityAccessor.SelectDeactiveAbilities(pageNumber, pageSize);
                results.Items = FormatAbility(results.Items);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve a list of deactivated abilities.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityManager"/>
        /// </summary>
        public PaginatedResult<Ability> GetAbilitiesByAbilityType(string abilityType, int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<Ability> results = new PaginatedResult<Ability>();

            if (String.IsNullOrWhiteSpace(abilityType))
            {
                throw new ArgumentNullException("Ability type cannot empty or null.");
            }
            if (pageNumber <= 0)
            {
                throw new ArgumentException("Page number must be greater than 0.");
            }
            if (pageSize <= 0)
            {
                throw new ArgumentException("Page size must be greater than 0.");
            }

            try
            {
                results = _abilityAccessor.SelectAbilitiesByAbilityType(abilityType, pageNumber, pageSize);
                results.Items = FormatAbility(results.Items);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve a list of abilities with a specified ability type.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityManager"/>
        /// </summary>
        public bool AddAbility(Ability ability)
        {
            bool result = false;

            if (ability == null)
            {
                throw new ArgumentNullException("Ability was empty.");
            }

            try
            {
                result = (1 == _abilityAccessor.InsertAbility(ability));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add an ability to the database.\n" +
                    "Please make sure the ability was not already created.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityManager"/>
        /// </summary>
        public bool EditAbility(Ability ability)
        {
            bool result = false;

            if (ability == null)
            {
                throw new ArgumentNullException("Ability was empty.");
            }

            try
            {
                result = (1 == _abilityAccessor.UpdateAbility(ability));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update the ability in the database.\n" +
                    "Please make sure the ability name was correct.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityManager"/>
        /// </summary>
        public bool DeleteAbility(string abilityID)
        {
            bool result = false;

            if (String.IsNullOrWhiteSpace(abilityID))
            {
                throw new ArgumentNullException("AbilityID cannot empty or null.");
            }


            try
            {
                result = (1 == _abilityAccessor.DeleteAbility(abilityID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete the ability in the database.\n" +
                    "Please make sure the ability is not attached to any cards.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityManager"/>
        /// </summary>
        public bool DeactivateAbility(string abilityID)
        {
            bool result = false;

            if (String.IsNullOrWhiteSpace(abilityID))
            {
                throw new ArgumentNullException("AbilityID cannot empty or null.");
            }

            try
            {
                result = (1 == _abilityAccessor.DeactivateAbility(abilityID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to deactivate the ability in the database.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityManager"/>
        /// </summary>
        public bool ReactivateAbility(string abilityID)
        {
            bool result = false;

            if (String.IsNullOrWhiteSpace(abilityID))
            {
                throw new ArgumentNullException("AbilityID cannot empty or null.");
            }

            try
            {
                result = (1 == _abilityAccessor.ReactivateAbility(abilityID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to reactivate the ability in the database.", ex);
            }

            return result;
        }

        /// <summary>
        /// Makes sure the first letter of the ID is capitalized and then sorts them by
        /// AbilityID
        /// </summary>
        /// <param name="abilities">The IEnumerable that is being sorted</param>
        /// <returns>Returns an IEnumberable of type Ability that is formated for dispaly.</returns>
        private List<Ability> FormatAbility(IEnumerable<Ability> abilities)
        {
            if (abilities == null)
            {
                throw new ArgumentNullException("Abilities could not be formatted.");
            }

            foreach (Ability ability in abilities)
            {
                ability.AbilityID = char.ToUpper(ability.AbilityID[0]) + ability.AbilityID.Substring(1);
            }

            abilities = abilities.OrderBy(ability => ability.AbilityID);
            return abilities.ToList();
        }


    }
}
