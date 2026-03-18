using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class RuleAccessorFakes : IRuleAccessor
    {
        List<PokemonRule> _rules;

        /// <summary>
        /// Fills the _artists list with fake data
        /// </summary>
        public RuleAccessorFakes()
        {
            _rules = new List<PokemonRule>();

            _rules.Add(new PokemonRule()
            {
                RuleID = "Test Rule 1",
                Description = "This is a test.",
                Active = true,
            });
            _rules.Add(new PokemonRule()
            {
                RuleID = "Test Rule 2",
                Description = "This is not a test.",
                Active = true,
            });
            _rules.Add(new PokemonRule()
            {
                RuleID = "Test Rule 3",
                Description = "This is really not a test.",
                Active = false,
            });
            _rules.Add(new PokemonRule()
            {
                RuleID = "Test Rule 4",
                Description = "This is really not a test.",
                Active = false,
            });
            _rules.Add(new PokemonRule()
            {
                RuleID = "Test Rule 5",
                Description = "This is really not a test.",
                Active = true,
            });
        }

        /// <summary>
        /// Implements from <see cref="IRuleAccessor"/> used for testing
        /// </summary>
        public PokemonRule SelectRuleByRuleID(string ruleID)
        {
            PokemonRule result = null;

            foreach (PokemonRule rule in _rules)
            {
                if (rule.RuleID == ruleID)
                { 
                    result = rule;
                }
            }
            return result;
        }

        /// <summary>
        /// Implements from <see cref="IRuleAccessor"/> used for testing
        /// </summary>
        public List<PokemonRule> SelectAllRules()
        {
            List<PokemonRule> results = null;
            results = _rules;
            return results;
        }

        /// <summary>
        /// Implements from <see cref="IRuleAccessor"/> used for testing
        /// </summary>
        public PaginatedResult<PokemonRule> SelectActiveRules(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<PokemonRule> results = new PaginatedResult<PokemonRule>();

            IEnumerable<PokemonRule> activeRules = _rules.Where(rule => rule.Active);

            results.PageNumber = pageNumber;
            results.PageSize = pageSize;
            results.TotalCount = activeRules.Count();
            results.TotalPages = (int)Math.Ceiling((double)activeRules.Count() / pageSize);

            results.Items = activeRules.Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .ToList();

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IRuleAccessor"/> used for testing
        /// </summary>
        public PaginatedResult<PokemonRule> SelectDeactiveRules(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<PokemonRule> results = new PaginatedResult<PokemonRule>();

            IEnumerable<PokemonRule> deactiveRules = _rules.Where(rule => !rule.Active);

            results.PageNumber = pageNumber;
            results.PageSize = pageSize;
            results.TotalCount = deactiveRules.Count();
            results.TotalPages = (int)Math.Ceiling((double)deactiveRules.Count() / pageSize);

            results.Items = deactiveRules.Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .ToList();

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IRuleAccessor"/> used for testing
        /// </summary>
        public int InsertRule(PokemonRule rule)
        {
            int count = 0;

            foreach (PokemonRule element in _rules)
            {
                if (element.RuleID == rule.RuleID)
                {
                    throw new Exception("Rule ID already used.");
                }
            }

            _rules.Add(rule);
            count++;

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IRuleAccessor"/> used for testing
        /// </summary>
        public int UpdateRule(PokemonRule rule)
        {
            int count = 0;
            PokemonRule updatedRule = null;

            foreach (PokemonRule element in _rules)
            {
                if (element.RuleID == rule.RuleID)
                {
                    updatedRule = element;
                    break;
                }
            }

            if (updatedRule != null)
            {
                updatedRule = rule;
                count++;
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IRuleAccessor"/> used for testing
        /// </summary>
        public int DeleteRule(string ruleID)
        {
            int count = 0;
            PokemonRule deleteRule = null;

            foreach (PokemonRule rule in _rules)
            {
                if (rule.RuleID == ruleID)
                {
                    count++;
                    deleteRule = rule;
                }
            }

            if (count == 1)
            {
                _rules.Remove(deleteRule);
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IRuleAccessor"/> used for testing
        /// </summary>
        public int DeactivateRule(string ruleID)
        {
            int count = 0;

            foreach (PokemonRule rule in _rules)
            {
                if (rule.RuleID == ruleID)
                {
                    rule.Active = false;
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IRuleAccessor"/> used for testing
        /// </summary>
        public int ReactivateRule(string ruleID)
        {
            int count = 0;

            foreach (PokemonRule rule in _rules)
            {
                if (rule.RuleID == ruleID)
                {
                    rule.Active = true;
                    count++;
                }
            }

            return count;
        }
    }
}
