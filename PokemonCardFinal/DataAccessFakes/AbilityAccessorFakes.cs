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
                Description = "This is description 1.",
                Active = true,
            });
            _abilities.Add(new Ability()
            {
                AbilityID = "Ability Test 2",
                AbilityType = "Ability Type",
                Description = "This is description 2.",
                Active = true,
            });
            _abilities.Add(new Ability()
            {
                AbilityID = "Ability Test 3",
                AbilityType = "Ability Type",
                Description = "This is description 3.",
                Active = true,
            });
            _abilities.Add(new Ability()
            {
                AbilityID = "Ability Test 4",
                AbilityType = "Ability Type",
                Description = "This is description 4.",
                Active = true,
            });
            _abilities.Add(new Ability()
            {
                AbilityID = "Ability Test 5",
                AbilityType = "Ability Test",
                Description = "This is description 5.",
                Active = false,
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
        public PaginatedResult<Ability> SelectActiveAbilities(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<Ability> results = new PaginatedResult<Ability>();

            IEnumerable<Ability> activeAbilities = _abilities.Where(ability => ability.Active);

            results.TotalCount = activeAbilities.Count();
            results.PageNumber = pageNumber;
            results.PageSize = pageSize;

            results.Items = activeAbilities.Skip((pageNumber - 1) * pageSize)
                                           .Take(pageSize)
                                           .ToList();
            return results;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityAccessor"/> used for testing
        /// </summary>
        public PaginatedResult<Ability> SelectDeactiveAbilities(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<Ability> results = new PaginatedResult<Ability>();

            IEnumerable<Ability> deactiveAbilities = _abilities.Where(ability => !ability.Active);

            results.TotalCount = deactiveAbilities.Count();
            results.PageNumber = pageNumber;
            results.PageSize = pageSize;

            results.Items = deactiveAbilities.Skip((pageNumber - 1) * pageSize)
                                           .Take(pageSize)
                                           .ToList();
            return results;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityAccessor"/> used for testing
        /// </summary>
        public PaginatedResult<Ability> SelectAbilitiesByAbilityType(string abilityType, int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<Ability> results = new PaginatedResult<Ability>();

            IEnumerable<Ability> abilitiesByType = _abilities.Where(ability => 
                                                                    ability.Active && 
                                                                    ability.AbilityType == abilityType);

            results.TotalCount = abilitiesByType.Count();
            results.PageNumber = pageNumber;
            results.PageSize = pageSize;

            results.Items = abilitiesByType.Skip((pageNumber - 1) * pageSize)
                                           .Take(pageSize)
                                           .ToList();
            return results;
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
            int index = 0;
            Ability updatedAbility = null;

            for (int i = 0; i < _abilities.Count; i++)
            {
                if (_abilities[i].AbilityID == ability.AbilityID)
                {
                    updatedAbility = _abilities[i];
                    index = i;
                    break;
                }
            }

            if (updatedAbility != null)
            {
                updatedAbility.AbilityID = ability.AbilityID;
                updatedAbility.AbilityType = ability.AbilityType;
                updatedAbility.Description = ability.Description;

                _abilities[index] = updatedAbility;

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

        /// <summary>
        /// Implements from <see cref="IAbilityAccessor"/> used for testing
        /// </summary>
        public int DeactivateAbility(string abilityID)
        {
            int count = 0;
            foreach (Ability ability in _abilities)
            {
                if (ability.AbilityID == abilityID)
                {
                    ability.Active = false;
                    count = 1;
                    break;
                }
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityAccessor"/> used for testing
        /// </summary>
        public int ReactivateAbility(string abilityID)
        {
            int count = 0;
            foreach (Ability ability in _abilities)
            {
                if (ability.AbilityID == abilityID)
                {
                    ability.Active = true;
                    count = 1;
                    break;
                }
            }

            return count;
        }
    }
}
