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
    public class MoviesDAL : IMoviesDAL
    {
        private string ConnectionString { get; set; }

        //Constructor:
        public MoviesDAL(string _connectionString)
        {
            ConnectionString = _connectionString;
        }
        
        //CRUD functionality for 'Movies' table:

        //Create:
        public IMovieMapper InsertMovie(IMovieMapper movie)
        {
            IMovieMapper output = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("insert_Movie", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //Add input parameters:
                        command.Parameters.Add("@Title", SqlDbType.VarChar).Value = movie.Title;
                        command.Parameters.Add("@RunTime", SqlDbType.Int).Value = movie.RunTime;
                        command.Parameters.Add("@Image", SqlDbType.VarChar).Value = movie.Image;

                        //Setup output parameter for the returned Movie.Id:
                        SqlParameter IdentityOutput = command.Parameters.Add("@IdentityOutput", SqlDbType.Int);
                        IdentityOutput.Value = null;
                        IdentityOutput.Direction = ParameterDirection.Output;

                        command.Prepare();
                        command.ExecuteNonQuery(); //Run the query.

                        //Return the user that was created:
                        output = new MovieMapper();
                        output.Id = (int)IdentityOutput.Value;
                        output.Title = (String)movie.Title;
                        output.RunTime = (int)movie.RunTime;
                        output.Image = (String)movie.Image;
                    }
                }
            }
            catch (SqlException e) 
            { 
                throw new SqlDALException("There was a problem with SQL.  Please provide valid data.  The Title, Runtime, and Image fields must be provided.  The Title field must be unique.", e); 
            }
            return output;
        }


        public IMovieMapper Update(IMovieMapper movie) 
        {
            IMovieMapper output = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("update_movie", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //Add input parameters:
                        command.Parameters.Add("@MovieID", SqlDbType.Int).Value = movie.Id;
                        command.Parameters.Add("@Title", SqlDbType.VarChar).Value = movie.Title;
                        command.Parameters.Add("@RunTime", SqlDbType.Int).Value = movie.RunTime;
                        command.Parameters.Add("@Image", SqlDbType.VarChar).Value = movie.Image;

                        command.Prepare();
                        int rowsAffected = command.ExecuteNonQuery(); //Run the query.

                        if(rowsAffected == 1){
                            output = movie;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw new SqlDALException("There was a problem with SQL.  Please provide valid data.  The MovieId, Title, Runtime, and Image fields must be provided.  The Title field must be unique.", e);
            }
            return output;
        }


        public IMovieMapper Get_Movie_by_ID(IMovieMapper movie)
        {
            IMovieMapper output = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("select_movie_by_id", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //Add input parameters:
                        command.Parameters.Add("@MovieID", SqlDbType.Int).Value = movie.Id;

                        //command.ExecuteNonQuery(); //Run the query.

                        command.Prepare();
                        SqlDataReader movies = command.ExecuteReader();

                        while (movies.Read())
                        {

                            //Return the user that was created:
                            output = new MovieMapper();
                            output.Id = movie.Id;
                            output.Title = (String)movies["Title"];
                            output.RunTime = (int)movies["RunTime"];
                            output.Image = (String)movies["Image"];
                            output.ShowTimes = Get_ShowTimes_by_MovieID(movie);
                        }

                    }
                }
            }
            catch (SqlException e)
            {
                throw new SqlDALException("There was a problem with SQL.  Please provide valid data.  The MovieId field must be provided and must exist in the database.", e);
            }
            return output;
        }

        public List<IMovieMapper> Get_All_Movies() 
        {
            List<IMovieMapper> output = new List<IMovieMapper>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("select_all_movies", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Prepare();
                        SqlDataReader movies = command.ExecuteReader();

                        while (movies.Read())
                        {
                            //Return the user that was created:
                            IMovieMapper movie = new MovieMapper();
                            movie.Id = (int)movies["Id"];
                            movie.Title = (String)movies["Title"];
                            movie.RunTime = (int)movies["RunTime"];
                            movie.Image = (String)movies["Image"];
                            movie.ShowTimes = Get_ShowTimes_by_MovieID(movie);
                            output.Add(movie);
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

        public bool DeleteMovie(IMovieMapper movie)
        {
            bool output = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("delete_movie", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //Add input parameters:
                        command.Parameters.Add("@MovieID", SqlDbType.Int).Value = movie.Id;

                        //Setup output parameter for the returned Movie.Id:
                        SqlParameter RowsAffected = command.Parameters.Add("@RowsAffected", SqlDbType.Int);
                        RowsAffected.Value = null;
                        RowsAffected.Direction = ParameterDirection.Output;

                        command.Prepare();
                        command.ExecuteNonQuery(); //Run the query.

                        //Return a boolean to let us know if the delete was successful:
                        output = (((int)RowsAffected.Value) == 1)?true:false;
                    }
                }
            }
            catch (SqlException e)
            {
                throw new SqlDALException("There was a problem with SQL.  Please provide valid data.  The MovieId field must be provided and must exist in the database.", e);
            }
            return output;
        }

        //CRUD functionality for 'ShowTimes' table:

        //Create:
        public IShowTimesMapper InsertShowTime(IMovieMapper movie, IShowTimesMapper showtime)
        {
            IShowTimesMapper output = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("insert_ShowTime", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //Add input parameters:
                        command.Parameters.Add("@ShowingDateTime", SqlDbType.DateTime).Value = showtime.ShowingDateTime;
                        command.Parameters.Add("@MovieId", SqlDbType.Int).Value = movie.Id;

                        //Setup output parameter for the returned Movie.Id:
                        SqlParameter IdentityOutput = command.Parameters.Add("@IdentityOutput", SqlDbType.Int);
                        IdentityOutput.Value = null;
                        IdentityOutput.Direction = ParameterDirection.Output;

                        command.Prepare();
                        command.ExecuteNonQuery(); //Run the query.

                        //Return the user that was created:
                        output = new ShowTimesMapper();
                        output.Id = (int)IdentityOutput.Value;
                        output.ShowingDateTime = (DateTime)showtime.ShowingDateTime;
                        output.MovieId = (int)movie.Id;
                    }
                }
            }
            catch (SqlException e)
            {
                throw new SqlDALException("There was a problem with SQL.  Please provide valid data.  The ShowingDateTime and MovieId fields must be provided.", e);
            }
            return output;
        }

        public List<IShowTimesMapper> Get_ShowTimes_by_MovieID(IMovieMapper movie) 
        {
            List<IShowTimesMapper> output = new List<IShowTimesMapper>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("select_all_showtimes_by_movie_id", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //Add input parameters:
                        command.Parameters.Add("@MovieID", SqlDbType.Int).Value = movie.Id;

                        command.Prepare();
                        SqlDataReader showtimes = command.ExecuteReader();

                        while(showtimes.Read())
                        {
                            //Create a new showtime object:
                            ShowTimesMapper showtime = new ShowTimesMapper();
                            showtime.Id = (int)showtimes["Id"];
                            showtime.ShowingDateTime = (DateTime)showtimes["ShowingDateTime"];
                            showtime.MovieId = (int)showtimes["MovieId"];

                            //Add this new object to a collection:
                            output.Add(showtime);
                        }
                        
                    }
                }
            }
            catch (SqlException e)
            {
                throw new SqlDALException("There was a problem with SQL.  Please provide valid data.  The MovieId field must be provided, and must exist in the database.", e);
            }
            return output;
        }

        public List<IMovieMapper> Search_Movie_by_Name(string search_criteria) 
        {
            List<IMovieMapper> output = new List<IMovieMapper>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("search_movies_by_title", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //Add input parameters:
                        command.Parameters.Add("@Title", SqlDbType.VarChar).Value = search_criteria;

                        command.Prepare();
                        SqlDataReader movies = command.ExecuteReader();

                        while (movies.Read())
                        {
                            MovieMapper movie = new MovieMapper();
                            movie.Id = (int)movies["Id"];
                            movie.Title = (String)movies["Title"];
                            movie.RunTime = (int)movies["RunTime"];
                            movie.Image = (String)movies["Image"];
                            movie.ShowTimes = Get_ShowTimes_by_MovieID(movie);
                            output.Add(movie);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw new SqlDALException("There was a problem with SQL.  Please provide valid data.  The Title field must be provided, and must exist in the database.", e);
            }
            return output;
        }

    }
}