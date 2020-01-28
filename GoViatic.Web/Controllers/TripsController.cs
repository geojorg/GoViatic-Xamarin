using GoViatic.Web.Data;
using GoViatic.Web.Data.Entities;
using GoViatic.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GoViatic.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class TripsController : Controller
    {
        private readonly DataContext _context;

        public TripsController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Trips
                .Include(p => p.Traveler)
                .ThenInclude(o => o.User)
                .Include(p => p.Viatics));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .Include(p => p.Traveler)
                .ThenInclude(o => o.User)
                .Include(p => p.Viatics)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .Include(p => p.Traveler)
                .Include(p => p.Viatics)
                .FirstOrDefaultAsync(p => p.Id == id.Value);
            if (trip == null)
            {
                return NotFound();
            }

            var view = new TripViewModel
            {
                Id = trip.Id,
                City = trip.City,
                Bugdet = trip.Bugdet,
                Date = trip.Date,
                EndDate = trip.EndDate,
                TravelerId = trip.Traveler.Id,
            };

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TripViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trip = new Trip
                {
                    Id = model.Id,
                    City = model.City,
                    Bugdet = model.Bugdet,
                    Date = model.Date,
                    EndDate = model.EndDate,
                };

                _context.Trips.Update(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }
            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
