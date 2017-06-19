using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Interfaces
{
    public interface IMoviesDAL
    {
        IMovieMapper InsertMovie(IMovieMapper movie);
        List<IMovieMapper> Get_All_Movies();
        IMovieMapper Get_Movie_by_ID(IMovieMapper movie);
        List<IMovieMapper> Search_Movie_by_Name(string search_criteria);
        IMovieMapper Update(IMovieMapper movie);
        bool DeleteMovie(IMovieMapper movie);
        IShowTimesMapper InsertShowTime(IMovieMapper movie, IShowTimesMapper showtime);
        List<IShowTimesMapper> Get_ShowTimes_by_MovieID(IMovieMapper movie);
    }
}
