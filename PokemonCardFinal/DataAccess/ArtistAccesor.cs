using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class ArtistAccesor : IArtistAccessor
    {
        /// <summary>
        /// Implements from <see cref="IArtistAccessor"/>. Access the database
        /// using sp_select_artist_by_artistid
        /// </summary>
        public Artist SelectArtistByArtistID(int artistID)
        {
            Artist resultArtist = null;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_artist_by_artistid";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
            cmd.Parameters.Add("@ArtistID",System.Data.SqlDbType.Int);
            cmd.Parameters["@ArtistID"].Value = artistID;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    resultArtist = new Artist()
                    {
                        ArtistID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        Surname = reader.GetString(2)
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

            return resultArtist;
        }

        /// <summary>
        /// Implements from <see cref="IArtistAccessor"/>. Access the database
        /// using sp_select_artist_by_name
        /// </summary>
        public Artist SelectArtistByArtistName(string givenName, string surname)
        {
            Artist resultArtist = null;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_artist_by_name";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@GivenName", System.Data.SqlDbType.NVarChar,50);
            cmd.Parameters.Add("@Surname", System.Data.SqlDbType.NVarChar,100);
            cmd.Parameters["@GivenName"].Value = givenName;
            cmd.Parameters["@Surname"].Value = surname;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    resultArtist = new Artist()
                    {
                        ArtistID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        Surname = reader.GetString(2)
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

            return resultArtist;
        }

        /// <summary>
        /// Implements from <see cref="IArtistAccessor"/>. Access the database
        /// using sp_select_artists
        /// </summary>
        public List<Artist> SelectArtists()
        {
            List<Artist> results = new List<Artist>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_artists";
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
                        results.Add(new Artist()
                        {
                            ArtistID = reader.GetInt32(0),
                            GivenName = reader.GetString(1),
                            Surname = reader.GetString(2)
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
        /// Implements from <see cref="IArtistAccessor"/>. Access the database
        /// using sp_insert_artist
        /// </summary>
        public int InsertArtist(string givenName, string surname)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_artist";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@GivenName", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Surname", System.Data.SqlDbType.NVarChar, 100);
            cmd.Parameters["@GivenName"].Value = givenName;
            cmd.Parameters["@Surname"].Value = surname;

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
        /// Implements from <see cref="IArtistAccessor"/>. Access the database
        /// using sp_update_artist_by_artistid
        /// </summary>
        public int UpdateArtistByArtistID(int artistID, string givenName, string surname)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_update_artist_by_artistid";
            SqlCommand cmd = new SqlCommand(cmdText,conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@ArtistID",System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@GivenName",System.Data.SqlDbType.NVarChar,50);
            cmd.Parameters.Add("@Surname",System.Data.SqlDbType.NVarChar,100);
            cmd.Parameters["@ArtistID"].Value = artistID;
            cmd.Parameters["@GivenName"].Value = givenName;
            cmd.Parameters["@Surname"].Value = surname;

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
        /// Implements from <see cref="IArtistAccessor"/>. Access the database
        /// using sp_delete_artist_by_artistid
        /// </summary>
        public int DeleteArtistByArtistID(int artistID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_delete_artist_by_artistid";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@ArtistID", System.Data.SqlDbType.Int);

            cmd.Parameters["@ArtistID"].Value = artistID;


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
