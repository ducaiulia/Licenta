using System;
using System.Data.Entity;
using AllShare.Infrastructure.DatabaseEngine;
using AllShare.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllShare.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private UserRepository repo;

        [TestInitialize]
        public void TestInitialize()
        {
            var db = new InitializeDatabase();
            Database.SetInitializer(db);
            repo = new UserRepository();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var list = repo.GetAll();
            var nr = list.Count;
        }
    }
}
