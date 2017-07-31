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
    public class UsersDAL_Tests
    {
        IUsersDAL users_dal { get; set; }
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
        public UsersDAL_Tests()
        {
            string environment = "test";
            CRoot CompositionRoot = new CRoot(environment);
            dbReset = new DatabaseReset(environment);

            users_dal = CompositionRoot.UsersDAL;
        }

        [TestMethod]
        public void Insert()
        {
            IUserMapper user = users_dal.Insert(new UserMapper { Name = "trump", RoleName = "user", password_hash = "thebestpassword" });
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void Get_User_by_User_Name()
        {
            //Create a new user:
            IUserMapper user = users_dal.Insert(new UserMapper { Name = "comey", RoleName = "user", password_hash = "hillarysemail" });
            //Get the user back out:
            IUserMapper foundUser = users_dal.Get_User_by_User_Name(new UserMapper { Name = "comey" });
            Assert.IsNotNull(foundUser);
        }

        [TestMethod]
        public void Get_User_by_User_Name_with_unknown_user()
        {
            IUserMapper user = users_dal.Get_User_by_User_Name(new UserMapper { Name = "hillary" });
            Assert.IsNull(user);
        }

        [TestMethod]
        public void Get_User_by_User_Name_with_emtpy_name()
        {
            IUserMapper user = users_dal.Get_User_by_User_Name(new UserMapper { Name = "" });
            Assert.IsNull(user);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlDALException))]
        public void Insert_with_emtpy_Name()
        {
            IUserMapper user = users_dal.Insert(new UserMapper { Name = "", RoleName = "user", password_hash = "thebestpassword" });
        }

        [TestMethod]
        [ExpectedException(typeof(SqlDALException))]
        public void Insert_with_emtpy_RoleName()
        {
            IUserMapper user = users_dal.Insert(new UserMapper { Name = "hillary", RoleName = "", password_hash = "thebestpassword" });
        }

        [TestMethod]
        [ExpectedException(typeof(SqlDALException))]
        public void Insert_with_emtpy_password_hash()
        {
            IUserMapper user = users_dal.Insert(new UserMapper { Name = "hillary", RoleName = "user", password_hash = "" });
        }

        [TestMethod]
        [ExpectedException(typeof(SqlDALException))]
        public void Insert_with_non_unique_Name()
        {
            IUserMapper user = users_dal.Insert(new UserMapper { Name = "trump", RoleName = "user", password_hash = "thebestpassword" });
            IUserMapper user2 = users_dal.Insert(new UserMapper { Name = "trump", RoleName = "user", password_hash = "thebestpassword2" });
        }

        [TestMethod]
        public void Get_All_Users()
        {
            //Create some movies:
            IUserMapper user1 = users_dal.Insert(new UserMapper { Name = "Jon Snow", RoleName = "user", password_hash = "thebestpassword" });
            IUserMapper user2 = users_dal.Insert(new UserMapper { Name = "Trump", RoleName = "user", password_hash = "thebestpassword" });
            IUserMapper user3 = users_dal.Insert(new UserMapper { Name = "Hillary", RoleName = "user", password_hash = "benghazi" });

            //Get all the movies out of the database:
            List<IUserMapper> all_users = users_dal.Get_All_Users();

            Assert.IsTrue(all_users.Count == 4);
        }

        [TestMethod]
        public void Get_User_by_Id()
        {
            IUserMapper user1 = users_dal.Get_User_by_Id(new UserMapper{ Id = 1, RoleName = "", password_hash = "" });
            Assert.IsTrue(user1.Name == "admin");
        }

        [TestMethod]
        public void Get_User_by_Id_bad_Id()
        {
            IUserMapper user1 = users_dal.Get_User_by_Id(new UserMapper { Id = 666, RoleName = "", password_hash = "" });
            Assert.IsNull(user1);
        }


    }
}