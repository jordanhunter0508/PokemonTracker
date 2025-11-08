using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class RuleAccessor : IRuleAccessor
    {
        /// <summary>
        /// Implements from <see cref="IRuleAccessor"/>. Access the database
        /// using sp_select_rule_by_rule_id
        /// </summary>
        public PokemonRule SelectRuleByRuleID(string ruleID)
        {
            PokemonRule result = null;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_rule_by_rule_id";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@PokemonRuleID", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters["@PokemonRuleID"].Value = ruleID;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows) 
                {
                    reader.Read();

                    result = new PokemonRule() 
                    {
                        RuleID = reader.GetString(0),
                        Description = reader.GetString(1)
                    };
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            { 
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IRuleAccessor"/>. Access the database 
        /// using sp_select_rules
        /// </summary>
        public List<PokemonRule> SelectRules()
        {
            List<PokemonRule> results = new List<PokemonRule>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_rules";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        results.Add(new PokemonRule()
                        {
                            RuleID = reader.GetString(0),
                            Description = reader.GetString(1)
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                conn.Close();
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IRuleAccessor"/>. Access the database 
        /// using sp_insert_rule
        /// </summary>
        public int InsertRule(PokemonRule rule)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_rule";
            SqlCommand cmd = new SqlCommand( cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@PokemonRuleID", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 150);

            cmd.Parameters["@PokemonRuleID"].Value = rule.RuleID;
            cmd.Parameters["@Description"].Value = rule.Description;

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IRuleAccessor"/>. Access the database 
        /// using sp_update_rule
        /// </summary>
        public int UpdateRule(PokemonRule rule)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_update_rule";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@PokemonRuleID", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 150);

            cmd.Parameters["@PokemonRuleID"].Value = rule.RuleID;
            cmd.Parameters["@Description"].Value = rule.Description;

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IRuleAccessor "/>. Access the database
        /// using sp_delete_rule
        /// </summary>
        public int DeleteRule(string ruleID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_delete_rule";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@PokemonRuleID", System.Data.SqlDbType.NVarChar, 50);

            cmd.Parameters["@PokemonRuleID"].Value = ruleID;


            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return count;
        }
    }
}
