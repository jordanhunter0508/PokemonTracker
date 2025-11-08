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
                Description = "This is a test."
            });
            _rules.Add(new PokemonRule()
            {
                RuleID = "Test Rule 2",
                Description = "This is not a test."
            });
            _rules.Add(new PokemonRule()
            {
                RuleID = "Test Rule 3",
                Description = "This is really not a test."
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
        public List<PokemonRule> SelectRules()
        {
            List<PokemonRule> results = null;
            results = _rules;
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
    }
}
