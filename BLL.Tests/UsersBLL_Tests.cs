using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BLL;
using CompositionRoot;
using SharedResources.Interfaces;
using SharedResources.Mappers;
using SharedResources.Exceptions.BLL;
using SharedResources.Exceptions.DAL;
using DAL;

namespace BLL.Tests
{
    [TestClass]
    public class UsersBLL_Tests
    {

        IUsersBLL users_bll { get; set; }

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
        public UsersBLL_Tests()
        {
            CRoot CompositionRoot = new CRoot("test");
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

    }
}
