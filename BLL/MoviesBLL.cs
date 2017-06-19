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


    }
}
