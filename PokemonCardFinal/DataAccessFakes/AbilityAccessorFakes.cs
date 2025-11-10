using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class AbilityAccessorFakes : IAbilityAccessor
    {
        List<Ability> _abilities;

        /// <summary>
        /// Fills the _abilities list with fake data
        /// </summary>
        public AbilityAccessorFakes()
        {
            _abilities = new List<Ability>();
            _abilities.Add(new Ability()
            {
                AbilityID = "Ability Test 1",
                AbilityType = "Ability Type",
                Description = "This is description 1."
            });
            _abilities.Add(new Ability()
            {
                AbilityID = "Ability Test 2",
                AbilityType = "Ability Type",
                Description = "This is description 2."
            });
            _abilities.Add(new Ability()
            {
                AbilityID = "Ability Test 3",
                AbilityType = "Ability Type",
                Description = "This is description 3."
            });
            _abilities.Add(new Ability()
            {
                AbilityID = "Ability Test 4",
                AbilityType = "Ability Type",
                Description = "This is description 4."
            });
            _abilities.Add(new Ability()
            {
                AbilityID = "Ability Test 5",
                AbilityType = "Ability Test",
                Description = "This is description 5."
            });
        }

        /// <summary>
        /// Implements from <see cref="IAbilityAccessor"/> used for testing
        /// </summary>
        public Ability SelectAbilityByAbilityID(string abilityID)
        {
            Ability result = null;

            foreach (Ability ability in _abilities)
            {
                if (ability.AbilityID == abilityID)
                {
                    result = ability;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityAccessor"/> used for testing
        /// </summary>
        public List<Ability> SelectAbilities()
        {
            List<Ability> results;
            results = _abilities;
            return results;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityAccessor"/> used for testing
        /// </summary>
        public List<Ability> SelectAbilitiesByAbilityType(string abilityType)
        {
            IEnumerable<Ability> results;
            results = from ability in _abilities
                      where ability.AbilityType == abilityType
                      orderby ability.AbilityID
                      select ability;
            return results.ToList();
        }

        /// <summary>
        /// Implements from <see cref="IAbilityAccessor"/> used for testing
        /// </summary>
        public int InsertAbility(Ability ability)
        {
            int count = 0;

            // checks if the abilityID is already used
            foreach (Ability element in _abilities)
            {
                if (element.AbilityID == ability.AbilityID)
                {
                    throw new Exception("Ability ID already used.");
                }
            }

            _abilities.Add(ability);
            count = 1;

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityAccessor"/> used for testing
        /// </summary>
        public int UpdateAbility(Ability ability)
        {
            int count = 0;
            Ability updatedAbility = null;

            foreach (Ability element in _abilities)
            {
                if (element.AbilityID == ability.AbilityID)
                {
                    updatedAbility = element;
                    break;
                }
            }

            if (updatedAbility != null)
            {
                updatedAbility.AbilityType = ability.AbilityType;
                updatedAbility.Description = ability.Description;
                count++;
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityAccessor"/> used for testing
        /// </summary>
        public int DeleteAbility(string abilityID)
        {
            int count = 0;
            Ability deleteAbility = null;

            foreach (Ability ability in _abilities)
            {
                if (ability.AbilityID == abilityID)
                {
                    count++;
                    deleteAbility = ability;
                }
            }

            if (count == 1)
            {
                _abilities.Remove(deleteAbility);
            }

            return count;
        }
    }
}
