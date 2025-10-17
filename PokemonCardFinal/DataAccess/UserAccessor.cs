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
        /// Implements from IUserAccessor access the database
        /// using sp_authenticate_user_by_email_and_password_hash
        /// </summary>
        public int AuthenticateUserByEmailAndPasswordHash(string email, string passwordHash)
        {
            int count = 0;

            // ADO.Net needs a connection
            var conn = DBConnection.GetConnection();

            // Command text
            var cmdText = "sp_authenticate_user_by_email_and_password_hash";

            // Create a comman object from the connection and command text
            var cmd = new SqlCommand(cmdText, conn);

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

        public User SelectUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public List<string> SelectRoleByUserEmail(string email)
        {
            throw new NotImplementedException();
        }

        
    }
}
