using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess
{
    public class AltArtAccessor : IAltArtAccessor
    {

        /// <summary>
        /// Implements from <see cref="IAltArtAccessor"/>. Access the database
        /// using sp_select_alternate_art_by_alternate_art_id
        /// </summary>
        public AlternateArt SelectAlternateArtByID(string alternateArtID)
        {
            AlternateArt resultAltArt = null;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_alternate_art_by_alternate_art_id";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AlternateArtID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@AlternateArtID"].Value = alternateArtID;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    resultAltArt = new AlternateArt()
                    {
                        AlternateArtID = reader.GetString(0),
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

            return resultAltArt;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtAccessor"/>. Access the database
        /// using sp_select_all_alternate_arts
        /// </summary>
        public List<AlternateArt> SelectAllAlternateArt()
        {
            List<AlternateArt> results = new List<AlternateArt>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_all_alternate_arts";
            SqlCommand cmd = new SqlCommand( cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(new AlternateArt()
                    {
                        AlternateArtID = reader.GetString(0),
                        Description= reader.GetString(1),
                        Active = reader.GetBoolean(2),
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
        /// Implements from <see cref="IAltArtAccessor"/>. Access the database
        /// using sp_insert_alternate_art
        /// </summary>
        public int InsertAlternateArt(AlternateArt alternateArt)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_alternate_art";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AlternateArtID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 250);

            cmd.Parameters["@AlternateArtID"].Value = alternateArt.AlternateArtID;
            cmd.Parameters["@Description"].Value = alternateArt.Description;

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
        /// Implements from <see cref="IAltArtAccessor"/>. Access the database
        /// using sp_update_alternate_art
        /// </summary>
        public int UpdateAlternateArt(AlternateArt alternateArt)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_update_alternate_art";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AlternateArtID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 250);

            cmd.Parameters["@AlternateArtID"].Value = alternateArt.AlternateArtID;
            cmd.Parameters["@Description"].Value = alternateArt.Description;

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
        /// Implements from <see cref="IAltArtAccessor"/>. Access the database
        /// using sp_delete_alternate_art
        /// </summary>
        public int DeleteAlternateArt(string alternateArtID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_delete_alternate_art";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AlternateArtID", SqlDbType.NVarChar, 50);

            cmd.Parameters["@AlternateArtID"].Value = alternateArtID;


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
        /// Implements from <see cref="IAltArtAccessor"/>. Access the database
        /// using sp_deactivate_alternate_art
        /// </summary>
        public int DeactivateAlternateArt(string alternateArtID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_deactivate_alternate_art";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AlternateArtID", SqlDbType.NVarChar, 30);
            cmd.Parameters["@AlternateArtID"].Value = alternateArtID;


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
        /// Implements from <see cref="IAltArtAccessor"/>. Access the database
        /// using sp_reactivate_alternate_art
        /// </summary>
        public int ReactivateAlternateArt(string alternateArtID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_reactivate_alternate_art";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AlternateArtID", SqlDbType.NVarChar, 30);
            cmd.Parameters["@AlternateArtID"].Value = alternateArtID;


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
