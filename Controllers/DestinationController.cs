using CoreApiFirst.DBContext;
using CoreApiFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreApiFirst.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
    }
}
