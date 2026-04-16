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
    public class MoveAccessor : IMoveAccessor
    {

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_select_move_by_moveid
        /// </summary>
        public Move SelectMoveByMoveID(int moveID)
        {
            Move resultMoves = null;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_move_by_moveid";
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MoveID", moveID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    resultMoves = new Move()
                    {
                        MoveID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Damage = reader.GetInt32(2),
                        Description = reader.GetString(3),
                        Active = reader.GetBoolean(4),
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

            return resultMoves;
        }

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_select_move_cost_by_moveid
        /// </summary>
        public List<MoveCost> SelectMoveCostsByMoveID(int moveID)
        {
            List<MoveCost> resultMoveCosts = new List<MoveCost>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_move_cost_by_moveid";
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MoveID", moveID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        resultMoveCosts.Add(new MoveCost()
                        {
                            MoveID = reader.GetInt32(0),
                            ElementType = reader.GetString(1),
                            Quantity = reader.GetInt32(2)
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

            return resultMoveCosts;
        }

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_select_moves_with_move_cost
        /// </summary>
        public List<MoveVM> SelectMoveVMsWithMoveCost()
        {
            Dictionary<int, MoveVM> results = new Dictionary<int, MoveVM>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_moves_with_move_cost";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int moveID = reader.GetInt32(0);

                        // If this is the first time seeing this MoveID, add it
                        if (!results.ContainsKey(moveID))
                        {
                            results.Add(moveID, new MoveVM()
                            {
                                MoveID = moveID,
                                Name = reader.GetString(1),
                                Damage = reader.GetInt32(2),
                                Description = reader.GetString(3),
                                Active = reader.GetBoolean(4),
                                Costs = new List<MoveCost>()
                            });
                        }

                        // Add MoveCost (each row has a cost)
                        results[moveID].Costs.Add(new MoveCost
                        {
                            MoveID = moveID,
                            ElementType = reader.GetString(5),
                            Quantity = reader.GetInt32(6)
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
            return results.Values.ToList();
        }

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_select_moves_without_move_cost
        /// </summary>
        public List<Move> SelectMovesWithoutMoveCost()
        {
            List<Move> results = new List<Move>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_moves_without_move_cost";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        results.Add(new Move()
                        {
                            MoveID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Damage = reader.GetInt32(2),
                            Description = reader.GetString(3),
                            Active = reader.GetBoolean(4),
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
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_select_moves_active_paginated
        /// </summary>
        public PaginatedResult<Move> SelectActiveMoves(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<Move> results = new PaginatedResult<Move>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_moves_active_paginated";
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
                    results.Items.Add(new Move()
                    {
                        MoveID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Damage = reader.GetInt32(2),
                        Description = reader.GetString(3),
                        Active = reader.GetBoolean(4),
                    });

                    results.TotalCount = reader.GetInt32(5);
                    results.PageNumber = reader.GetInt32(6);
                    results.PageSize = reader.GetInt32(7);
                    results.TotalPages = Convert.ToInt32(reader.GetDecimal(8));
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
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_select_moves_deactive_paginated
        /// </summary>
        public PaginatedResult<Move> SelectDeactiveMoves(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<Move> results = new PaginatedResult<Move>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_moves_deactive_paginated";
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
                    results.Items.Add(new Move()
                    {
                        MoveID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Damage = reader.GetInt32(2),
                        Description = reader.GetString(3),
                        Active = reader.GetBoolean(4),
                    });

                    results.TotalCount = reader.GetInt32(5);
                    results.PageNumber = reader.GetInt32(6);
                    results.PageSize = reader.GetInt32(7);
                    results.TotalPages = Convert.ToInt32(reader.GetDecimal(8));
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
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_insert_move
        /// </summary>
        public int InsertMove(Move move)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_move";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Damage", move.Damage);

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 200);
            
            cmd.Parameters["@Name"].Value = move.Name;
            cmd.Parameters["@Description"].Value = move.Description;

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
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_insert_move_cost
        /// </summary>
        public int InsertMoveCost(MoveCost cost)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_move_cost";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MoveID", cost.MoveID);
            cmd.Parameters.AddWithValue("@Quantity", cost.Quantity);

            cmd.Parameters.Add("@ElementTypeID", SqlDbType.NVarChar, 15);
            cmd.Parameters["@ElementTypeID"].Value = cost.ElementType;

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
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_delete_move
        /// </summary>
        public int DeleteMove(int moveID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_delete_move";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

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
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_update_move
        /// </summary>
        public int UpdateMove(Move move)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_update_move";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MoveID", move.MoveID);
            cmd.Parameters.AddWithValue("@Damage", move.Damage);

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 200);

            cmd.Parameters["@Name"].Value = move.Name;
            cmd.Parameters["@Description"].Value = move.Description;

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
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_delete_move_cost
        /// </summary>
        public int DeleteMoveCost(int moveID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_delete_move_cost";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

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
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_deactivate_move
        /// </summary>
        public int DeactivateMove(int moveID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_deactivate_move";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MoveID", moveID);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_reactivate_move
        /// </summary>
        public int ReactivateMove(int moveID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_reactivate_move";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MoveID", moveID);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return count;
        }
    }
}
