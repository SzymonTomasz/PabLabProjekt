using Application.Interfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Controllers;

namespace ControllersTests
{
    [TestClass]
    public class AnimalsControllerTests
    {
        IAppDbContext context;
        // Konstruktor, który tworzy nową bazę danych w pamięci, dodaje do niej jedno zwierzę i zapisuje zmiany.
        public AnimalsControllerTests()
        {
            // Tworzymy opcje do bazy danych w pamięci. Robimy to na Guid w celu uniknięcia konfliktów nazw baz danych.
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            // Tworzymy nowy kontekst bazy danych przy użyciu powyżej stworzonych opcji.
            context = new AppDbContext(options);

            // Dodajemy do bazy danych jedno zwierzę.
            context.Animals.Add(new Animal { Name = "Pies" });
            // Zapisujemy zmiany.
            context.SaveChanges();

            // Przypisujemy nowy kontekst do zmiennej.
            context = new AppDbContext(options);
        }

        // Test metody Get, która zwraca listę zwierząt.
        [TestMethod]
        public void GetMethodTest()
        {
            // Utowrzenie obiektu kontrolera.
            var testObject = new AnimalsController(context);
            // Wywołanie metody Get.
            var result = testObject.Get();
            // rzutowanie wyniku na OkObjectResult w celu sprawdzenia statusu i zawartości.
            var objectResult = result as OkObjectResult;

            // Sprawdzenie czy wynik jest różny od null.
            Assert.IsNotNull(objectResult);
            // Sprawdzenie czy status kod wyniku to 200.
            Assert.AreEqual(200, objectResult.StatusCode);
            // Sprawdzenie czy lista zwierząt zawiera jedno zwierzę.
            Assert.AreEqual(1, (objectResult.Value as List<Animal>).Count);
        }

        [TestMethod]
        public void GetWithIdMethodTest()
        {
            var testObject = new AnimalsController(context);
            var result = testObject.Get(1);
            var objectResult = result as OkObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual("Pies", (objectResult.Value as Animal).Name);
        }

        // Do metody wrzuciłem testy dla metod Delete i Put.
        // Nie wchodząc w szczegóły zrobienie tego oddzielnie wiązałoby się z zabawą w kolejnośc wykonywania testów itd, czyli grubszy temat.
        // Jakbyś chciał to zrobić to daj znać, zrefaktoruje.
        [TestMethod]
        public void PostAndDeleteMethodTest()
        {
            var testObject = new AnimalsController(context);

            // Sprawdzenie czy w bazie znajduje się jedno zwierzę.
            var result = testObject.Get();
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(1, (objectResult.Value as List<Animal>).Count);


            // Dodanie nowego zwierzęcia do bazy.
            result = testObject.Post(new Animal { Name = "Ryś" });
            objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);

            // Sprawdzenie czy w bazie znajdują się dwa zwierzęta.
            result = testObject.Get();
            objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(2, (objectResult.Value as List<Animal>).Count);

            // Usunięcie jednego zwierzęcia.
            result = testObject.Delete(2);
            var castedResult = result as OkResult;
            Assert.IsNotNull(castedResult);
            Assert.AreEqual(200, castedResult.StatusCode);

            // Sprawdzenie czy w bazie ponownie znajduje się tylko jedno zwierzę.
            result = testObject.Get();
            objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(1, (objectResult.Value as List<Animal>).Count);
        }

        [TestMethod]
        public void PutMethodTest()
        {
            var testObject = new AnimalsController(context);
            var result = testObject.Put(1, new Animal { Name = "Ryś" });
            var objectResult = result as OkObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual("Ryś", (objectResult.Value as Animal).Name);
        }
    }
}