using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ArtistID", artistID);

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
                        Surname = reader.GetString(2),
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
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@GivenName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 100);

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
                        Surname = reader.GetString(2),
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

            return resultArtist;
        }

        /// <summary>
        /// Implements from <see cref="IArtistAccessor"/>. Access the database
        /// using sp_select_all_artists
        /// </summary>
        public List<Artist> SelectAllArtists()
        {
            List<Artist> results = new List<Artist>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_all_artists";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new Artist()
                    {
                        ArtistID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        Surname = reader.GetString(2),
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
        /// Implements from <see cref="IArtistAccessor"/>. Access the database
        /// using sp_select_artists_active_paginated
        /// </summary>
        public PaginatedResult<Artist> SelectActiveArtists(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<Artist> results = new PaginatedResult<Artist>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_artists_active_paginated";
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
                    results.Items.Add(new Artist()
                    {
                        ArtistID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        Surname = reader.GetString(2),
                        Active = reader.GetBoolean(3),
                    });

                    results.TotalCount = reader.GetInt32(4);
                    results.PageNumber = reader.GetInt32(5);
                    results.PageSize = reader.GetInt32(6);
                    results.TotalPages = Convert.ToInt32(reader.GetDecimal(7));
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
        /// using sp_select_artists_deactive_paginated
        /// </summary>
        public PaginatedResult<Artist> SelectDeactiveArtists(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<Artist> results = new PaginatedResult<Artist>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_artists_deactive_paginated";
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
                    results.Items.Add(new Artist()
                    {
                        ArtistID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        Surname = reader.GetString(2),
                        Active = reader.GetBoolean(3),
                    });

                    results.TotalCount = reader.GetInt32(4);
                    results.PageNumber = reader.GetInt32(5);
                    results.PageSize = reader.GetInt32(6);
                    results.TotalPages = Convert.ToInt32(reader.GetDecimal(7));
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
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@GivenName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 100);

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
        /// using sp_update_artist
        /// </summary>
        public int UpdateArtist(int artistID, string givenName, string surname)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_update_artist";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ArtistID", artistID);

            cmd.Parameters.Add("@GivenName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 100);

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
        /// using sp_delete_artist
        /// </summary>
        public int DeleteArtist(int artistID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_delete_artist";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ArtistID", artistID);


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
        /// using sp_deactivate_artist
        /// </summary>
        public int DeactivateArtist(int artistID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_deactivate_artist";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ArtistID", artistID);


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
        /// using sp_reactivate_artist
        /// </summary>
        public int ReactivateArtist(int artistID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_reactivate_artist";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ArtistID", artistID);

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
