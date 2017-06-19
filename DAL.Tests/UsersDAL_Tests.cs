using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DAL;
using CompositionRoot;
using SharedResources.Interfaces;
using SharedResources.Mappers;
using SharedResources.Exceptions.DAL;

namespace DAL.Tests
{
    [TestClass]
    public class UsersDAL_Tests
    {
        IUsersDAL users_dal { get; set; }

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
        public UsersDAL_Tests()
        {
            CRoot CompositionRoot = new CRoot("test");
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
    }
}