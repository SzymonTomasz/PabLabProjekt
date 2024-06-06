using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class LocationsController : ControllerBase
    {
        IAppDbContext context;

        public LocationsController(IAppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(context.Locations.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Location? location = context.Locations.FirstOrDefault(z => z.Id == id);
            if (location is not null)
                return Ok(location);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Post(Location location)
        {
            context.Locations.Add(location);
            context.SaveChanges();
            return Ok(location);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Location input)
        {
            Location? existing = context.Locations.FirstOrDefault(z => z.Id == id);
            if (existing is null)
                return NoContent();

            existing.Name = input.Name;
            context.SaveChanges();

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Location? location = context.Locations.FirstOrDefault(z => z.Id == id);
            if (location is null)
                return NotFound();

            context.Locations.Remove(location);
            context.SaveChanges();

            return Ok();
        }
    }
}
