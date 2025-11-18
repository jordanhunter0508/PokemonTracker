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
            Dictionary<string, MoveVM> results = new Dictionary<string, MoveVM>();

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
        /// Stores the data into the parameter resutls
        /// Checks to see if the MoveID has already been used.<br/>
        /// If not create a new MoveVM, then check if the ElementTypeID and Quantity for the MoveCost are Null.<br/>
        /// If not add the MoveCost to the MoveVM where the moveIDs match.
        /// </summary>
        /// <param name="results">Saves the results into this Disctionary</param>
        /// <param name="reader">Reader Line to be saved</param>
        private static void StoreMoveVMInDictionary(Dictionary<string, MoveVM> results, SqlDataReader reader)
        {
            string moveID = reader.GetString(0);

            // Checks to see if the moveID already has a MoveVM created
            if (!results.ContainsKey(moveID))
            {
                results.Add(moveID, new MoveVM()
                {
                    MoveID = moveID,
                    Damage = reader.GetInt32(1),
                    Description = reader.GetString(2),
                    Costs = new List<MoveCost>()
                });
            }

            // makes sure the moveCost is not null before adding it to the Costs
            if (!reader.IsDBNull(3) && !reader.IsDBNull(4))
            {
                results[moveID].Costs.Add(new MoveCost()
                {
                    MoveID = moveID,
                    ElementType = reader.GetString(3),
                    Quantity = reader.GetInt32(4),
                });
            }
        }
    }
}
