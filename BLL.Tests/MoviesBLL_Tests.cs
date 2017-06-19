using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BLL;
using CompositionRoot;
using SharedResources.Interfaces;
using SharedResources.Mappers;
using SharedResources.Exceptions.BLL;
using DAL; //Need this to get to the DatabaseReset class so we can reset database before/after each test.

namespace BLL.Tests
{
    [TestClass]
    public class MoviesBLL_Tests
    {
        IMoviesBLL movies_bll { get; set; }

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
        public MoviesBLL_Tests()
        {
            CRoot CompositionRoot = new CRoot("test");
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

    }
}
