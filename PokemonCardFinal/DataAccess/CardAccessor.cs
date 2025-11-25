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
                        Stage = reader.GetString(16)
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
        /// using sp_select_moves_by_card_id
        /// </summary>
        public List<MoveVM> SelectMovesByCardID(int cardID)
        {
            Dictionary<int, MoveVM> results = new Dictionary<int, MoveVM>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_moves_by_card_id";
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
                    while (reader.Read())
                    {
                        StoreMoveVMInDictionary(results, reader);
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

            return results.Values.ToList();
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/>. Access the database
        /// using sp_select_alternate_arts_by_card_id
        /// </summary>
        public List<string> SelectAlternateArtsByCardID(int cardID)
        {
            List<string> results = new List<string>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_alternate_arts_by_card_id";
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
                    while (reader.Read())
                    {
                        results.Add(reader.GetString(0));
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
        /// Implements from <see cref="ICardAccessor"/>. Access the database
        /// using sp_select_cards
        /// </summary>
        public Dictionary<int, Card> SelectCards()
        {
            Dictionary<int, Card> results = new Dictionary<int, Card>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_cards";
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
                        int cardID = reader.GetInt32(0);

                        // Shoud never fail. Just a precaution
                        if (!results.ContainsKey(cardID))
                        {
                            results.Add(cardID, new Card()
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
                                Stage = reader.GetString(16)
                            });
                        }
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
        /// Implements from <see cref="ICardAccessor"/>. Access the database
        /// using sp_select_card_moves
        /// </summary>
        public Dictionary<int, List<MoveVM>> SelectCardMoves()
        {
            Dictionary<int, List<MoveVM>> results = new Dictionary<int, List<MoveVM>>();
            Dictionary<int, MoveVM> moveVMs = new Dictionary<int, MoveVM>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_card_moves";
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
                        int cardID = reader.GetInt32(0);
                        int moveID = reader.GetInt32(1);

                        // saves the cardID as the key if it hasn't been seen before
                        if (!results.ContainsKey(cardID))
                        {
                            results.Add(cardID, new List<MoveVM>());
                        }

                        StoreMoveVMInDictionary(moveVMs, reader);

                        // checks to see if the MoveVM is already inside the list
                        // at cardID
                        if (!results[cardID].Contains(moveVMs[moveID]))
                        {
                            results[cardID].Add(moveVMs[moveID]);
                        }
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
        /// Implements from <see cref="ICardAccessor"/>. Access the database
        /// using sp_select_card_alternate_arts
        /// </summary>
        public Dictionary<int, List<string>> SelectCardAlternateArts()
        {
            Dictionary<int, List<string>> results = new Dictionary<int, List<string>>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_card_alternate_arts";
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
                        int cardID = reader.GetInt32(0);
                        string altArtID = reader.GetString(1);

                        // Shoud never fail. Just a precaution
                        if (!results.ContainsKey(cardID))
                        {
                            results.Add(cardID, new List<string>());
                        }

                        if (!results[cardID].Contains(altArtID))
                        {
                            results[cardID].Add(altArtID);
                        }
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
        /// Implements from <see cref="ICardAccessor"/>. Access the database
        /// using sp_select_cards_by_card_name
        /// </summary>
        public Dictionary<int, Card> SelectCardsByCardName(string name)
        {
            Dictionary<int, Card> results = new Dictionary<int, Card>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_cards_by_card_name";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Name",System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters["@Name"].Value = name;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int cardID = reader.GetInt32(0);

                        // Shoud never fail. Just a precaution
                        if (!results.ContainsKey(cardID))
                        {
                            results.Add(cardID, new Card()
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
                                Stage = reader.GetString(16)
                            });
                        }
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
        /// Implements from <see cref="ICardAccessor"/>. Access the database
        /// using sp_select_card_moves_by_card_name
        /// </summary>
        public Dictionary<int, List<MoveVM>> SelectCardMovesByCardName(string name)
        {
            Dictionary<int, List<MoveVM>> results = new Dictionary<int, List<MoveVM>>();
            Dictionary<int, MoveVM> moveVMs = new Dictionary<int, MoveVM>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_card_moves_by_card_name";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters["@Name"].Value = name;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int cardID = reader.GetInt32(0);
                        int moveID = reader.GetInt32(1);

                        // saves the cardID as the key if it hasn't been seen before
                        if (!results.ContainsKey(cardID))
                        {
                            results.Add(cardID, new List<MoveVM>());
                        }

                        StoreMoveVMInDictionary(moveVMs, reader);

                        // checks to see if the MoveVM is already inside the list
                        // at cardID
                        if (!results[cardID].Contains(moveVMs[moveID]))
                        {
                            results[cardID].Add(moveVMs[moveID]);
                        }
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
        /// Implements from <see cref="ICardAccessor"/>. Access the database
        /// using sp_select_card_alternate_arts_by_card_name
        /// </summary>
        public Dictionary<int, List<string>> SelectCardAlternateArtsByCardName(string name)
        {
            Dictionary<int, List<string>> results = new Dictionary<int, List<string>>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_card_alternate_arts_by_card_name";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters["@Name"].Value = name;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int cardID = reader.GetInt32(0);
                        string altArtID = reader.GetString(1);

                        // Shoud never fail. Just a precaution
                        if (!results.ContainsKey(cardID))
                        {
                            results.Add(cardID, new List<string>());
                        }

                        if (!results[cardID].Contains(altArtID))
                        {
                            results[cardID].Add(altArtID);
                        }
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

            cmd.Parameters.Add("@PokemonCardID",System.Data.SqlDbType.Int);
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

        /// <summary>
        /// Stores the data into the parameter resutls
        /// Checks to see if the MoveID has already been used.<br/>
        /// If not create a new MoveVM, then check if the ElementTypeID and Quantity for the MoveCost are Null.<br/>
        /// If not add the MoveCost to the MoveVM where the moveIDs match.
        /// </summary>
        /// <param name="moveVMs">Saves the results into this Disctionary</param>
        /// <param name="reader">Reader Line to be saved</param>
        private static void StoreMoveVMInDictionary(Dictionary<int, MoveVM> moveVMs, SqlDataReader reader)
        {
            // Uses reader.GetOrdinal instead of numbers so this
            // method can be used by multiple of methods.

            int moveID = reader.GetInt32(reader.GetOrdinal("MoveID"));

            // Checks to see if the moveID already has a MoveVM created
            if (!moveVMs.ContainsKey(moveID))
            {
                moveVMs.Add(moveID, new MoveVM()
                {
                    MoveID = moveID,
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Damage = reader.GetInt32(reader.GetOrdinal("Damage")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    Costs = new List<MoveCost>()
                });
            }

            // makes sure the moveCost is not null before adding it to the Costs
            if (!reader.IsDBNull(reader.GetOrdinal("ElementTypeID")) && !reader.IsDBNull(reader.GetOrdinal("Quantity")))
            {
                moveVMs[moveID].Costs.Add(new MoveCost()
                {
                    MoveID = moveID,
                    ElementType = reader.GetString(reader.GetOrdinal("ElementTypeID")),
                    Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                });
            }
        }

        
    }
}
