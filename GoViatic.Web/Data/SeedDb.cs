using GoViatic.Web.Data.Entities;
using GoViatic.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoViatic.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            DataContext context,
            IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRoles();
            await CheckViaticAsync();

            var manager = await CheckUserAync("Jorge Guerrero", "geojorg@gmail.com", "78305713", "GeojorgCO", "Manager");
            var traveler = await CheckUserAync("Andres Guerrero", "jorge.guerrero.montes@gmail.com", "78305713", "GeojorgCO", "Traveler");
            await CheckTravelerAsync(traveler);
            await CheckManagerAsync(manager);
        }

        private async Task<User> CheckUserAync(string firstname, string email, string document, string company, string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstname,
                    Email = email,
                    UserName = email,
                    Document = document,
                    Company = company
                };
                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);
            }
            return user;
        }

        private async Task CheckManagerAsync(User user)
        {
            if (!_context.Managers.Any())
            {
                _context.Managers.Add(new Manager { User = user });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Manager");
            await _userHelper.CheckRoleAsync("Traveler");
        }

        private async Task CheckViaticAsync()
        {
            if (!_context.Viatics.Any())
            {
                AddViatic("Gasolina Extra","Tanqueo de 8 Galones", "Fuel", 200);
                AddViatic("Almuerzo Creppes and Waffles","Almuerzo para invitar a cliente","Food" ,400);
                await _context.SaveChangesAsync();
            }
        }
        private void AddViatic(string viaticName, string description, string viaticType, decimal invoiceAmmount)
        {
            _context.Viatics.Add(new Viatic
            {
                InvoiceDate = DateTime.Today,
                ViaticName = viaticName,
                Description = description,
                ViaticType = viaticType,
                InvoiceAmmount = invoiceAmmount
            });
        }

        private async Task CheckTravelerAsync(User user)
        {
            if (!_context.Travelers.Any())
            {
                _context.Travelers.Add(new Traveler { User = user });
                await _context.SaveChangesAsync();
            }
        }
    }
}

