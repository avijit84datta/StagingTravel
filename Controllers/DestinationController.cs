using CoreApiFirst.DBContext;
using CoreApiFirst.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreApiFirst.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DestinationController : ControllerBase
    {
        private readonly TravelDbContext _context;

        public DestinationController(TravelDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDestinations()
        {
            var destinations = await _context.Destinations.ToListAsync();
            return Ok(destinations);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDestination(Destination destination)
        {
            _context.Destinations.Add(destination);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDestinations), new { id = destination.Id }, destination);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDestination(int Id)
        {
            var destination = await _context.Destinations.FirstOrDefaultAsync(x => x.Id == Id);
            if (destination == null) 
                return NotFound();

            _context.Destinations.Remove(destination);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDestination(int id, Destination updatedDestination)
        {
            if (id != updatedDestination.Id)
                return BadRequest("Destination ID mismatch");

            var destination = await _context.Destinations.FirstOrDefaultAsync(x => x.Id == id);
            if (destination == null)
                return NotFound();

            // Update destination properties
            destination.Name = updatedDestination.Name;
            destination.Description = updatedDestination.Description;
            destination.Country = updatedDestination.Country;

            // Save changes to the database
            _context.Entry(destination).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
