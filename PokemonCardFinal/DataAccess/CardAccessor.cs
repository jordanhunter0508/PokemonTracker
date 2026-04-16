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
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PokemonCardID", cardID);

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
                        ImagePath = reader.IsDBNull(17) ? null : reader.GetString(17),
                        Active = reader.GetBoolean(18)
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
                        ImagePath = reader.IsDBNull(7) ? null : reader.GetString(7),
                        Active = reader.GetBoolean(8)
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
                    cmd.Parameters.Add("@BoosterID", SqlDbType.NVarChar,50);
                    cmd.Parameters["@BoosterID"].Value = filterOption.BoosterID;
                }

                if (!string.IsNullOrWhiteSpace(filterOption.Rarity))
                {
                    cmd.Parameters.Add("@Rarity", SqlDbType.NVarChar,30);
                    cmd.Parameters["@Rarity"].Value = filterOption.Rarity;
                }

                if (!string.IsNullOrWhiteSpace(filterOption.CardType))
                {
                    cmd.Parameters.Add("@CardType", SqlDbType.NVarChar,50);
                    cmd.Parameters["@CardType"].Value = filterOption.CardType;
                }

                if (!string.IsNullOrWhiteSpace(filterOption.ElementTypeID))
                {
                    cmd.Parameters.Add("@ElementTypeID", SqlDbType.NVarChar,15);
                    cmd.Parameters["@ElementTypeID"].Value = filterOption.ElementTypeID;
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
                        ImagePath = reader.IsDBNull(7) ? null : reader.GetString(7),
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
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ArtistID", card.ArtistID);
            cmd.Parameters.AddWithValue("@BoosterNumber", card.BoosterNumber);
            cmd.Parameters.AddWithValue("@WeaknessValue", card.WeaknessValue);
            cmd.Parameters.AddWithValue("@ResistanceValue", card.ResistanceValue);
            cmd.Parameters.AddWithValue("@RetreatCost", card.RetreatCost);
            cmd.Parameters.AddWithValue("@Health", card.Health);

            cmd.Parameters.Add("@AbilityID", SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@BoosterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PokemonRuleID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@ElementTypeID", SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@CardType", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Rarity", SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@WeaknessType", SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@ResistanceType", SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@Stage", SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@ImagePath", SqlDbType.NVarChar, 250);

            cmd.Parameters["@AbilityID"].Value = card.AbilityID;
            cmd.Parameters["@BoosterID"].Value = card.BoosterID;
            cmd.Parameters["@PokemonRuleID"].Value = card.PokemonRuleID;
            cmd.Parameters["@ElementTypeID"].Value = card.ElementTypeID;
            cmd.Parameters["@Name"].Value = card.Name;
            cmd.Parameters["@CardType"].Value = card.CardType;
            cmd.Parameters["@Rarity"].Value = card.Rarity;
            cmd.Parameters["@WeaknessType"].Value = card.WeaknessType;
            cmd.Parameters["@ResistanceType"].Value = card.ResistanceType;
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
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PokemonCardID", card.CardID);
            cmd.Parameters.AddWithValue("@ArtistID", card.ArtistID);
            cmd.Parameters.AddWithValue("@BoosterNumber", card.BoosterNumber);
            cmd.Parameters.AddWithValue("@WeaknessValue", card.WeaknessValue);
            cmd.Parameters.AddWithValue("@ResistanceValue", card.ResistanceValue);
            cmd.Parameters.AddWithValue("@RetreatCost", card.RetreatCost);
            cmd.Parameters.AddWithValue("@Health", card.Health);
            cmd.Parameters.AddWithValue("@Active", card.Active);

            cmd.Parameters.Add("@AbilityID", SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@BoosterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PokemonRuleID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@ElementTypeID", SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@CardType", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Rarity", SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@WeaknessType", SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@ResistanceType", SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@Stage", SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@ImagePath", SqlDbType.NVarChar, 250);

            cmd.Parameters["@AbilityID"].Value = card.AbilityID;
            cmd.Parameters["@BoosterID"].Value = card.BoosterID;
            cmd.Parameters["@PokemonRuleID"].Value = card.PokemonRuleID;
            cmd.Parameters["@ElementTypeID"].Value = card.ElementTypeID;
            cmd.Parameters["@Name"].Value = card.Name;
            cmd.Parameters["@CardType"].Value = card.CardType;
            cmd.Parameters["@Rarity"].Value = card.Rarity;
            cmd.Parameters["@WeaknessType"].Value = card.WeaknessType;
            cmd.Parameters["@ResistanceType"].Value = card.ResistanceType;
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
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PokemonCardID", cardID);

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
        /// using sp_deactivate_card
        /// </summary>
        public int DeactivateCard(int cardID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_deactivate_card";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PokemonCardID", cardID);

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
        /// using sp_reactivate_card
        /// </summary>
        public int ReactivateCard(int cardID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_reactivate_card";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PokemonCardID", cardID);

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
