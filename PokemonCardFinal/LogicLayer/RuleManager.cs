using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccessInterfaces;
using DataDomain;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class RuleManager : IRuleManager
    {
        IRuleAccessor _ruleAccessor;

        /// <summary>
        /// General RuleManager created for the presentaion layer
        /// </summary>
        public RuleManager()
        {
            _ruleAccessor = new RuleAccessor();
        }

        /// <summary>
        /// Used for testing to pass in fake data
        /// </summary>
        /// <param name="ruleAccessor">Set the IRuleAccessor in the RuleManager</param>
        public RuleManager(IRuleAccessor ruleAccessor)
        {
            _ruleAccessor = ruleAccessor;
        }

        /// <summary>
        /// Implements from <see cref="IRuleManager"/>
        /// </summary>
        public PokemonRule GetRuleByRuleID(string ruleID)
        {
            PokemonRule result = null;

            try
            {
                result = _ruleAccessor.SelectRuleByRuleID(ruleID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Faild to get a pokemon role.");
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IRuleManager"/>
        /// </summary>
        public List<PokemonRule> GetAllRules()
        {
            List<PokemonRule> results = null;

            try
            {
                results = _ruleAccessor.SelectAllRules();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Faild to get a list of pokemon roles.");
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IRuleManager"/>
        /// </summary>
        public PaginatedResult<PokemonRule> GetActiveRules(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<PokemonRule> results = new PaginatedResult<PokemonRule>();

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
                results = _ruleAccessor.SelectActiveRules(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve a list of active pokemon rules.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IRuleManager"/>
        /// </summary>
        public PaginatedResult<PokemonRule> GetDeactiveRules(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<PokemonRule> results = new PaginatedResult<PokemonRule>();

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
                results = _ruleAccessor.SelectDeactiveRules(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve a list of deactive pokemon rules.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IRuleManager"/>
        /// </summary>
        public bool AddRule(PokemonRule rule)
        {
            bool result = false;

            if (rule == null)
            {
                throw new ArgumentNullException("Rule is empty.");
            }

            try
            {
                result = (1 == _ruleAccessor.InsertRule(rule));
            }
            catch (Exception)
            {

                throw new ApplicationException("Failed to add a rule to the database.\n" + 
                    "Please make sure the rule was not already created.");
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IRuleManager"/>
        /// </summary>
        public bool EditRule(PokemonRule rule)
        {
            bool result = false;

            if (rule == null)
            {
                throw new ArgumentNullException("Rule is empty.");
            }

            try
            {
                result = (1 == _ruleAccessor.UpdateRule(rule));
            }
            catch (Exception)
            {

                throw new ApplicationException("Failed to update the rule in the database.\n" +
                    "Please make sure the rule name was correct.");
            }
            return result;
        }

        /// <summary>
        /// Implements from <see cref="IRuleManager"/>
        /// </summary>
        public bool DeleteRule(string ruleID)
        {
            bool result = false;

            try
            {
                result = (1 == _ruleAccessor.DeleteRule(ruleID));
            }
            catch (Exception)
            {

                throw new ApplicationException("Failed to delete the rule in the database.\n" +
                    "Please make sure the rule is not attached to any cards.");
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IRuleManager"/>
        /// </summary>
        public bool DeactivateRule(string ruleID)
        {
            bool result = false;

            try
            {
                result = (1 == _ruleAccessor.DeactivateRule(ruleID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to deactivate the pokemon rule.",ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IRuleManager"/>
        /// </summary>
        public bool ReactivateRule(string ruleID)
        {
            bool result = false;

            try
            {
                result = (1 == _ruleAccessor.ReactivateRule(ruleID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to reactivate the pokemon rule.", ex);
            }

            return result;
        }
    }
}
