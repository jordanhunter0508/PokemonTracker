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

namespace DataAccess
{
    public class CardAccessor : ICardAccessor
    {
        /// <summary>
        /// Implements from <see cref="ICardAccessor"/>. Access the database
        /// using sp_select_card_by_card_id
        /// </summary>
        public Card SelectCardByCardID(int cardID)
        {
            Card resultCard = null;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_card_by_card_id";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@PokemonCardID", System.Data.SqlDbType.Int);
            cmd.Parameters["@PokemonCardID"].Value = cardID;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    resultCard = new Card()
                    {
                        CardID = reader.GetInt32(0),
                        ArtistID = reader.GetInt32(1),
                        AbilityID = reader.GetString(2),
                        BoosterID = reader.GetString(3),
                        PokemonRuleID = reader.GetString(4),
                        ElementTypeID = reader.GetString(5),
                        Name = reader.GetString(6),
                        BoosterNumber = reader.GetInt32(7),
                        CardType = reader.GetString(8),
                        Rarity = reader.GetString(9),
                        WeaknessType = reader.GetString(10),
                        ResistanceType = reader.GetString(11),
                        WeaknessValue = reader.GetInt32(12),
                        ResistanceValue = reader.GetInt32(13),
                        RetreatCost = reader.GetInt32(14),
                        Health = reader.GetInt32(15),
                        Stage = reader.GetString(16),
                        ImagePath = reader.IsDBNull(17) ? null : reader.GetString(17)
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

            return resultCard;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/>. Access the database
        /// using sp_select_all_cards
        /// </summary>
        public List<Card> SelectAllCards()
        {
            List<Card> results = new List<Card>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_all_cards";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

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
        /// Implements from <see cref="ICardAccessor"/>. Access the database
        /// using sp_select_cards_paginated
        /// </summary>
        public PaginatedResult<Card> SelectCardsPaginated(FilterOption filterOption, int pageNumber = 1, int pageSize = 25)
        {
            PaginatedResult<Card> result = new PaginatedResult<Card>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_cards_paginated";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Filter Options
            if (filterOption != null)
            {
                if (!string.IsNullOrWhiteSpace(filterOption.BoosterID))
                {
                    cmd.Parameters.AddWithValue("@BoosterID", filterOption.BoosterID);
                }

                if (!string.IsNullOrWhiteSpace(filterOption.Rarity))
                {
                    cmd.Parameters.AddWithValue("@Rarity", filterOption.Rarity);
                }

                if (!string.IsNullOrWhiteSpace(filterOption.CardType))
                {
                    cmd.Parameters.AddWithValue("@CardType", filterOption.CardType);
                }

                if (!string.IsNullOrWhiteSpace(filterOption.ElementTypeID))
                {
                    cmd.Parameters.AddWithValue("@ElementTypeID", filterOption.ElementTypeID);
                }

                if (filterOption.ArtistID != 0)
                {
                    cmd.Parameters.AddWithValue("@ArtistID",filterOption.ArtistID);
                }
            }

            cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Items.Add(new Card()
                    {
                        CardID = reader.GetInt32(0),
                        BoosterID = reader.GetString(1),
                        ElementTypeID = reader.GetString(2),
                        Name = reader.GetString(3),
                        BoosterNumber = reader.GetInt32(4),
                        CardType = reader.GetString(5),
                        Rarity = reader.GetString(6),
                        ImagePath = reader.GetString(7),
                    });

                    result.TotalCount = reader.GetInt32(8);
                    result.PageNumber = reader.GetInt32(9);
                    result.PageSize = reader.GetInt32(10);
                    result.TotalPages = Convert.ToInt32(reader.GetDecimal(11));
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
        /// Implements from <see cref="ICardAccessor"/>. Access the database
        /// using sp_insert_card
        /// </summary>
        public int InsertCard(Card card)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_card";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@ArtistID", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@AbilityID", System.Data.SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@BoosterID", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PokemonRuleID", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@ElementTypeID", System.Data.SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@BoosterNumber", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@CardType", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Rarity", System.Data.SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@WeaknessType", System.Data.SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@ResistanceType", System.Data.SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@WeaknessValue", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@ResistanceValue", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@RetreatCost", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@Health", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@Stage", System.Data.SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@ImagePath", System.Data.SqlDbType.NVarChar, 250);

            cmd.Parameters["@ArtistID"].Value = card.ArtistID;
            cmd.Parameters["@AbilityID"].Value = card.AbilityID;
            cmd.Parameters["@BoosterID"].Value = card.BoosterID;
            cmd.Parameters["@PokemonRuleID"].Value = card.PokemonRuleID;
            cmd.Parameters["@ElementTypeID"].Value = card.ElementTypeID;
            cmd.Parameters["@Name"].Value = card.Name;
            cmd.Parameters["@BoosterNumber"].Value = card.BoosterNumber;
            cmd.Parameters["@CardType"].Value = card.CardType;
            cmd.Parameters["@Rarity"].Value = card.Rarity;
            cmd.Parameters["@WeaknessType"].Value = card.WeaknessType;
            cmd.Parameters["@ResistanceType"].Value = card.ResistanceType;
            cmd.Parameters["@WeaknessValue"].Value = card.WeaknessValue;
            cmd.Parameters["@ResistanceValue"].Value = card.ResistanceValue;
            cmd.Parameters["@RetreatCost"].Value = card.RetreatCost;
            cmd.Parameters["@Health"].Value = card.Health;
            cmd.Parameters["@Stage"].Value = card.Stage;

            if (!String.IsNullOrWhiteSpace(card.ImagePath))
            {
                cmd.Parameters["@ImagePath"].Value = card.ImagePath;
            }

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    count = Convert.ToInt32(reader.GetDecimal(0));
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

            return count;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/>. Access the database
        /// using sp_update_card
        /// </summary>
        public int UpdateCard(Card card)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_update_card";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@PokemonCardID", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@ArtistID", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@AbilityID", System.Data.SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@BoosterID", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PokemonRuleID", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@ElementTypeID", System.Data.SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@BoosterNumber", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@CardType", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Rarity", System.Data.SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@WeaknessType", System.Data.SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@ResistanceType", System.Data.SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@WeaknessValue", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@ResistanceValue", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@RetreatCost", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@Health", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@Stage", System.Data.SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@ImagePath", System.Data.SqlDbType.NVarChar, 250);

            cmd.Parameters["@PokemonCardID"].Value = card.CardID;
            cmd.Parameters["@ArtistID"].Value = card.ArtistID;
            cmd.Parameters["@AbilityID"].Value = card.AbilityID;
            cmd.Parameters["@BoosterID"].Value = card.BoosterID;
            cmd.Parameters["@PokemonRuleID"].Value = card.PokemonRuleID;
            cmd.Parameters["@ElementTypeID"].Value = card.ElementTypeID;
            cmd.Parameters["@Name"].Value = card.Name;
            cmd.Parameters["@BoosterNumber"].Value = card.BoosterNumber;
            cmd.Parameters["@CardType"].Value = card.CardType;
            cmd.Parameters["@Rarity"].Value = card.Rarity;
            cmd.Parameters["@WeaknessType"].Value = card.WeaknessType;
            cmd.Parameters["@ResistanceType"].Value = card.ResistanceType;
            cmd.Parameters["@WeaknessValue"].Value = card.WeaknessValue;
            cmd.Parameters["@ResistanceValue"].Value = card.ResistanceValue;
            cmd.Parameters["@RetreatCost"].Value = card.RetreatCost;
            cmd.Parameters["@Health"].Value = card.Health;
            cmd.Parameters["@Stage"].Value = card.Stage;

            if (!String.IsNullOrWhiteSpace(card.ImagePath))
            {
                cmd.Parameters["@ImagePath"].Value = card.ImagePath;
            }

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
        /// Implements from <see cref="ICardAccessor"/>. Access the database
        /// using sp_delete_card
        /// </summary>
        public int DeleteCard(int cardID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_delete_card";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@PokemonCardID", System.Data.SqlDbType.Int);
            cmd.Parameters["@PokemonCardID"].Value = cardID;

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
