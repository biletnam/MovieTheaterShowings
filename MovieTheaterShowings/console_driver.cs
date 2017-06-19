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

            IUsersDAL users_dal = CompositionRoot.UsersDAL;
            IMoviesDAL movies_dal = CompositionRoot.MoviesDAL;

            IUsersBLL users_bll = CompositionRoot.UsersBLL;
            IMoviesBLL movies_bll = CompositionRoot.MoviesBLL;

            //Insert a user:
            //IUserMapper user1 = users_bll.Insert(new UserMapper { Name = "comey", RoleName = "user", password_hash = "hillary" });
            //IUserMapper foundUser = users_bll.Get_User_by_User_Name(user1);
            //Console.WriteLine(foundUser.password_hash);

            //Insert a user:
            //IUserMapper user1 = users_dal.Insert(new UserMapper { Name = "", RoleName = "user", password_hash = "emails" });
            //IUserMapper foundUser = users_bll.Get_User_by_User_Name(user1);
            //Console.WriteLine(foundUser.password_hash);

            //Authenticate a user:
            //bool authentic = users_bll.authenticate_user(new UserMapper { Name = "trump", password_hash = "thebestpassword" });
            //if (authentic)
            //{
            //    Console.WriteLine("Is authentic");
            //}
            //else
            //{
            //    Console.WriteLine("NOT authentic");
            //}

            //Get a user by name:
            //IUserMapper foundUser = users_bll.Get_User_by_User_Name(new UserMapper { Name = "t" });
            //Console.WriteLine(foundUser.password_hash);

            //Insert a movie:
            //IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 0, Image = "/path/to/image/braveheart.png" });





            //Add a showtime to a movie:
            //DateTime Constructor for reference:
            //public DateTime(
            //    int year,
            //    int month,
            //    int day,
            //    int hour,
            //    int minute,
            //    int second
            //)
            //IShowTimesMapper showtime1 = movies_dal.InsertShowTime(movie1, new ShowTimesMapper { ShowingDateTime = new DateTime(2017, 01, 20, 13, 01, 05) });

            //List<IShowTimesMapper> showtimes = movies_dal.Get_ShowTimes_by_MovieID(new MovieMapper{ Id = 7 });

            //foreach(IShowTimesMapper showtime in showtimes){
            //    Console.WriteLine(showtime.Id);
            //    Console.WriteLine(showtime.ShowingDateTime);
            //    Console.WriteLine(showtime.MovieId);
            //}


           // MovieMapper movie2 = movies_dal.
            //IMovieMapper movie3 = movies_dal.Get_Movie_by_ID(new MovieMapper { Id = 7 });

            //Console.WriteLine(movie3.Id);
            //Console.WriteLine(movie3.Title);
            //Console.WriteLine(movie3.RunTime);
            //Console.WriteLine(movie3.Image);

            //foreach (IShowTimesMapper showtime in movie3.ShowTimes)
            //{
            //    Console.WriteLine("Showtime: " + showtime.Id + showtime.ShowingDateTime + showtime.MovieId);
            //}

            //List<IMovieMapper> all_movies = movies_dal.Get_All_Movies();

            //foreach(IMovieMapper movie in all_movies){
            //    Console.WriteLine(movie.Title);
            //    foreach(IShowTimesMapper showtime in movie.ShowTimes){
            //        Console.WriteLine("    " + showtime.ShowingDateTime); 
            //    }
            //}


            //Boolean deleted = movies_dal.DeleteMovie(new MovieMapper{ Id = 5 });
            //if(deleted){
            //    Console.WriteLine("Movie Deleted.");
            //}
            //else { Console.WriteLine("Failed:  Movie not deleted."); }


            //Update a movie
            //IMovieMapper updated_movie = movies_dal.Update(new MovieMapper{ Id = 7, Title = "Back to the Future II", RunTime = 300, Image = "/another/path/to/an/image.png" });
            //Console.WriteLine(updated_movie.Title);

            //Search a movie by title:
            //List<IMovieMapper> search_results = movies_dal.Search_Movie_by_Name("matrix");
            //foreach (IMovieMapper movie in search_results)
            //{
            //    Console.WriteLine(movie.Title);
            //    foreach (IShowTimesMapper showtime in movie.ShowTimes)
            //    {
            //        Console.WriteLine("    " + showtime.ShowingDateTime);
            //    }
            //}


            //Search a movie by title:
            //IMovieMapper movie1 = movies_dal.InsertMovie(new MovieMapper { Title = "Kill Bill 2", RunTime = 130, Image = "/path/to/image/kb2.png" });
            //IMovieMapper movie2 = movies_dal.InsertMovie(new MovieMapper { Title = "Another movie with kill in the title", RunTime = 130, Image = "/path/to/image/kadf.png" });

            //System.Threading.Thread.Sleep(5000);
            
            //List<IMovieMapper> search_results = movies_dal.Search_Movie_by_Name("kill");
            //Console.WriteLine(search_results.Count);
            //foreach (IMovieMapper movie in search_results)
            //{
            //    Console.WriteLine(movie.Title);
            //    foreach (IShowTimesMapper showtime in movie.ShowTimes)
            //    {
            //        Console.WriteLine("    " + showtime.ShowingDateTime);
            //    }
            //}


        }
    }
}