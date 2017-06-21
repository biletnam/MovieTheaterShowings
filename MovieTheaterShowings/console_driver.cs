using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharedResources.Interfaces;
using SharedResources.Mappers;
using CompositionRoot;

namespace MovieTheaterShowings
{
    public class console_driver
    {
        static void Main(string[] args)
        {
            CRoot CompositionRoot = new CRoot("test");

            //IUsersDAL users_dal = CompositionRoot.UsersDAL;
            //IMoviesDAL movies_dal = CompositionRoot.MoviesDAL;

            IUsersBLL users_bll = CompositionRoot.UsersBLL;
            IMoviesBLL movies_bll = CompositionRoot.MoviesBLL;
        }
    }
}