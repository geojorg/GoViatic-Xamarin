using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GoViatic.Common.Models;
using GoViatic.Web.Data;
using GoViatic.Web.Data.Entities;
using GoViatic.Web.Helpers;
using Microsoft.EntityFrameworkCore;

namespace GoViatic.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TripsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public TripsController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        [HttpPost]
        public async Task<IActionResult> PostTrip([FromBody]TripRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var traveler = await _context.Travelers.FindAsync(request.TravelerId);
            if (traveler == null)
            {
                return BadRequest("Not valid traveler.");
            }
            
            var trip = new Trip
            {
                City = request.City,
                Budget = request.Budget,
                Date = request.Date.ToUniversalTime(),
                EndDate = request.EndDate.ToUniversalTime(),
                Traveler = traveler
            };
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
            return Ok(_converterHelper.ToTripResponse(trip));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrip([FromRoute] int id, [FromBody] TripRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.Id)
            {
                return BadRequest();
            }

            var oldTrip = await _context.Trips.FindAsync(request.Id);
            if (oldTrip == null)
            {
                return BadRequest("Trip doesn't exists.");
            }

            oldTrip.City = request.City;
            oldTrip.Budget = request.Budget;
            oldTrip.Date = request.Date.ToUniversalTime();
            oldTrip.EndDate = request.EndDate.ToUniversalTime();

            _context.Trips.Update(oldTrip);
            await _context.SaveChangesAsync();
            return Ok(_converterHelper.ToTripResponse(oldTrip));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var trip = await _context.Trips
                .Include(p => p.Viatics)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (trip == null)
            {
                return this.NotFound();
            }

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
            return Ok("Trip deleted");
        }
    }
}
