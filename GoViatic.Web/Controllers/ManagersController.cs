using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoViatic.Web.Data;
using GoViatic.Web.Data.Entities;
using GoViatic.Web.Helpers;
using GoViatic.Web.Models;

namespace GoViatic.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;

        public ManagersController(
            DataContext dataContext,
            IUserHelper userHelper,
            IMailHelper mailHelper)
        {
            _context = dataContext;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
        }

        public IActionResult Index()
        {
            return View(_context.Managers.Include(m => m.User));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await AddUser(model);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "This email is already used.");
                    return View(model);
                }

                var manager = new Manager { User = user };

                _context.Managers.Add(manager);
                await _context.SaveChangesAsync();

                var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                var tokenLink = Url.Action("ConfirmEmail", "Account", new
                {
                    userid = user.Id,
                    token = myToken
                }, protocol: HttpContext.Request.Scheme);

                _mailHelper.SendMail(model.Username, "Email confirmation",
                    $"<p>&nbsp;</p>" +
                    $"<table style='max-width: 600px; padding: 10px; margin: 0 auto; border-collapse: collapse;'>" +
                    $"<tbody>" +
                    $"<tr>" +
                    $"<td style='background-color: #247d4d; text-align: center; padding: 0;'>&nbsp;</td>" +
                    $"</tr>" +
                    $"<tr>" +
                    $"<td style='background-color: #ecf0f1;'><br />" +
                    $"<div style='color: #34495e; margin: 4% 10% 2%; text-align: justify; font-family: sans-serif;'><br />" +
                    $"<h1 style='color: #e67e22; margin: 0 0 7px;'><span style='color: #247d4d;'>Hola</span></h1>" +
                    $"Bienvenido a GoViatic, es hora de comenzar a viajar y registrar sin problemas tus gastos de viaje:<br />" +
                    $"<h2 style='color: #247d4d; margin: 0 0 7px;'>Email Confirmation</h2>" +
                    $"To allow the user, please click in this link:</div>" +
                    $"<div style='color: #34495e; margin: 4% 10% 2%; font-family: sans-serif; text-align: center;'><a style='text-decoration: none; border-radius: 5px; padding: 11px 23px; color: white; background-color: #247d4d;' href=\"{tokenLink}\">Confirm Email</a> <br />" +
                    $"<p style='color: #b3b3b3; font-size: 12px; text-align: center; margin: 30px 0 0;'>GoViatic by GEOJOR.CO</p>" +
                    $"</div>" +
                    $"</td>" +
                    $"</tr>" +
                    $"</tbody>" +
                    $"</table>" +
                    $"<p>&nbsp;</p>");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private async Task<User> AddUser(AddUserViewModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Company = model.Company,
                Email = model.Username,
                UserName = model.Username
            };

            var result = await _userHelper.AddUserAsync(user, model.Password);
            if (result != IdentityResult.Success)
            {
                return null;
            }

            var newUser = await _userHelper.GetUserByEmailAsync(model.Username);
            await _userHelper.AddUserToRoleAsync(newUser, "Manager");
            return newUser;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                FirstName = manager.User.FirstName,
                LastName = manager.User.LastName,
                Company = manager.User.Company,
                Id = manager.Id,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var traveler = await _context.Travelers
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == model.Id);

                traveler.User.FirstName = model.FirstName;
                traveler.User.LastName = model.LastName;
                traveler.User.Company = model.Company;
                await _userHelper.UpdateUserAsync(traveler.User);
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

            var manager = await _context.Managers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }
            _context.Managers.Remove(manager);
            //await _userHelper.DeleteUserAsync(manager.User.Id);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerExists(int id)
        {
            return _context.Managers.Any(e => e.Id == id);
        }
    }
}
