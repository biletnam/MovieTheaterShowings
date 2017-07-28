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
    public class UsersBLL_Tests
    {

        IUsersBLL users_bll { get; set; }
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
        public UsersBLL_Tests()
        {
            string environment = "test";
            CRoot CompositionRoot = new CRoot(environment);

            dbReset = new DatabaseReset(environment);

            users_bll = CompositionRoot.UsersBLL;
        }

        [TestMethod]
        public void Insert()
        {
            IUserMapper user = users_bll.Insert(new UserMapper { Name = "trump", RoleName = "user", password_hash = "thebestpassword" });
            Assert.IsNotNull(user);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlBLLException))]
        public void Insert_with_existing_Name()
        {
            IUserMapper user = users_bll.Insert(new UserMapper { Name = "trump", RoleName = "user", password_hash = "thebestpassword" });
            IUserMapper user1 = users_bll.Insert(new UserMapper { Name = "trump", RoleName = "user", password_hash = "thebestpassword2" });
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void Insert_with_missing_Name()
        {
            IUserMapper user = users_bll.Insert(new UserMapper { Name = "", RoleName = "user", password_hash = "thebestpassword" });
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void Insert_with_missing_RoleName()
        {
            IUserMapper user = users_bll.Insert(new UserMapper { Name = "trump", RoleName = "", password_hash = "thebestpassword" });
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void Insert_with_missing_password_hash()
        {
            IUserMapper user = users_bll.Insert(new UserMapper { Name = "trump", RoleName = "user", password_hash = "" });
        }

        [TestMethod]
        public void Get_User_by_User_Name()
        {
            //Create a new user:
            IUserMapper user = users_bll.Insert(new UserMapper { Name = "comey", RoleName = "user", password_hash = "hillaryemails" });
            //Get the user back out:
            IUserMapper foundUser = users_bll.Get_User_by_User_Name(new UserMapper { Name = "comey" });
            Assert.IsNotNull(foundUser);
        }

        [TestMethod]
        public void Get_User_by_User_Name_unknown_Name()
        {
            IUserMapper foundUser = users_bll.Get_User_by_User_Name(new UserMapper { Name = "bobdole" });
            Assert.IsNull(foundUser);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void Get_User_by_User_Name_missing_Name()
        {
            IUserMapper foundUser = users_bll.Get_User_by_User_Name(new UserMapper { Name = "" });
        }

        [TestMethod]
        public void authenticate_user()
        {
            //Create a new user:
            IUserMapper user = users_bll.Insert(new UserMapper { Name = "trump", RoleName = "user", password_hash = "thebestpassword" });
            //Authenticate user:
            bool authentic = users_bll.authenticate_user(new UserMapper { Name = "trump", password_hash = "thebestpassword" });
            Assert.IsTrue(authentic);
        }

        [TestMethod]
        public void authenticate_user_bad_password()
        {
            //Create a new user:
            IUserMapper user = users_bll.Insert(new UserMapper { Name = "trump", RoleName = "user", password_hash = "thebestpassword" });
            //Authenticate user:
            bool authentic = users_bll.authenticate_user(new UserMapper { Name = "trump", password_hash = "iloveputin" });
            Assert.IsTrue(!authentic);
        }

        [TestMethod]
        public void authenticate_user_unknown_Name()
        {
            //Authenticate user:
            bool authentic = users_bll.authenticate_user(new UserMapper { Name = "bobdole", password_hash = "bobdolebobdole" });
            Assert.IsTrue(!authentic);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void authenticate_user_missing_Name()
        {
            //Authenticate user:
            bool authentic = users_bll.authenticate_user(new UserMapper { Name = "", password_hash = "bobdolebobdole" });
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDataBLLException))]
        public void authenticate_user_missing_password_hash()
        {
            //Authenticate user:
            bool authentic = users_bll.authenticate_user(new UserMapper { Name = "trump", password_hash = "" });
        }

        [TestMethod]
        public void Get_All_Users()
        {
            IUserMapper user1 = users_bll.Insert(new UserMapper { Name = "Trump", RoleName = "user", password_hash = "thebestpassword" });
            IUserMapper user2 = users_bll.Insert(new UserMapper { Name = "Melania", RoleName = "user", password_hash = "michelleObama" });
            IUserMapper user3 = users_bll.Insert(new UserMapper { Name = "Ivanka", RoleName = "user", password_hash = "chineseproducts" });
            List<IUserMapper> found_movies = users_bll.Get_All_Users();
            Assert.AreEqual(found_movies.Count, 4);
        }

        [TestMethod]
        public void Get_All_Users_only_admin_exists()
        {
            List<IUserMapper> found_users = users_bll.Get_All_Users();
            Assert.AreEqual(found_users.Count, 1);
        }



    }
}
