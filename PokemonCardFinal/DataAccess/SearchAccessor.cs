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
        public List<Card> SelectCardsByName(string name)
        {
            List<Card> results = new List<Card>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_cards_by_card_name";
            SqlCommand cmd = new SqlCommand(cmdText,conn);
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
