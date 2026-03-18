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
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PokemonRuleID", SqlDbType.NVarChar, 50);
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
                        Description = reader.GetString(1),
                        Active = reader.GetBoolean(2),
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
        public List<PokemonRule> SelectAllRules()
        {
            List<PokemonRule> results = new List<PokemonRule>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_rules";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

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
                            Description = reader.GetString(1),
                            Active = reader.GetBoolean(2),
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
        /// Implements from <see cref="IRuleAccessor "/>. Access the database
        /// using sp_select_rule_active_paginated
        /// </summary>
        public PaginatedResult<PokemonRule> SelectActiveRules(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<PokemonRule> results = new PaginatedResult<PokemonRule>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_rule_active_paginated";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Items.Add(new PokemonRule()
                    {
                        RuleID = reader.GetString(0),
                        Description = reader.GetString(1),
                        Active = reader.GetBoolean(2),
                    });

                    results.TotalCount = reader.GetInt32(3);
                    results.PageNumber = reader.GetInt32(4);
                    results.PageSize = reader.GetInt32(5);
                    results.TotalPages = Convert.ToInt32(reader.GetDecimal(6));
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
        /// Implements from <see cref="IRuleAccessor "/>. Access the database
        /// using sp_select_rule_deactive_paginated
        /// </summary>
        public PaginatedResult<PokemonRule> SelectDeactiveRules(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<PokemonRule> results = new PaginatedResult<PokemonRule>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_rule_deactive_paginated";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Items.Add(new PokemonRule()
                    {
                        RuleID = reader.GetString(0),
                        Description = reader.GetString(1),
                        Active = reader.GetBoolean(2),
                    });

                    results.TotalCount = reader.GetInt32(3);
                    results.PageNumber = reader.GetInt32(4);
                    results.PageSize = reader.GetInt32(5);
                    results.TotalPages = Convert.ToInt32(reader.GetDecimal(6));
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
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PokemonRuleID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 150);

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
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PokemonRuleID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 150);

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
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PokemonRuleID", SqlDbType.NVarChar, 50);

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

        /// <summary>
        /// Implements from <see cref="IRuleAccessor "/>. Access the database
        /// using sp_deactivate_rule
        /// </summary>
        public int DeactivateRule(string ruleID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_deactivate_rule";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PokemonRuleID", ruleID);

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
        /// using sp_reactivate_rule
        /// </summary>
        public int ReactivateRule(string ruleID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_reactivate_rule";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PokemonRuleID", ruleID);

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
