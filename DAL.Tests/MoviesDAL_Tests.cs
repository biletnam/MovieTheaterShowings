using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DAL;
using CompositionRoot;
using SharedResources.Interfaces;
using SharedResources.Mappers;
using SharedResources.Exceptions.DAL;
using System.Collections.Generic;

namespace DAL.Tests
{
    [TestClass]
    public class MoviesDAL_Tests
    {
        IMoviesDAL movies_dal { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            //Reset the database after all the tests:
            DatabaseReset.resetDatabase();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //Reset the database after all the tests:
            DatabaseReset.resetDatabase();
        }

        //Constructor:
        public MoviesDAL_Tests()
        {
            CRoot CompositionRoot = new CRoot("test");
            movies_dal = CompositionRoot.MoviesDAL;
        }

        [TestMethod]
        public void Insert()
        {
            IMovieMapper movie1 = movies_dal.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf.png" });
            Assert.IsNotNull(movie1);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlDALException))]
        public void Insert_with_empty_Title()
        {
            IMovieMapper movie1 = movies_dal.InsertMovie(new MovieMapper { Title = "", RunTime = 130, Image = "/path/to/image/bttf.png" });
        }

        [TestMethod]
        [ExpectedException(typeof(SqlDALException))]
        public void Insert_already_existing_Title()
        {
            IMovieMapper movie1 = movies_dal.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf.png" });
            IMovieMapper movie2 = movies_dal.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 160, Image = "/path/to/image/bttf2.png" });
        }

        [TestMethod]
        public void Get_Movie_by_ID()
        {
            //Create a new movie and get its Id:
            IMovieMapper movie1 = movies_dal.InsertMovie(new MovieMapper { Title = "The Matrix", RunTime = 130, Image = "/path/to/image/braveheart.png" });

            //Use the id to get the movie back out:
            IMovieMapper movie2 = movies_dal.Get_Movie_by_ID(new MovieMapper { Id = movie1.Id });

            Assert.IsNotNull(movie2);
        }

        [TestMethod]
        public void Update()
        {
            //Create a new movie and get its Id:
            IMovieMapper movie1 = movies_dal.InsertMovie(new MovieMapper { Title = "Braveheart", RunTime = 130, Image = "/path/to/image/braveheart.png" });

            //Update the movie:
            IMovieMapper updated_movie = movies_dal.Update(new MovieMapper { Id = movie1.Id, Title = "Braveheart 2: Still Can't Take Muh' Freedom!", RunTime = 300, Image = "/another/path/to/an/image.png" });

            //Get the movie out of the database:
            IMovieMapper foundMovie = movies_dal.Get_Movie_by_ID(new MovieMapper { Id = updated_movie.Id });

            Assert.AreEqual(updated_movie.Title, foundMovie.Title);
            Assert.AreEqual(updated_movie.RunTime, foundMovie.RunTime);
            Assert.AreEqual(updated_movie.Image, foundMovie.Image);
        }

        [TestMethod]
        public void Get_All_Movies()
        {
            //Create some movies:
            IMovieMapper movie1 = movies_dal.InsertMovie(new MovieMapper { Title = "The Matrix 2", RunTime = 130, Image = "/path/to/image/matrix2.png" });
            IMovieMapper movie2 = movies_dal.InsertMovie(new MovieMapper { Title = "The Matrix 3", RunTime = 130, Image = "/path/to/image/matrix3.png" });
            IMovieMapper movie3 = movies_dal.InsertMovie(new MovieMapper { Title = "The Animatrix", RunTime = 130, Image = "/path/to/image/matrix4.png" });

            //Get all the movies out of the database:
            List<IMovieMapper> all_movies = movies_dal.Get_All_Movies();

            Assert.IsTrue(all_movies.Count == 3);
        }

        [TestMethod]
        public void DeleteMovie()
        {
            //Create a movie:
            IMovieMapper movie1 = movies_dal.InsertMovie(new MovieMapper { Title = "Pulp Fiction", RunTime = 130, Image = "/path/to/image/pf.png" });

            //Delete the movie:
            Boolean deleted = movies_dal.DeleteMovie(new MovieMapper { Id = movie1.Id });

            Assert.IsTrue(deleted);

            //Use the id to try to get the movie back out (It should be deleted by now):
            IMovieMapper movie2 = movies_dal.Get_Movie_by_ID(new MovieMapper { Id = movie1.Id });

            Assert.IsNull(movie2);
        }

        [TestMethod]
        public void Get_ShowTimes_by_MovieID()
        {
            //Create a movie:
            IMovieMapper movie1 = movies_dal.InsertMovie(new MovieMapper { Title = "Kill Bill", RunTime = 130, Image = "/path/to/image/kb.png" });

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
            IShowTimesMapper showtime1 = movies_dal.InsertShowTime(movie1, new ShowTimesMapper { ShowingDateTime = new DateTime(2017, 01, 20, 13, 01, 05) });

            //Get the show times by movieId
            List<IShowTimesMapper> showtimes = movies_dal.Get_ShowTimes_by_MovieID(new MovieMapper { Id = movie1.Id });

            Assert.IsTrue(showtimes.Count == 1);
        }

        [TestMethod]
        public void InsertShowTime()
        {
            //Create a movie:
            IMovieMapper movie1 = movies_dal.InsertMovie(new MovieMapper { Title = "Pulp Fiction 2: Pulpier Fiction", RunTime = 130, Image = "/path/to/image/pf2.png" });

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
            IShowTimesMapper showtime1 = movies_dal.InsertShowTime(movie1, new ShowTimesMapper { ShowingDateTime = new DateTime(2017, 01, 20, 13, 01, 05) });
            Assert.IsNotNull(showtime1);

            //Get the show times by movieId
            List<IShowTimesMapper> showtimes = movies_dal.Get_ShowTimes_by_MovieID(new MovieMapper { Id = movie1.Id });

            Assert.IsTrue(showtimes.Count == 1);
        }

        //[TestMethod]
        //public void Search_Movie_by_Name()
        //{
        //    //Create some movies:
        //    IMovieMapper movie1 = movies_dal.InsertMovie(new MovieMapper { Title = "Kill Bill 2", RunTime = 130, Image = "/path/to/image/kb2.png" });
        //    IMovieMapper movie2 = movies_dal.InsertMovie(new MovieMapper { Title = "Another movie with kill in the title", RunTime = 130, Image = "/path/to/image/kadf.png" });

        //    System.Threading.Thread.Sleep(5000); //Need this because the Search_Movie_by_Name method takes time.

        //    List<IMovieMapper> searchResults = movies_dal.Search_Movie_by_Name("kill");
        //    Assert.IsTrue(searchResults.Count == 2);            
        //}

        [TestMethod]
        public void Search_Movie_by_Name_Like()
        {
            //Create some movies:
            IMovieMapper movie1 = movies_dal.InsertMovie(new MovieMapper { Title = "Kill Bill 2", RunTime = 130, Image = "/path/to/image/kb2.png" });
            IMovieMapper movie2 = movies_dal.InsertMovie(new MovieMapper { Title = "To Kill a Mockingbird", RunTime = 130, Image = "/path/to/image/kadf.png" });
            IMovieMapper movie3 = movies_dal.InsertMovie(new MovieMapper { Title = "Back to the Future", RunTime = 130, Image = "/path/to/image/bttf.png" });
            IMovieMapper movie4 = movies_dal.InsertMovie(new MovieMapper { Title = "Natural Born Killers", RunTime = 130, Image = "/path/to/image/bttf.png" });
            IMovieMapper movie5 = movies_dal.InsertMovie(new MovieMapper { Title = "Into The Wild", RunTime = 130, Image = "/path/to/image/itw.png" });

            //System.Threading.Thread.Sleep(5000); //Need this because the Search_Movie_by_Name method takes time.

            List<IMovieMapper> searchResults = movies_dal.Search_Movie_by_Name_Like("to the");
            Assert.IsTrue(searchResults.Count == 2);
        }

    }
}