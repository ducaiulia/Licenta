using System;
using System.Data.Entity;
using System.Threading;
using AllShare.Core.Domain;
using AllShare.Infrastructure.DatabaseEngine;
using AllShare.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllShare.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private UserRepository repo;

        [TestMethod]
        public void TestMethod1()
        {
            var jobRepo = new JobRepository();
            var userRepo = new UserRepository();
            //var user = userRepo.GetUser(1);
            //var user2 = userRepo.GetUser("test");
            //jobRepo.Add(new SharePostJobModel
            //{
            //    DateTime = DateTime.Now,
            //    ImagePath = "http://res.cloudinary.com/djwta3alu/image/upload/v1464283802/jtwf1nhppuhilnzdhnwd.jpg",
            //    IsFacebook = true,
            //    IsFile = true,
            //    Text = "TEST",
            //    UserId = 1,
            //    User = userRepo.GetUser(1),
            //    ToBeRunAt = new DateTime(2016, 5, 26, 22, 20, 00)
            //});
        }
    }
}
