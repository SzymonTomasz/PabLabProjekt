using Application.Interfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Controllers;

namespace ControllersTests
{
    [TestClass]
    public class LocationsControllerTests
    {
        IAppDbContext context;
        public LocationsControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            context = new AppDbContext(options);

            context.Locations.Add(new Location { Name = "Kraków" });
            context.SaveChanges();

            context = new AppDbContext(options);
        }

        [TestMethod]
        public void GetMethodTest()
        {
            var testObject = new LocationsController(context);
            var result = testObject.Get();
            var objectResult = result as OkObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(1, (objectResult.Value as List<Location>).Count);
        }

        [TestMethod]
        public void GetWithIdMethodTest()
        {
            var testObject = new LocationsController(context);
            var result = testObject.Get(1);
            var objectResult = result as OkObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual("Kraków", (objectResult.Value as Location).Name);
        }

        [TestMethod]
        public void PostAndDeleteMethodTest()
        {
            var testObject = new LocationsController(context);

            var result = testObject.Get();
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(1, (objectResult.Value as List<Location>).Count);


            result = testObject.Post(new Location { Name = "Wroc³aw" });
            objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);

            result = testObject.Get();
            objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(2, (objectResult.Value as List<Location>).Count);

            result = testObject.Delete(2);
            var castedResult = result as OkResult;
            Assert.IsNotNull(castedResult);
            Assert.AreEqual(200, castedResult.StatusCode);

            result = testObject.Get();
            objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(1, (objectResult.Value as List<Location>).Count);
        }

        [TestMethod]
        public void PutMethodTest()
        {
            var testObject = new LocationsController(context);
            var result = testObject.Put(1, new Location { Name = "Wroc³aw" });
            var objectResult = result as OkObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual("Wroc³aw", (objectResult.Value as Location).Name);
        }
    }
}