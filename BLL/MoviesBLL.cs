using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharedResources.Interfaces;
using SharedResources.Exceptions.BLL;
using SharedResources.Exceptions.DAL;

namespace BLL
{
    public class MoviesBLL : IMoviesBLL
    {
        private IMoviesDAL moviesDAL { get; set; }

        public MoviesBLL(IMoviesDAL _moviesDAL)
        {
            moviesDAL = _moviesDAL;
        }

        public IMovieMapper InsertMovie(IMovieMapper movie) 
        {
            IMovieMapper output = null;
            //Check for null or empty values:
            var testVars = new Object[]{
                movie.Title,
                movie.RunTime,
                movie.Image
            };
            if (!DataValidator.is_null_empty_or_zero(testVars))
            {
                try
                {
                    //Insert the movie:
                    output = moviesDAL.InsertMovie(movie);
                }
                catch (SqlDALException e)
                {
                    throw new SqlBLLException("One or more SQL constraints may have caused this issue.  Please be sure the Title field is unique and all other data is valid.", e);
                }
            }
            else
            {
                throw new MissingDataBLLException("Cannot complete this operation because required data is missing.  Please be sure to provide title, runtime, and image.");
            }
            return output;
        }

        public IMovieMapper Update(IMovieMapper movie)
        {
            IMovieMapper output = null;
            //Check for null or empty values:
            var testVars = new Object[]{
                movie.Id,
                movie.Title,
                movie.RunTime,
                movie.Image
            };
            if (!DataValidator.is_null_empty_or_zero(testVars))
            {
                try
                {
                    //Insert the movie:
                    output = moviesDAL.Update(movie);
                }
                catch (SqlDALException e)
                {
                    throw new SqlBLLException("One or more SQL constraints may have caused this issue.  Please be sure the Title field is unique and all other data is valid.", e);
                }
            }
            else
            {
                throw new MissingDataBLLException("Cannot complete this operation because required data is missing.  Please be sure to provide movieId, title, runtime, and image.");
            }
            return output;
        }

        public IMovieMapper Get_Movie_by_ID(IMovieMapper movie)
        {
            IMovieMapper output = null;
            //Check for null or empty values:
            var testVars = new Object[]{
                movie.Id
            };
            if (!DataValidator.is_null_empty_or_zero(testVars))
            {
                try
                {
                    //Insert the movie:
                    output = moviesDAL.Get_Movie_by_ID(movie);
                }
                catch (SqlDALException e)
                {
                    throw new SqlBLLException("One or more SQL constraints may have caused this issue.  Please provide the movie ID.", e);
                }
            }
            else
            {
                throw new MissingDataBLLException("Cannot complete this operation because required data is missing.  Please be sure to provide movie ID.");
            }
            return output;
        }

        public List<IMovieMapper> Get_All_Movies() 
        {
            List<IMovieMapper> output = new List<IMovieMapper>();
            try
            {
                //Insert the movie:
                output = moviesDAL.Get_All_Movies();
            }
            catch (SqlDALException e)
            {
                throw new SqlBLLException("An error occurred with SQL.", e);
            }
            return output;
        }

        public bool DeleteMovie(IMovieMapper movie) 
        {
            bool output = false;
            //Check for null or empty values:
            var testVars = new Object[]{
                movie.Id
            };
            if (!DataValidator.is_null_empty_or_zero(testVars))
            {
                try
                {
                    //Insert the movie:
                    output = moviesDAL.DeleteMovie(movie);
                }
                catch (SqlDALException e)
                {
                    throw new SqlBLLException("One or more SQL constraints may have caused this issue.  Please provide the movie ID.", e);
                }
            }
            else
            {
                throw new MissingDataBLLException("Cannot complete this operation because required data is missing.  Please be sure to provide movie ID.");
            }
            return output;
        }

        public IShowTimesMapper InsertShowTime(IMovieMapper movie, IShowTimesMapper showtime) 
        {
            IShowTimesMapper output = null;
            //Check for null or empty values:
            var testVars = new Object[]{
                movie.Id,
                showtime.ShowingDateTime
            };
            if (!DataValidator.is_null_empty_or_zero(testVars))
            {
                try
                {
                    //Insert the movie:
                    output = moviesDAL.InsertShowTime(movie, showtime);
                }
                catch (SqlDALException e)
                {
                    throw new SqlBLLException("One or more SQL constraints may have caused this issue.  Please provide the movie.Id and showtime.ShowingDateTime.", e);
                }
            }
            else
            {
                throw new MissingDataBLLException("Cannot complete this operation because required data is missing.  Please be sure to provide movie.Id and showtime.ShowingDateTime.");
            }
            return output;
        }

        public List<IShowTimesMapper> Get_ShowTimes_by_MovieID(IMovieMapper movie) 
        {
            List<IShowTimesMapper> output = new List<IShowTimesMapper>();
            //Check for null or empty values:
            var testVars = new Object[]{
                movie.Id
            };
            if (!DataValidator.is_null_empty_or_zero(testVars))
            {
                try
                {
                    //Insert the movie:
                    output = moviesDAL.Get_ShowTimes_by_MovieID(movie);
                }
                catch (SqlDALException e)
                {
                    throw new SqlBLLException("One or more SQL constraints may have caused this issue.  Please provide the movie.Id.", e);
                }
            }
            else
            {
                throw new MissingDataBLLException("Cannot complete this operation because required data is missing.  Please be sure to provide movie.Id.");
            }
            return output;
        }

        public List<IMovieMapper> Search_Movie_by_Name(string search_criteria) 
        {
            List<IMovieMapper> output = new List<IMovieMapper>();
            //Check for null or empty values:
            var testVars = new Object[]{
                search_criteria
            };
            if (!DataValidator.is_null_empty_or_zero(testVars))
            {
                try
                {
                    //Insert the movie:
                    output = moviesDAL.Search_Movie_by_Name(search_criteria);
                }
                catch (SqlDALException e)
                {
                    throw new SqlBLLException("One or more SQL constraints may have caused this issue.  Please provide the search_criteria (Name of user).", e);
                }
            }
            else
            {
                throw new MissingDataBLLException("Cannot complete this operation because required data is missing.  Please provide the search_criteria (Name of user).");
            }
            return output;
        }

    }
}
