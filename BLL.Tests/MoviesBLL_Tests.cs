using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BLL;
using CompositionRoot;
using SharedResources.Interfaces;
using SharedResources.Mappers;
using SharedResources.Exceptions.BLL;
using DAL;
using System.Collections.Generic; //Need this to get to the DatabaseReset class so we can reset database before/after each test.

namespace BLL.Tests
{
    [TestClass]
    public class MoviesBLL_Tests
    {
        IMoviesBLL movies_bll { get; set; }
        DatabaseReset dbReset { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            //Reset the database after all the tests:
            dbReset.resetDatabase();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //Reset the database after all the tests:
            dbReset.resetDatabase();
        }

        //Constructor:
        public MoviesBLL_Tests()
        {
            string environment = "test";
            CRoot CompositionRoot = new CRoot(environment);
            dbReset = new DatabaseReset(environment);

            movies_bll = CompositionRoot.MoviesBLL;
        }

        [TestMethod]
        public void InsertMovie()
        {
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
            Assert.IsNotNull(movie1);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlBLLException))]
        public void InsertMovie_existing_Title()
        {
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
            IMovieMapper movie2 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void InsertMovie_missing_Title()
        {
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "", RunTime = 130, Image = "/path/to/image/bttf2.png" });
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void InsertMovie_missing_Image()
        {
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "" });
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void InsertMovie_missing_RunTime()
        {
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 0, Image = "/path/to/image/bttf2.png" });
        }

        [TestMethod]
        public void Update()
        {
            //Insert a movie:
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
            IMovieMapper updated_info = new MovieMapper { Id = movie1.Id, Title = "Back To The Future III", RunTime = 135, Image = "/path/to/image/bttf2_large.png" };
            IMovieMapper updatedMovie = movies_bll.Update(updated_info);
            Assert.IsNotNull(updatedMovie);
        }

        [TestMethod]
        public void Update_nonexisting_movie()
        {
            //Try to update movie.ID = 10001 (doesn't exist):
            IMovieMapper updatedMovie = movies_bll.Update(new MovieMapper { Id = 10001, Title = "Back To The Future III", RunTime = 135, Image = "/path/to/image/bttf2_large.png" });
            Assert.IsNull(updatedMovie);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void Update_missing_Id()
        {
            IMovieMapper updatedMovie = movies_bll.Update(new MovieMapper { Id = 0, Title = "Back To The Future III", RunTime = 135, Image = "/path/to/image/bttf2_large.png" });
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void Update_missing_Title()
        {
            //Insert a movie:
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
            IMovieMapper updatedMovie = movies_bll.Update(new MovieMapper { Id = movie1.Id, Title = "", RunTime = 135, Image = "/path/to/image/bttf2_large.png" });
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void Update_missing_RunTime()
        {
            //Insert a movie:
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
            IMovieMapper updatedMovie = movies_bll.Update(new MovieMapper { Id = movie1.Id, Title = "Back To the Future II", RunTime = 0, Image = "/path/to/image/bttf2_large.png" });
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void Update_missing_Image()
        {
            //Insert a movie:
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
            IMovieMapper updatedMovie = movies_bll.Update(new MovieMapper { Id = movie1.Id, Title = "Back To the Future II", RunTime = 130, Image = "" });
        }

        [TestMethod]
        public void Get_Movie_by_ID()
        {
            //Insert a movie:
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
            IMovieMapper found_movie = movies_bll.Get_Movie_by_ID(movie1);
            Assert.AreEqual(movie1.Title, found_movie.Title);
            Assert.AreEqual(movie1.RunTime, found_movie.RunTime);
            Assert.AreEqual(movie1.Image, found_movie.Image);
            Assert.AreEqual(movie1.Id, found_movie.Id);
        }

        [TestMethod]
        public void Get_Movie_by_ID_nonexisting_movieID()
        {
            IMovieMapper found_movie = movies_bll.Get_Movie_by_ID(new MovieMapper { Id = 10001 });
            Assert.IsNull(found_movie);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void Get_Movie_by_ID_missing_movieID()
        {
            IMovieMapper found_movie = movies_bll.Get_Movie_by_ID(new MovieMapper { Id = 0 });
        }

        [TestMethod]
        public void Get_All_Movies()
        {
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future I", RunTime = 130, Image = "/path/to/image/bttf1.png" });
            IMovieMapper movie2 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
            IMovieMapper movie3 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future III", RunTime = 130, Image = "/path/to/image/bttf3.png" });
            List<IMovieMapper> found_movies = movies_bll.Get_All_Movies();
            Assert.AreEqual(found_movies.Count, 3);
        }

        [TestMethod]
        public void Get_All_Movies_none_found()
        {
            List<IMovieMapper> found_movies = movies_bll.Get_All_Movies();
            Assert.AreEqual(found_movies.Count, 0);
        }

        [TestMethod]
        public void DeleteMovie()
        {
            //Insert a movie:
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
            bool deleted = movies_bll.DeleteMovie(movie1);
            Assert.IsTrue(deleted);
        }

        [TestMethod]
        public void InsertShowTime()
        {
            //Insert a movie:
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
            IShowTimesMapper showtime1 = new ShowTimesMapper { ShowingDateTime = new DateTime(2017, 01, 20, 13, 01, 05) };
            IShowTimesMapper returned_showtime = movies_bll.InsertShowTime(movie1, showtime1);
            Assert.IsNotNull(returned_showtime);
            Assert.IsTrue(returned_showtime.Id > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlBLLException))]
        public void InsertShowTime_nonexisting_movieId()
        {
            IMovieMapper movie1 = new MovieMapper { Id = 10001 };
            IShowTimesMapper showtime1 = new ShowTimesMapper { ShowingDateTime = new DateTime(2017, 01, 20, 13, 01, 05) };
            IShowTimesMapper returned_showtime = movies_bll.InsertShowTime(movie1, showtime1);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void InsertShowTime_missing_movieId()
        {
            IMovieMapper movie1 = new MovieMapper { Id = 0 };
            IShowTimesMapper showtime1 = new ShowTimesMapper { ShowingDateTime = new DateTime(2017, 01, 20, 13, 01, 05) };
            IShowTimesMapper returned_showtime = movies_bll.InsertShowTime(movie1, showtime1);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void InsertShowTime_missing_ShowingDateTime()
        {
            //Insert a movie:
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
            IShowTimesMapper showtime1 = new ShowTimesMapper { ShowingDateTime = new DateTime() }; //Try to insert minValue DateTime (empty datetime).
            IShowTimesMapper returned_showtime = movies_bll.InsertShowTime(movie1, showtime1);
        }

        [TestMethod]
        public void Get_ShowTimes_by_MovieID()
        {
            //Insert a movie:
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
            //Create some showtimes:
            IShowTimesMapper showtime1 = new ShowTimesMapper { ShowingDateTime = new DateTime(2017, 01, 20, 13, 01, 05) };
            IShowTimesMapper showtime2 = new ShowTimesMapper { ShowingDateTime = new DateTime(2016, 01, 20, 13, 01, 05) };
            IShowTimesMapper showtime3 = new ShowTimesMapper { ShowingDateTime = new DateTime(2015, 01, 20, 13, 01, 05) };
            //Insert the showtimes:
            movies_bll.InsertShowTime(movie1, showtime1);
            movies_bll.InsertShowTime(movie1, showtime2);
            movies_bll.InsertShowTime(movie1, showtime3);
            //Get the showtimes back out of the database:
            List<IShowTimesMapper> found_showtimes = movies_bll.Get_ShowTimes_by_MovieID(movie1);
            //Assert
            Assert.IsTrue(found_showtimes.Count == 3);
        }

        [TestMethod]
        public void Get_ShowTimes_by_MovieID_nonexisting_MovieID()
        {
            List<IShowTimesMapper> found_showtimes = movies_bll.Get_ShowTimes_by_MovieID(new MovieMapper { Id = 10001 });
            //Assert
            Assert.IsTrue(found_showtimes.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void Get_ShowTimes_by_MovieID_missing_MovieID()
        {
            List<IShowTimesMapper> found_showtimes = movies_bll.Get_ShowTimes_by_MovieID(new MovieMapper { Id = 0 });
        }

        //[TestMethod]
        //public void Search_Movie_by_Name()
        //{
        //    //Insert some movies:
        //    IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future I", RunTime = 130, Image = "/path/to/image/bttf1.png" });
        //    IMovieMapper movie2 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
        //    IMovieMapper movie3 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future III", RunTime = 130, Image = "/path/to/image/bttf3.png" });
        //    IMovieMapper movie4 = movies_bll.InsertMovie(new MovieMapper { Title = "Future World", RunTime = 130, Image = "/path/to/image/fw.png" });
        //    //Insert some movies without the word "future":
        //    IMovieMapper movie5 = movies_bll.InsertMovie(new MovieMapper { Title = "The Matrix", RunTime = 130, Image = "/path/to/image/matrix.png" });
        //    IMovieMapper movie6 = movies_bll.InsertMovie(new MovieMapper { Title = "Braveheart", RunTime = 130, Image = "/path/to/image/bh.png" });

        //    System.Threading.Thread.Sleep(7000); //Need this because the Search_Movie_by_Name method takes time.

        //    //Get the showtimes back out of the database:
        //    List<IMovieMapper> found_movies = movies_bll.Search_Movie_by_Name("future");
        //    //Assert
        //    Assert.IsTrue(found_movies.Count == 4);
        //}

        //[TestMethod]
        //public void Search_Movie_by_Name_nonexisting_Name()
        //{
        //    //Insert some movies:
        //    IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future I", RunTime = 130, Image = "/path/to/image/bttf1.png" });
        //    IMovieMapper movie2 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
        //    IMovieMapper movie3 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future III", RunTime = 130, Image = "/path/to/image/bttf3.png" });
        //    IMovieMapper movie4 = movies_bll.InsertMovie(new MovieMapper { Title = "Future World", RunTime = 130, Image = "/path/to/image/fw.png" });
        //    //Insert some movies without the word "future":
        //    IMovieMapper movie5 = movies_bll.InsertMovie(new MovieMapper { Title = "The Matrix", RunTime = 130, Image = "/path/to/image/matrix.png" });
        //    IMovieMapper movie6 = movies_bll.InsertMovie(new MovieMapper { Title = "Braveheart", RunTime = 130, Image = "/path/to/image/bh.png" });

        //    System.Threading.Thread.Sleep(5000); //Need this because the Search_Movie_by_Name method takes time.

        //    //Get the showtimes back out of the database:
        //    List<IMovieMapper> found_movies = movies_bll.Search_Movie_by_Name("blahyadablah");
        //    //Assert
        //    Assert.IsTrue(found_movies.Count == 0);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(MissingDataBLLException))]
        //public void Search_Movie_by_Name_missing_Name()
        //{
        //    //Insert some movies:
        //    IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future I", RunTime = 130, Image = "/path/to/image/bttf1.png" });
        //    IMovieMapper movie2 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
        //    IMovieMapper movie3 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future III", RunTime = 130, Image = "/path/to/image/bttf3.png" });
        //    IMovieMapper movie4 = movies_bll.InsertMovie(new MovieMapper { Title = "Future World", RunTime = 130, Image = "/path/to/image/fw.png" });
        //    //Insert some movies without the word "future":
        //    IMovieMapper movie5 = movies_bll.InsertMovie(new MovieMapper { Title = "The Matrix", RunTime = 130, Image = "/path/to/image/matrix.png" });
        //    IMovieMapper movie6 = movies_bll.InsertMovie(new MovieMapper { Title = "Braveheart", RunTime = 130, Image = "/path/to/image/bh.png" });

        //    System.Threading.Thread.Sleep(5000); //Need this because the Search_Movie_by_Name method takes time.

        //    //Get the showtimes back out of the database:
        //    List<IMovieMapper> found_movies = movies_bll.Search_Movie_by_Name("");
        //    //Assert
        //    Assert.IsTrue(found_movies.Count == 0);
        //}

        //Test the Search_Movie_by_Name_Like method:
        [TestMethod]
        public void Search_Movie_by_Name_Like()
        {
            //Insert some movies:
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future I", RunTime = 130, Image = "/path/to/image/bttf1.png" });
            IMovieMapper movie2 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
            IMovieMapper movie3 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future III", RunTime = 130, Image = "/path/to/image/bttf3.png" });
            IMovieMapper movie4 = movies_bll.InsertMovie(new MovieMapper { Title = "Future World", RunTime = 130, Image = "/path/to/image/fw.png" });
            //Insert some movies without the word "future":
            IMovieMapper movie5 = movies_bll.InsertMovie(new MovieMapper { Title = "The Matrix", RunTime = 130, Image = "/path/to/image/matrix.png" });
            IMovieMapper movie6 = movies_bll.InsertMovie(new MovieMapper { Title = "Braveheart", RunTime = 130, Image = "/path/to/image/bh.png" });

            //System.Threading.Thread.Sleep(7000); //Need this because the Search_Movie_by_Name method takes time.

            //Get the showtimes back out of the database:
            List<IMovieMapper> found_movies = movies_bll.Search_Movie_by_Name_Like("future");
            //Assert
            Assert.IsTrue(found_movies.Count == 4);
        }

        [TestMethod]
        public void Search_Movie_by_Name_Like_nonexisting_Name()
        {
            //Insert some movies:
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future I", RunTime = 130, Image = "/path/to/image/bttf1.png" });
            IMovieMapper movie2 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
            IMovieMapper movie3 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future III", RunTime = 130, Image = "/path/to/image/bttf3.png" });
            IMovieMapper movie4 = movies_bll.InsertMovie(new MovieMapper { Title = "Future World", RunTime = 130, Image = "/path/to/image/fw.png" });
            //Insert some movies without the word "future":
            IMovieMapper movie5 = movies_bll.InsertMovie(new MovieMapper { Title = "The Matrix", RunTime = 130, Image = "/path/to/image/matrix.png" });
            IMovieMapper movie6 = movies_bll.InsertMovie(new MovieMapper { Title = "Braveheart", RunTime = 130, Image = "/path/to/image/bh.png" });

            //System.Threading.Thread.Sleep(5000); //Need this because the Search_Movie_by_Name method takes time.

            //Get the showtimes back out of the database:
            List<IMovieMapper> found_movies = movies_bll.Search_Movie_by_Name_Like("blahyadablah");
            //Assert
            Assert.IsTrue(found_movies.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void Search_Movie_by_Name_Like_missing_Name()
        {
            //Insert some movies:
            IMovieMapper movie1 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future I", RunTime = 130, Image = "/path/to/image/bttf1.png" });
            IMovieMapper movie2 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future II", RunTime = 130, Image = "/path/to/image/bttf2.png" });
            IMovieMapper movie3 = movies_bll.InsertMovie(new MovieMapper { Title = "Back To the Future III", RunTime = 130, Image = "/path/to/image/bttf3.png" });
            IMovieMapper movie4 = movies_bll.InsertMovie(new MovieMapper { Title = "Future World", RunTime = 130, Image = "/path/to/image/fw.png" });
            //Insert some movies without the word "future":
            IMovieMapper movie5 = movies_bll.InsertMovie(new MovieMapper { Title = "The Matrix", RunTime = 130, Image = "/path/to/image/matrix.png" });
            IMovieMapper movie6 = movies_bll.InsertMovie(new MovieMapper { Title = "Braveheart", RunTime = 130, Image = "/path/to/image/bh.png" });

            //System.Threading.Thread.Sleep(5000); //Need this because the Search_Movie_by_Name method takes time.

            //Get the showtimes back out of the database:
            List<IMovieMapper> found_movies = movies_bll.Search_Movie_by_Name_Like("");
            //Assert
            Assert.IsTrue(found_movies.Count == 0);
        }

    }
}
