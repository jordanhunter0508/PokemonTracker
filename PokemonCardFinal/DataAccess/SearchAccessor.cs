using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class SearchAccessor : ISearchAccessor
    {
        /// <summary>
        /// Implements from <see cref="ISearchAccessor"/>. Access the database
        /// using sp_select_cards_by_card_name
        /// </summary>
        [Obsolete(message: "Use GetCards(FilterOption.CardName) instead.", false)]
        public List<Card> SelectCardsByName(string name)
        {
            List<Card> results = new List<Card>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_cards_by_card_name";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", name);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(new Card()
                    {
                        CardID = reader.GetInt32(0),
                        BoosterID = reader.GetString(1),
                        ElementTypeID = reader.GetString(2),
                        Name = reader.GetString(3),
                        BoosterNumber = reader.GetInt32(4),
                        CardType = reader.GetString(5),
                        Rarity = reader.GetString(6),
                        ImagePath = reader.IsDBNull(7) ? null : reader.GetString(7)
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
        /// Implements from <see cref="ISearchAccessor"/>. Access the database
        /// using sp_select_cards_by_filter
        /// </summary>
        public List<Card> SelectCards(FilterOption filterOption)
        {
            List<Card> results = new List<Card>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_cards_by_filter";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (filterOption == null)
            {
                throw new ArgumentNullException("Filter option must not be null.");
            }

            // Filter Options
            if (!string.IsNullOrWhiteSpace(filterOption.CardName))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
                cmd.Parameters["@Name"].Value = filterOption.CardName;
            }

            if (!string.IsNullOrWhiteSpace(filterOption.BoosterID))
            {
                cmd.Parameters.Add("@BoosterID", SqlDbType.NVarChar, 50);
                cmd.Parameters["@BoosterID"].Value = filterOption.BoosterID;
            }

            if (!string.IsNullOrWhiteSpace(filterOption.Rarity))
            {
                cmd.Parameters.Add("@Rarity", SqlDbType.NVarChar, 30);
                cmd.Parameters["@Rarity"].Value = filterOption.Rarity;
            }

            if (!string.IsNullOrWhiteSpace(filterOption.CardType))
            {
                cmd.Parameters.Add("@CardType", SqlDbType.NVarChar, 50);
                cmd.Parameters["@CardType"].Value = filterOption.CardType;
            }

            if (!string.IsNullOrWhiteSpace(filterOption.ElementTypeID))
            {
                cmd.Parameters.Add("@ElementTypeID", SqlDbType.NVarChar, 15);
                cmd.Parameters["@ElementTypeID"].Value = filterOption.ElementTypeID;
            }

            if (filterOption.ArtistID != 0)
            {
                cmd.Parameters.AddWithValue("@ArtistID", filterOption.ArtistID);
            }

            // If no filter options were added to the command
            // throw an error to prevent the return of all cards
            if (cmd.Parameters.Count <= 0)
            {
                throw new ArgumentException("Filter option must be specified to use search manager's get cards.");
            }

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(new Card()
                    {
                        CardID = reader.GetInt32(0),
                        BoosterID = reader.GetString(1),
                        ElementTypeID = reader.GetString(2),
                        Name = reader.GetString(3),
                        BoosterNumber = reader.GetInt32(4),
                        CardType = reader.GetString(5),
                        Rarity = reader.GetString(6),
                        ImagePath = reader.IsDBNull(7) ? null : reader.GetString(7),
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
    }
}
