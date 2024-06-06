using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")] 
    [ApiController] 
    [Authorize] 
    public class ZoosController : ControllerBase
    {
        IAppDbContext context;

        public ZoosController(IAppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(context.Zoos.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Zoo? zoo = context.Zoos
                .Include(x => x.Location) 
                .Include(x => x.Animals) 
                .FirstOrDefault(z => z.Id == id);

            if (zoo is not null)
                return Ok(zoo);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Post(Zoo zoo)
        {
            context.Zoos.Add(zoo);
            context.SaveChanges();
            return Ok(zoo);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Zoo input)
        {
            Zoo? existing = context.Zoos.FirstOrDefault(z => z.Id == id);
            if (existing is null)
                return NoContent();

            existing.Name = input.Name;
            existing.Location = input.Location;
            existing.LocationId = input.LocationId;
            existing.Animals = input.Animals;
            context.SaveChanges();

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Zoo? zoo = context.Zoos.FirstOrDefault(z => z.Id == id);
            if (zoo is null)
                return NotFound();

            context.Zoos.Remove(zoo);
            context.SaveChanges();

            return Ok();
        }
    }
}
