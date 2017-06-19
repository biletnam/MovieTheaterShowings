using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharedResources.Interfaces;
using SharedResources.Mappers;
using DAL;
using BLL;
using System.Configuration;

namespace CompositionRoot
{
    public class CRoot
    {
        //This is the database connection string:
        private string ConnectionString { get; set; }

        public IUsersDAL UsersDAL { get; private set; }
        public IMoviesDAL MoviesDAL { get; private set; }

        public IUsersBLL UsersBLL { get; private set; }
        public IMoviesBLL MoviesBLL { get; private set; }

        //Constructor:
        public CRoot(string environment = "prod")
        {
            if(environment == "prod"){
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MovieTheaterShowings_connection"].ConnectionString;
            }
            else if(environment == "test"){
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testDB_MovieTheaterShowings_connection"].ConnectionString;
            }

            UsersDAL = (IUsersDAL)(new UsersDAL(ConnectionString));
            MoviesDAL = (IMoviesDAL)(new MoviesDAL(ConnectionString));

            UsersBLL = (IUsersBLL)(new UsersBLL(UsersDAL));
            MoviesBLL = (IMoviesBLL)(new MoviesBLL(MoviesDAL));
        }
    }
}