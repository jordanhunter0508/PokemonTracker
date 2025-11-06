using System;
using System.Collections.Generic;
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
        /// Implements from <see cref="IArtistAccessor"/>. Access the database
        /// using sp_select_booster_by_boosterid
        /// </summary>
        public Booster SelectBoosterByBoosterID(string boosterID)
        {
            Booster result = null;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_booster_by_boosterid";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@BoosterID", System.Data.SqlDbType.NVarChar,50);
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
                        Series = reader.GetString(1),
                        ReleaseDate = reader.GetDateTime(2),
                        Abbreviation = reader.GetString(3),
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
        /// Implements from <see cref="IArtistAccessor"/>. Access the database
        /// using sp_select_boosters
        /// </summary>
        public List<Booster> SelectBoosters()
        {
            List<Booster> results = new List<Booster>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_boosters";
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
                        results.Add(new Booster()
                        {
                            BoosterID = reader.GetString(0),
                            Series = reader.GetString(1),
                            ReleaseDate = reader.GetDateTime(2),
                            Abbreviation = reader.GetString(3),
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
        /// using sp_insert_booster
        /// </summary>
        public int InsertBooster(Booster booster)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implements from <see cref="IArtistAccessor"/>. Access the database
        /// using sp_update_booster
        /// </summary>
        public int UpdateBooster(Booster booster)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implements from <see cref="IArtistAccessor"/>. Access the database
        /// using sp_delete_booster
        /// </summary>
        public int DeleteBooster(string boosterID)
        {
            throw new NotImplementedException();
        }

    }
}
