using GoViatic.Web.Data.Entities;
using GoViatic.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoViatic.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            DataContext context,
            IUserHelper userHelper)
        {
            _dataContext = context;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRoles();
            await CheckViaticTypesAsync();
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
            if (!_dataContext.Managers.Any())
            {
                _dataContext.Managers.Add(new Manager { User = user });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Manager");
            await _userHelper.CheckRoleAsync("Traveler");
        }

        private async Task CheckViaticAsync()
        {
            if (!_dataContext.Viatics.Any())
            {
                AddViatic("Gasolina Extra","Tanqueo de 8 Galones");
                AddViatic("Almuerzo Creppes and Waffles","Almuerzo para invitar a cliente");
                await _dataContext.SaveChangesAsync();
            }
        }
        private void AddViatic(string viaticName, string description)
        {
            _dataContext.Viatics.Add(new Viatic
            {
                ViaticName = viaticName,
                Description = description,
            });
        }

        private async Task CheckTravelerAsync(User user)
        {
            if (!_dataContext.Travelers.Any())
            {
                _dataContext.Travelers.Add(new Traveler { User = user });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckViaticTypesAsync()
        {
            if (!_dataContext.ViaticTypes.Any())
            {
                AddViaticType("Food");
                AddViaticType("Fuel");
                AddViaticType("Parking");
                AddViaticType("Personal Charges");
                AddViaticType("Phone");
                AddViaticType("Transport");
                AddViaticType("Lodging");
                AddViaticType("Others");
                await _dataContext.SaveChangesAsync();
            }
        }
        private void AddViaticType(string concept)
        {
            _dataContext.ViaticTypes.Add(new ViaticType
            {
                Concept = concept
            });
        }
    }
}

