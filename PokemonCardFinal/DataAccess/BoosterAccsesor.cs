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
    public class BoosterAccsesor : IBoosterAccessor
    {
        /// <summary>
        /// Implements from <see cref="IBoosterAccessor"/>. Access the database
        /// using sp_select_booster_by_boosterid
        /// </summary>
        public Booster SelectBoosterByBoosterID(string boosterID)
        {
            Booster result = null;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_booster_by_boosterid";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BoosterID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@BoosterID"].Value = boosterID;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    result = new Booster()
                    {
                        BoosterID = reader.GetString(0),
                        SeriesID = reader.GetString(1),
                        ReleaseDate = reader.GetDateTime(2),
                        Abbreviation = reader.GetString(3),
                        BaseCount = reader.GetInt32(4),
                        SecretCount = reader.GetInt32(5),
                        LogoPath = reader.IsDBNull(7) ? null : reader.GetString(7),
                        SymbolPath = reader.IsDBNull(8) ? null : reader.GetString(8),
                        Active = reader.GetBoolean(9),
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
        /// Implements from <see cref="IBoosterAccessor"/>. Access the database
        /// using sp_select_boosters
        /// </summary>
        public List<Booster> SelectBoosters()
        {
            List<Booster> results = new List<Booster>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_boosters";
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
                        results.Add(new Booster()
                        {
                            BoosterID = reader.GetString(0),
                            SeriesID = reader.GetString(1),
                            ReleaseDate = reader.GetDateTime(2),
                            Abbreviation = reader.GetString(3),
                            BaseCount = reader.GetInt32(4),
                            SecretCount = reader.GetInt32(5),
                            LogoPath = reader.IsDBNull(7) ? null : reader.GetString(7),
                            SymbolPath = reader.IsDBNull(8) ? null : reader.GetString(8),
                            Active = reader.GetBoolean(9),
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
        /// Implements from <see cref="IBoosterAccessor"/>. Access the database
        /// using sp_select_active_boosters
        /// </summary>
        public List<Booster> SelectActiveBoosters()
        {
            List<Booster> results = new List<Booster>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_active_boosters";
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
                        results.Add(new Booster()
                        {
                            BoosterID = reader.GetString(0),
                            SeriesID = reader.GetString(1),
                            ReleaseDate = reader.GetDateTime(2),
                            Abbreviation = reader.GetString(3),
                            BaseCount = reader.GetInt32(4),
                            SecretCount = reader.GetInt32(5),
                            LogoPath = reader.IsDBNull(7) ? null : reader.GetString(7),
                            SymbolPath = reader.IsDBNull(8) ? null : reader.GetString(8),
                            Active = reader.GetBoolean(9),
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
        /// Implements from <see cref="IBoosterAccessor"/>. Access the database
        /// using sp_select_boosterids
        /// </summary>
        public List<string> SelectBoosterIDs()
        {
            List<string> results = new List<string>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_boosterids";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(reader.GetString(0));
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
        /// Implements from <see cref="IBoosterAccessor"/>. Access the database
        /// using sp_select_active_boosterids
        /// </summary>
        public List<string> SelectActiveBoosterIDs()
        {
            List<string> results = new List<string>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_active_boosterids";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(reader.GetString(0));
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
        /// Implements from <see cref="IBoosterAccessor"/>. Access the database
        /// using sp_insert_booster
        /// </summary>
        public int InsertBooster(Booster booster)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_booster";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ReleaseDate", booster.ReleaseDate);
            cmd.Parameters.AddWithValue("@BaseCount", booster.BaseCount);
            cmd.Parameters.AddWithValue("@SecretCount", booster.SecretCount);

            cmd.Parameters.Add("@BoosterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@SeriesID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Abbreviation", SqlDbType.NVarChar, 5);
            cmd.Parameters.Add("@LogoPath", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@SymbolPath", SqlDbType.NVarChar, 250);

            cmd.Parameters["@BoosterID"].Value = booster.BoosterID;
            cmd.Parameters["@SeriesID"].Value = booster.SeriesID;
            cmd.Parameters["@Abbreviation"].Value = booster.Abbreviation;
            cmd.Parameters["@LogoPath"].Value = booster.LogoPath == null ? DBNull.Value : booster.LogoPath;
            cmd.Parameters["@SymbolPath"].Value = booster.SymbolPath == null ? DBNull.Value : booster.SymbolPath;

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
        /// Implements from <see cref="IBoosterAccessor"/>. Access the database
        /// using sp_update_booster
        /// </summary>
        public int UpdateBooster(Booster booster)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_update_booster";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ReleaseDate", booster.ReleaseDate);
            cmd.Parameters.AddWithValue("@BaseCount", booster.BaseCount);
            cmd.Parameters.AddWithValue("@SecretCount", booster.SecretCount);

            cmd.Parameters.Add("@BoosterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@SeriesID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Abbreviation", SqlDbType.NVarChar, 5);
            cmd.Parameters.Add("@LogoPath", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@SymbolPath", SqlDbType.NVarChar, 250);

            cmd.Parameters["@BoosterID"].Value = booster.BoosterID;
            cmd.Parameters["@SeriesID"].Value = booster.SeriesID;
            cmd.Parameters["@Abbreviation"].Value = booster.Abbreviation;
            cmd.Parameters["@LogoPath"].Value = booster.LogoPath == null ? DBNull.Value : booster.LogoPath;
            cmd.Parameters["@SymbolPath"].Value = booster.SymbolPath == null ? DBNull.Value : booster.SymbolPath;

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
        /// Implements from <see cref="IBoosterAccessor"/>. Access the database
        /// using sp_delete_booster
        /// </summary>
        public int DeleteBooster(string boosterID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_delete_booster";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BoosterID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@BoosterID"].Value = boosterID;

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
        /// Implements from <see cref="IBoosterAccessor"/>. Access the database
        /// using sp_activate_booster
        /// </summary>
        public int ActivateBooster(string boosterID, bool active)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_activate_booster";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Active", active);
            cmd.Parameters.Add("@BoosterID", SqlDbType.NVarChar,50);
            cmd.Parameters["@BoosterID"].Value = boosterID;

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
        /// Implements from <see cref="IBoosterAccessor"/>. Access the database
        /// using sp_activate_card_by_boosterid
        /// </summary>
        public ActivationResults ActivateCardsByBoosterID(string boosterID, bool active)
        {
            ActivationResults results = new ActivationResults();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_activate_card_by_boosterid";
            SqlCommand cmd = new SqlCommand(cmdText,conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Active", active);
            cmd.Parameters.Add("@BoosterID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@BoosterID"].Value = boosterID;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    results.ExpectedCount = reader.GetInt32(0);
                    results.UpdatedCount = reader.GetInt32(1);
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
    }
}
