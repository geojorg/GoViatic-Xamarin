using GoViatic.Web.Data;
using GoViatic.Web.Data.Entities;
using GoViatic.Web.Helpers;
using GoViatic.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoViatic.Web.Controllers
{
    [Authorize(Roles ="Manager")]
    public class TravelersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public TravelersController(DataContext context, IUserHelper userHelper, IConverterHelper converterHelper, IImageHelper imageHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
        }
        
        public IActionResult CreateTraveler()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTraveler(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FirstName = model.FirstName,
                    Email = model.Username,
                    Document = model.Document,
                    Company = model.Company,
                    UserName = model.Username
                };

                var response = await _userHelper.AddUserAsync(user, model.Password);
                if (response.Succeeded)
                {
                    var userInDB = await _userHelper.GetUserByEmailAsync(model.Username);
                    await _userHelper.AddUserToRoleAsync(userInDB, "Traveler");
                    var traveler = new Traveler
                    {
                        Trips = new List<Trip>(),
                        Viatics = new List<Viatic>(),
                        User = userInDB
                    };
                    _context.Travelers.Add(traveler);

                    try
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(IndexTraveler));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.ToString());
                        return View(model);
                    }
                }
                ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
            }
            return View(model);
        }

        public IActionResult IndexTraveler()
        {
            return View(_context.Travelers
                .Include(t => t.User)
                .Include(t => t.Trips));
        }

        public async Task<IActionResult> DetailsTraveler(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var traveler = await _context.Travelers
                .Include(t => t.User)
                .Include(t => t.Trips)
                .ThenInclude(v => v.Viatics)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (traveler == null)
            {
                return NotFound();
            }
            return View(traveler);
        }
        public async Task<IActionResult> EditTraveler(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traveler = await _context.Travelers
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id.Value);
            if (traveler == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Company = traveler.User.Company,
                Document = traveler.User.Document,
                FirstName = traveler.User.FirstName,
                Id = traveler.Id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTraveler(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var traveler = await _context.Travelers
                    .Include(t => t.User)
                    .FirstOrDefaultAsync(t => t.Id == model.Id);

                traveler.User.FirstName = model.FirstName;
                traveler.User.Document = model.Document;
                traveler.User.Company = model.Company;
                await _userHelper.UpdateUserAsync(traveler.User);
                return RedirectToAction(nameof(IndexTraveler));
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteTraveler(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traveler = await _context.Travelers
                .Include(t =>t.User)
                .Include(t =>t.Trips)
                .Include(t=> t.Viatics)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (traveler == null)
            {
                return NotFound();
            }
            if (traveler.Trips.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "The Traveler can't be remove");
                return RedirectToAction(nameof(IndexTraveler));
            }
            await _userHelper.DeleteUserAsync(traveler.User.Email);
            _context.Travelers.Remove(traveler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexTraveler));
        }

        public async Task<IActionResult> CreateTrip(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var traveler = await _context.Travelers.FindAsync(id.Value);
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
        public async Task<IActionResult> CreateTrip(TripViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trip = await _converterHelper.ToTripAsync(model, true);
                _context.Trips.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction("DetailsTraveler", "Travelers", new { id = model.TravelerId });
            };
            return View(model);
        }

        public async Task<IActionResult> DetailsTrip(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .Include(t => t.Traveler)
                .Include(t => t.Viatics)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (trip == null)
            {
                return NotFound();
            }
            
            return View(trip);
        }

        public async Task<IActionResult> EditTrip(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .Include(t => t.Traveler)
                .Include(t => t.Viatics)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(_converterHelper.ToTripViewModel(trip));
        }

        [HttpPost]
        public async Task<IActionResult> EditTrip(TripViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trip = await _converterHelper.ToTripAsync(model, false);
                _context.Trips.Update(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction("DetailsTraveler", "Travelers", new {id = model.TravelerId});
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteTrip(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .Include(r => r.Traveler)
                .Include(r => r.Viatics)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }
            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction("DetailsTraveler", "Travelers", new { id = trip.Traveler.Id });
        }

        public async Task<IActionResult> CreateViatic(int? id)
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
        public async Task<IActionResult> CreateViatic(ViaticViewModel model)
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
                return RedirectToAction("DetailsTrip", "Travelers", new { id = model.TripId });
            }
            return View(model);
        }

        public async Task<IActionResult> EditViatic(int? id)
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
        public async Task<IActionResult> EditViatic(ViaticViewModel model)
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
                return RedirectToAction("DetailsTrip", "Travelers", new { id = model.TripId });
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteViatic(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viatic = await _context.Viatics
                .Include(t =>t.Trip)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viatic == null)
            {
                return NotFound();
            }
            _context.Viatics.Remove(viatic);
            await _context.SaveChangesAsync();
            return RedirectToAction("DetailsTrip", "Travelers", new { id = viatic.Trip.Id });
        }
    }
}
