using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
//using System.Configuration;
using SharedResources.Interfaces;
using SharedResources.Mappers;
using SharedResources.Exceptions.DAL;

namespace DAL
{
    public class UsersDAL : IUsersDAL
    {
        private string ConnectionString { get; set; }

        //Constructor:
        public UsersDAL(string _connectionString)
        {
            ConnectionString = _connectionString;
        }
        
        //CRUD functionality for 'Users' table:

        //Create:
        public IUserMapper Insert(IUserMapper user)
        {
            IUserMapper output = null;
            try
            {
                using(SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("insert_User", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //Add input parameters:
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = user.Name;
                        command.Parameters.Add("@Role", SqlDbType.VarChar).Value = user.RoleName;
                        command.Parameters.Add("@password_hash", SqlDbType.VarChar).Value = user.password_hash;

                        //Setup output parameter for the returned User.Id:
                        SqlParameter IdentityOutput = command.Parameters.Add("@IdentityOutput", SqlDbType.Int);
                        IdentityOutput.Value = null;
                        IdentityOutput.Direction = ParameterDirection.Output;

                        //Setup output paramter for the returned Role.Id:
                        SqlParameter RoleIDOutput = command.Parameters.Add("@RoleIDOutput", SqlDbType.Int);
                        RoleIDOutput.Value = null;
                        RoleIDOutput.Direction = ParameterDirection.Output;

                        command.Prepare();
                        command.ExecuteNonQuery(); //Run the query.

                        if (!(IdentityOutput.Value is DBNull))
                        {
                            //Return the user that was created:
                            output = new UserMapper();
                            output.Id = (int)IdentityOutput.Value;
                            output.Name = user.Name;
                            output.password_hash = user.password_hash;
                            output.RoleName = user.RoleName;
                            output.RoleId = (int)RoleIDOutput.Value;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw new SqlDALException("There was a problem with SQL.  Please provide valid data.  The Name, Rolename, and password_hash fields must be provided.  The Name field must be unique.", e);
            }

            //Return the method output:  If zero, that means there was a problem.
            return output;
        }

        //Get User by User.Name
        public IUserMapper Get_User_by_User_Name(IUserMapper user)
        {
            IUserMapper output = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("select_user_by_name", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Add input parameters:
                        command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.Name;

                        //Setup output parameter for the returned User.Id:
                        SqlParameter UserId = command.Parameters.Add("@Id", SqlDbType.Int);
                        UserId.Value = null;
                        UserId.Direction = ParameterDirection.Output;

                        //Setup output paramter for the returned User.password_hash:
                        SqlParameter password_hash = command.Parameters.Add("@password_hash", SqlDbType.VarChar);
                        password_hash.Value = null;
                        password_hash.Size = 255;
                        password_hash.Direction = ParameterDirection.Output;

                        //Setup output paramter for the returned user's RoleId:
                        SqlParameter RoleID = command.Parameters.Add("@RoleId", SqlDbType.Int);
                        RoleID.Value = null;
                        RoleID.Direction = ParameterDirection.Output;

                        //Setup output paramter for the returned user's role name:
                        SqlParameter RoleName = command.Parameters.Add("@RoleName", SqlDbType.VarChar);
                        RoleName.Value = null;
                        RoleName.Size = 50;
                        RoleName.Direction = ParameterDirection.Output;

                        command.Prepare();
                        command.ExecuteNonQuery(); //Run the query.

                        if (!(UserId.Value is DBNull))
                        {
                            //Return the user that was created:
                            output = new UserMapper();
                            output.Id = (int)UserId.Value;
                            output.Name = user.Name;
                            output.password_hash = (string)password_hash.Value;
                            output.RoleName = (string)RoleName.Value;
                            output.RoleId = (int)RoleID.Value;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw new SqlDALException("There was a problem with SQL.  Please provide valid data.  The Name field must be provided, and must exist in the database.", e);
            }

            //Return the method output:  If zero, that means there was a problem.
            return output;
        }

        public List<IUserMapper> Get_All_Users()
        {
            List<IUserMapper> output = new List<IUserMapper>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("select_all_users", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Prepare();
                        SqlDataReader users = command.ExecuteReader();

                        while (users.Read())
                        {
                            //Return the user that was created:
                            IUserMapper user = new UserMapper();
                            user.Id = (int)users["Id"];
                            user.Name = (String)users["Name"];
                            user.password_hash = (String)users["password_hash"];
                            user.RoleId = (int)users["RoleId"];
                            user.RoleName = (String)users["RoleName"];
                            output.Add(user);
                        }

                    }
                }
            }
            catch (SqlException e)
            {
                throw new SqlDALException("There was a problem with SQL.", e);
            }
            return output;
        }




    }
}
