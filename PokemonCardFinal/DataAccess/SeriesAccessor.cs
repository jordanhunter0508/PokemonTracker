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
    public class SeriesAccessor : ISeriesAccessor
    {
        /// <summary>
        /// Implements from <see cref="ISeriesAccessor"/>. Access the database
        /// using sp_select_all_series
        /// </summary>
        public List<Series> SelectAllSeries()
        {
            List<Series> results = new List<Series>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_all_series";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new Series
                    {
                        SeriesID = reader.GetString(0),
                        BoosterCount = reader.GetInt32(1),
                        ReleaseDate = reader.GetDateTime(2),
                        ImagePath = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Active = reader.GetBoolean(4),
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
        /// Implements from <see cref="ISeriesAccessor"/>. Access the database
        /// using sp_select_series_image_paths
        /// </summary>
        public List<Series> SelectSeriesImagePaths()
        {
            List<Series> results = new List<Series>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_series_image_paths";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new Series
                    {
                        SeriesID = reader.GetString(0),
                        ImagePath = reader.IsDBNull(1) ? null : reader.GetString(1),
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
        /// Implements from <see cref="ISeriesAccessor"/>. Access the database
        /// using sp_activate_series
        /// </summary>
        public int ActivateSeries(string seriesID, bool active)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_activate_series";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Active", active);
            cmd.Parameters.Add("@SeriesID", SqlDbType.NVarChar, 100);
            cmd.Parameters["@SeriesID"].Value = seriesID;

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
        /// Implements from <see cref="ISeriesAccessor"/>. Access the database
        /// using sp_activate_booster_by_seriesid
        /// </summary>
        public ActivationResults ActivateBoostersBySeriesID(string seriesID, bool active)
        {
            ActivationResults results = new ActivationResults();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_activate_booster_by_seriesid";
            SqlCommand cmd = new SqlCommand(cmdText,conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Active", active);
            cmd.Parameters.Add("@SeriesID", SqlDbType.NVarChar, 100);
            cmd.Parameters["@SeriesID"].Value = seriesID;

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

        /// <summary>
        /// Implements from <see cref="ISeriesAccessor"/>. Access the database
        /// using sp_activate_card_by_seriesid
        /// </summary>
        public ActivationResults ActivateCardsBySeriesID(string seriesID, bool active)
        {
            ActivationResults results = new ActivationResults();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_activate_card_by_seriesid";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Active", active);
            cmd.Parameters.Add("@SeriesID", SqlDbType.NVarChar, 100);
            cmd.Parameters["@SeriesID"].Value = seriesID;

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
