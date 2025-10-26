using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class UserAccessor : IUserAccessor
    {
        /// <summary>
        /// Implements from <see cref="IUserAccessor"/>. Access the database
        /// using sp_authenticate_user_by_email_and_password_hash
        /// </summary>
        public int AuthenticateUserByEmailAndPasswordHash(string email, string passwordHash)
        {
            int count = 0;

            // ADO.Net needs a connection
            SqlConnection conn = DBConnection.GetConnection();

            // Command text
            string cmdText = "sp_authenticate_user_by_email_and_password_hash";

            // Create a command object from the connection and command text
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // Set the command type
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // Add Parameters to the command
            cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@PasswordHash", System.Data.SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            try
            {
                // Open the connection
                conn.Open();

                // Execute the command and capture the results
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // After the connection is used close it
            finally
            {
                conn.Close();
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IUserAccessor"/>. Access the database
        /// using sp_select_user_by_email
        /// </summary>
        public User SelectUserByEmail(string email)
        {
            User result = null;

            // ADO.Net needs a Conncetion
            SqlConnection conn = DBConnection.GetConnection();

            // Command Text
            string cmdText = "sp_select_user_by_email";

            // Create command object from the string and connection
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // Set the command type
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // Add paramater
            cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar, 250);

            // Set paramater
            cmd.Parameters["@Email"].Value = email;

            try
            {
                // Open a connection
                conn.Open();

                // Creates a reader object
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    result = new User()
                    {
                        UserID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        Surname = reader.GetString(2),
                        Email = reader.GetString(3),
                        Active = reader.GetBoolean(4),
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Close connection after user
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IUserAccessor"/>. Access the database
        /// using sp_select_role_by_user_email
        /// </summary>
        public List<string> SelectRoleByUserEmail(string email)
        {
            List<string> results = new List<string>();

            // Establish a connection
            SqlConnection conn = DBConnection.GetConnection();

            // Command text
            string cmdText = "sp_select_role_by_user_email";

            // Create command object from the string and connection
            SqlCommand cmd = new SqlCommand(cmdText,conn);

            // Set the command type
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // Add parameters
            cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar, 250);

            // Set paramaters
            cmd.Parameters["@Email"].Value = email;

            try
            {
                // Open Connection
                conn.Open();

                // Creates a reader object
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    // Loop through rows
                    while (reader.Read())
                    {
                        // Add to results
                        results.Add(reader.GetString(0));
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Close connection after use
            finally
            { 
                conn.Close();
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IUserAccessor"/>. Access the database
        /// using sp_create_user_account
        /// </summary>
        public int CreateUserAccount(string givenName, string surname, string email, string passwordHash)
        {
            int count = 0;

            // ADO.Net needs a connection
            SqlConnection conn = DBConnection.GetConnection();

            // Command text
            string cmdText = "sp_create_user_account";

            // Create a command object from the connection and command text
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // Set the command type
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // Add Parameters to the command
            cmd.Parameters.Add("@GivenName", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Surname", System.Data.SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@PasswordHash", System.Data.SqlDbType.NVarChar, 100);

            cmd.Parameters["@GivenName"].Value = givenName;
            cmd.Parameters["@Surname"].Value = surname;      
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;


            try
            {
                // Open the connection
                conn.Open();

                // Execute the command and capture the results
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // After the connection is used close it
            finally
            {
                conn.Close();
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IUserAccessor"/>. Access the database
        /// using sp_select_user_count_by_email
        /// </summary>
        public int SelectUserCountByEmail(string email)
        {
            int count = 0;

            // ADO.Net needs a connection
            SqlConnection conn = DBConnection.GetConnection();

            // Command text
            string cmdText = "sp_select_user_count_by_email";

            // Create a command object from the connection and command text
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // Set the command type
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // Add Parameters to the command
            cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar, 250);

            cmd.Parameters["@Email"].Value = email;

            try
            {
                // Open the connection
                conn.Open();

                // Execute the command and capture the results
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // After the connection is used close it
            finally
            {
                conn.Close();
            }

            return count;
        }

        public int AddUserRole(int userID, string roleID = "General")
        {
            int count = 0;

            // ADO.Net needs a connection
            SqlConnection conn = DBConnection.GetConnection();

            // Command text
            string cmdText = "sp_add_user_role";

            // Create a command object from the connection and command text
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // Set the command type
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // Add Parameters to the command
            cmd.Parameters.Add("@RoleID", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@UserID", System.Data.SqlDbType.Int);

            cmd.Parameters["@RoleID"].Value = roleID;
            cmd.Parameters["@UserID"].Value = userID;



            try
            {
                // Open the connection
                conn.Open();

                // Execute the command and capture the results
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // After the connection is used close it
            finally
            {
                conn.Close();
            }

            return count;
        }
    }
}
