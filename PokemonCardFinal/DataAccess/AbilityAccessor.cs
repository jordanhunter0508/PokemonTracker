using System.Data;
using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;

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
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AbilityID", SqlDbType.NVarChar, 30);
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
                        Description = reader.GetString(2),
                        Active = reader.GetBoolean(3),
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
        /// using sp_select_all_abilities
        /// </summary>
        public List<AbilityVM> SelectAllAbilities()
        {
            List<AbilityVM> results = new List<AbilityVM>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_all_abilities";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new AbilityVM()
                    {
                        AbilityID = reader.GetString(0),
                        AbilityType = reader.GetString(1),
                        Description = reader.GetString(2),
                        Active = reader.GetBoolean(3),
                    });

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
        /// Implements from <see cref="IAbilityAccessor"/>. Access the database
        /// using sp_insert_ability
        /// </summary>
        public int InsertAbility(Ability ability)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_ability";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AbilityID", SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@AbilityType", SqlDbType.NVarChar, 25);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 650);

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
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AbilityID", SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@AbilityType", SqlDbType.NVarChar, 25);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 650);

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
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AbilityID", SqlDbType.NVarChar, 30);

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

        /// <summary>
        /// Implements from <see cref="IAbilityAccessor"/>. Access the database
        /// using sp_deactivate_ability
        /// </summary>
        public int DeactivateAbility(string abilityID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_deactivate_ability";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AbilityID", SqlDbType.NVarChar, 30);

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

        /// <summary>
        /// Implements from <see cref="IAbilityAccessor"/>. Access the database
        /// using sp_reactivate_ability
        /// </summary>
        public int ReactivateAbility(string abilityID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_reactivate_ability";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AbilityID", SqlDbType.NVarChar, 30);

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
