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
    public class CardComponentAccessor : ICardComponentAccessor
    {
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
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PokemonCardID", cardID);

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
        /// Implements from <see cref="ICardComponentAccessor"/>. Access the database
        /// using sp_select_moves_by_card_id
        /// </summary>
        public List<MoveVM> SelectMovesByCardID(int cardID)
        {
            Dictionary<int, MoveVM> results = new Dictionary<int, MoveVM>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_moves_by_card_id";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PokemonCardID", cardID);

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
        /// Implements from <see cref="ICardComponentAccessor"/>. Access the database
        /// using sp_insert_card_move
        /// </summary>
        public int InsertCardMove(int cardID, int moveID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_card_move";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PokemonCardID", cardID);
            cmd.Parameters.AddWithValue("@MoveID", moveID);

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
        /// Implements from <see cref="ICardComponentAccessor"/>. Access the database
        /// using sp_delete_card_moves
        /// </summary>
        public int DeleteCardMoves(int cardID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_delete_card_moves";
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
        /// Implements from <see cref="ICardComponentAccessor"/>. Access the database
        /// using sp_insert_card_alternate_art
        /// </summary>
        public int InsertCardAlternateArt(int cardID, string alternateArtID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_card_alternate_art";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PokemonCardID", cardID);

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
        /// Implements from <see cref="ICardComponentAccessor"/>. Access the database
        /// using sp_delete_card_alternate_arts
        /// </summary>
        public int DeleteCardAlternateArts(int cardID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_delete_card_alternate_arts";
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
