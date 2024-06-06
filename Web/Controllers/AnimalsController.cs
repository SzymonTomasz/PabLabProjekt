using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AnimalsController : ControllerBase
    {
        IAppDbContext context;

        public AnimalsController(IAppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(context.Animals.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Animal? animal = context.Animals.FirstOrDefault(z => z.Id == id);
            if (animal is not null)
                return Ok(animal);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Post(Animal animal)
        {
            context.Animals.Add(animal);
            context.SaveChanges();
            return Ok(animal);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Animal input)
        {
            Animal? existing = context.Animals.FirstOrDefault(z => z.Id == id);
            if (existing is null)
                return NoContent();

            existing.Name = input.Name;
            context.SaveChanges();

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Animal? animal = context.Animals.FirstOrDefault(z => z.Id == id);
            if (animal is null)
                return NotFound();

            context.Animals.Remove(animal);
            context.SaveChanges();

            return Ok();
        }
    }
}
