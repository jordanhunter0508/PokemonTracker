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

            try
            {
                result = _abilityAccessor.SelectAbilityByAbilityID(abilityID);
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed to get an ability.");
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityManager"/>
        /// </summary>
        public List<Ability> GetAbilities()
        {
            List<Ability> results = null;

            try
            {
                results = _abilityAccessor.SelectAbilities();
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed to retrieve a list of abilities.");
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityManager"/>
        /// </summary>
        public List<Ability> GetAbilityByAbilityType(string abilityType)
        {
            List<Ability> results = null;

            try
            {
                results = _abilityAccessor.SelectAbilitiesByAbilityType(abilityType);
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed to retrieve a list of abilities with a specified ability type.");
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
            catch (Exception)
            {
                throw new ApplicationException("Failed to add an ability to the database.\n" +
                    "Please make sure the ability was not already created.");
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
            catch (Exception)
            {
                throw new ApplicationException("Failed to update the ability in the database.\n" +
                    "Please make sure the ability name was correct.");
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityManager"/>
        /// </summary>
        public bool DeleteAbility(string abilityID)
        {
            bool result = false;

            try
            {
                result = (1 == _abilityAccessor.DeleteAbility(abilityID));
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed to delete the ability in the database.\n" +
                    "Please make sure the ability is not attached to any cards.");
            }

            return result;
        }

        public IEnumerable<Ability> FormatAbility(IEnumerable<Ability> abilities)
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
            return abilities;
        }
    }
}
