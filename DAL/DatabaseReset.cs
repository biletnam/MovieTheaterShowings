using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DAL
{
    public class DatabaseReset
    {
        public string ConnectionString { get; set; }
        public DatabaseReset(string environment = "prod")
        {
            if (environment == "prod")
            {
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MovieTheaterShowings_connection"].ConnectionString;
            }
            else if (environment == "test")
            {
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testDB_MovieTheaterShowings_connection"].ConnectionString;
            }
        }
        public void resetDatabase() 
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("reset_db", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery(); //Run the query.
                    }
                }
            }
            catch(SqlException e){}
        }
    }
}
