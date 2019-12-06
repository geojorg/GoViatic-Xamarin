using GoViatic.Web.Data;
using GoViatic.Web.Data.Entities;
using GoViatic.Web.Helpers;
using GoViatic.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GoViatic.Web.Controllers
{
    [Authorize(Roles ="Manager")]
    public class TravelersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;

        public TravelersController(DataContext context, IUserHelper userHelper, IComboHelper comboHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
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

        public async Task<IActionResult> EditTraveler(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traveler = await _context.Travelers.FindAsync(id);
            if (traveler == null)
            {
                return NotFound();
            }
            return View(traveler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTraveler(int id, [Bind("Id")] Traveler traveler)
        {
            if (id != traveler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(traveler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelerExists(traveler.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexTraveler));
            }
            return View(traveler);
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
                .ThenInclude(v => v.ViaticType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (traveler == null)
            {
                return NotFound();
            }
            return View(traveler);
        }

        public async Task<IActionResult> DeleteTraveler(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traveler = await _context.Travelers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (traveler == null)
            {
                return NotFound();
            }

            return View(traveler);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var traveler = await _context.Travelers.FindAsync(id);
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
                Date = DateTime.Today
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
                return RedirectToAction($"DetailsTraveler/{model.TravelerId}");
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

            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTrip(int id, [Bind("Id")] Trip trip)
        {
            if (id != trip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelerExists(trip.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexTraveler));
            }
            return View(trip);
        }

        public async Task<IActionResult> DeleteTrip(int? id)
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

            return View(trip);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTripConfirmed(int id)
        {
            var trip = await _context.Travelers.FindAsync(id);
            _context.Travelers.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexTraveler));
        }
























        private bool TravelerExists(int id)
        {
            return _context.Travelers.Any(e => e.Id == id);
        }


        


        //ADD VIATIC
        public async Task<IActionResult> AddViatic(int? id)
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
                ViaticTypes = _comboHelper.GetComboViaticTypes()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddViatic(ViaticViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null)
                    {
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";
                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\image\\Invoices",
                            file);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(stream);
                        }
                        path = $"~/image/Invoices/{file}";
                    }
                var viatic = await _converterHelper.ToViaticAsync(model, path);
                _context.Viatics.Add(viatic);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{model.TripId}");
            }
            return View(model);
        }
    }
}
