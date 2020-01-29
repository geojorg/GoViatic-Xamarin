using GoViatic.Web.Data;
using GoViatic.Web.Data.Entities;
using GoViatic.Web.Helpers;
using GoViatic.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GoViatic.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public HomeController(DataContext context, IImageHelper imageHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }

        //CONTROLLER INFORMATION

        [Authorize(Roles = "Traveler")]
        public IActionResult Trips()
        {
            return View(_context.Trips
                .Include(p => p.Viatics)
                .Where(p => p.Traveler.User.Email.ToLower().Equals(User.Identity.Name.ToLower())));
        }

        [Authorize(Roles = "Traveler")]
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

        [Authorize(Roles = "Traveler")]
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

            var model = new TripViewModel
            {
                Id = trip.Id,
                City = trip.City,
                Budget = trip.Budget,
                Date = trip.Date,
                EndDate = trip.EndDate,
                Viatics = trip.Viatics,
                TravelerId = trip.Traveler.Id,
            };
            return View(model);
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
                    Budget = model.Budget,
                    Date = model.Date,
                    EndDate = model.EndDate,
                    Traveler = await _context.Travelers.FindAsync(model.TravelerId)
                };
                _context.Trips.Update(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Trips));
            }
            return View(model);
        }

        [Authorize(Roles = "Traveler")]
        public async Task<IActionResult> Create()
        {
            var traveler = await _context.Travelers
                .FirstOrDefaultAsync(o => o.User.Email.ToLower().Equals(User.Identity.Name.ToLower()));
            if (traveler == null)
            {
                return NotFound();
            }
            var model = new TripViewModel
            {
                TravelerId = traveler.Id,
                Date = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TripViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trip = new Trip
                {
                    City = model.City,
                    Budget = model.Budget,
                    Date = model.Date,
                    EndDate = model.EndDate,
                    Traveler = await _context.Travelers.FindAsync(model.TravelerId),
                };
                _context.Trips.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Trips)}");
            }
            return View(model);
        }

        // TODO: CREATE VIATIC
        public async Task<IActionResult> Viatics(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var trip = await _context.Trips.FindAsync(id.Value);
            if (trip == null)
            {
                return NotFound();
            }
            var model = new ViaticViewModel
            {
                TripId = trip.Id,
                InvoiceDate = DateTime.Today
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Viatics(ViaticViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;
                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile);
                }
                var viatic = await _converterHelper.ToViaticAsync(model, path, true);
                _context.Viatics.Add(viatic);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Home", new { id = model.TripId });
            }
            return View(model);
        }

        public async Task<IActionResult> ViaticEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viatic = await _context.Viatics
                .Include(v => v.Trip)
                .FirstOrDefaultAsync(v => v.Id == id);
            if (viatic == null)
            {
                return NotFound();
            }
            return View(_converterHelper.ToViaticViewModel(viatic));
        }

        [HttpPost]
        public async Task<IActionResult> ViaticEdit(ViaticViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = model.ImageUrl;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile);
                }

                var viatic = await _converterHelper.ToViaticAsync(model, path, false);
                _context.Viatics.Update(viatic);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Home", new { id = model.TripId });
            }
            return View(model);
        }

        [Authorize(Roles = "Traveler")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .Include(p => p.Viatics)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Trips));
        }

        public async Task<IActionResult> DeleteViatic(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viatic = await _context.Viatics
                .Include(t => t.Trip)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viatic == null)
            {
                return NotFound();
            }
            _context.Viatics.Remove(viatic);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Home", new { id = viatic.Trip.Id });
        }
    }
}
