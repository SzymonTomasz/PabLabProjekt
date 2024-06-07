using Application.Interfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Controllers;

namespace ControllersTests
{
    [TestClass]
    public class ZoosControllerTests
    {
        IAppDbContext context;
        public ZoosControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            context = new AppDbContext(options);

            context.Locations.Add(new Location { Name = "Kraków" });
            context.SaveChanges();

            context.Zoos.Add(new Zoo { Name = "TestZoo", Description = "Opis Zoo", LocationId = 1 });
            context.SaveChanges();

            context = new AppDbContext(options);
        }

        [TestMethod]
        public void GetMethodTest()
        {
            var testObject = new ZoosController(context);
            var result = testObject.Get();
            var objectResult = result as OkObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(1, (objectResult.Value as List<Zoo>).Count);
        }

        [TestMethod]
        public void GetWithIdMethodTest()
        {
            var testObject = new ZoosController(context);
            var result = testObject.Get(1);
            var objectResult = result as OkObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual("TestZoo", (objectResult.Value as Zoo).Name);
        }

        [TestMethod]
        public void PostAndDeleteMethodTest()
        {
            var testObject = new ZoosController(context);

            var result = testObject.Get();
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(1, (objectResult.Value as List<Zoo>).Count);


            result = testObject.Post(new Zoo { Name = "InneZoo", Description = "Opis Innego Zoo", LocationId = 1 });
            objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);

            result = testObject.Get();
            objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(2, (objectResult.Value as List<Zoo>).Count);

            result = testObject.Delete(2);
            var castedResult = result as OkResult;
            Assert.IsNotNull(castedResult);
            Assert.AreEqual(200, castedResult.StatusCode);

            result = testObject.Get();
            objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(1, (objectResult.Value as List<Zoo>).Count);
        }

        [TestMethod]
        public void PutMethodTest()
        {
            var testObject = new ZoosController(context);
            var result = testObject.Put(1, new Zoo { Name = "InneZoo" });
            var objectResult = result as OkObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual("InneZoo", (objectResult.Value as Zoo).Name);
        }
    }
}