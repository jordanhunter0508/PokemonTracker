using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess
{
    public class AbilityAccessor : IAbilityAccessor
    {
        /// <summary>
        /// Implements from <see cref="IAbilityAccessor"/>. Access the database
        /// using sp_select_ability_by_ability_id
        /// </summary>
        public Ability SelectAbilityByAbilityID(string abilityID)
        {
            Ability resultAbility = null;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_ability_by_ability_id";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@AbilityID", System.Data.SqlDbType.NVarChar, 30);
            cmd.Parameters["@AbilityID"].Value = abilityID;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    resultAbility = new Ability()
                    {
                        AbilityID = reader.GetString(0),
                        AbilityType = reader.GetString(1),
                        Description = reader.GetString(2)
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

            return resultAbility;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityAccessor"/>. Access the database
        /// using sp_select_abilities
        /// </summary>
        public List<Ability> SelectAbilities()
        {
            List<Ability> resultAbility = new List<Ability>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_abilities";
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
                        resultAbility.Add(new Ability()
                        {
                            AbilityID = reader.GetString(0),
                            AbilityType = reader.GetString(1),
                            Description = reader.GetString(2)
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

            return resultAbility;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityAccessor"/>. Access the database
        /// using sp_select_abilities_by_ability_type
        /// </summary>
        public List<Ability> SelectAbilitiesByAbilityType(string abilityType)
        {
            List<Ability> resultAbility = new List<Ability>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_abilities_by_ability_type";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@AbilityType", System.Data.SqlDbType.NVarChar, 25);
            cmd.Parameters["@AbilityType"].Value = abilityType;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        resultAbility.Add(new Ability()
                        {
                            AbilityID = reader.GetString(0),
                            AbilityType = reader.GetString(1),
                            Description = reader.GetString(2)
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

            return resultAbility;
        }

        /// <summary>
        /// Implements from <see cref="IAbilityAccessor"/>. Access the database
        /// using sp_insert_ability
        /// </summary>
        public int InsertAbility(Ability ability)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_ability";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@AbilityID", System.Data.SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@AbilityType", System.Data.SqlDbType.NVarChar, 25);
            cmd.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 650);

            cmd.Parameters["@AbilityID"].Value = ability.AbilityID;
            cmd.Parameters["@AbilityType"].Value = ability.AbilityType;
            cmd.Parameters["@Description"].Value = ability.Description;

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
        /// Implements from <see cref="IAbilityAccessor"/>. Access the database
        /// using sp_update_ability
        /// </summary>
        public int UpdateAbility(Ability ability)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_update_ability";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@AbilityID", System.Data.SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@AbilityType", System.Data.SqlDbType.NVarChar, 25);
            cmd.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 650);

            cmd.Parameters["@AbilityID"].Value = ability.AbilityID;
            cmd.Parameters["@AbilityType"].Value = ability.AbilityType;
            cmd.Parameters["@Description"].Value = ability.Description;

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
        /// Implements from <see cref="IAbilityAccessor"/>. Access the database
        /// using sp_delete_ability
        /// </summary>
        public int DeleteAbility(string abilityID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_delete_ability";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@AbilityID", System.Data.SqlDbType.NVarChar, 30);

            cmd.Parameters["@AbilityID"].Value = abilityID;


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
