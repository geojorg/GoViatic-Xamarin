using GoViatic.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GoViatic.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;

        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckViaticTypesAsync();
            await CheckTravelerAsync();
            await CheckViaticAsync();
        }

        private async Task CheckViaticAsync()
        {
            if (_context.Viatics.Any())
            {
                AddViatic("Gasolina Extra","Tanqueo de 8 Galones");
                AddViatic("Almuerzo Creppes and Waffles","Almuerzo para invitar a cliente");
                await _context.SaveChangesAsync();
            }
        }
        private void AddViatic(string viaticName, string description)
        {
            _context.Viatics.Add(new Viatic
            {
                ViaticName = viaticName,
                Description = description,
            });
        }

        private async Task CheckTravelerAsync()
        {
            if (_context.Travelers.Any())
            {
                AddTraveler("Jorge Guerrero","GeojorgCO");
                AddTraveler("Andres Montes", "GeojorgCO");
                await _context.SaveChangesAsync();
            }
        }
        private void AddTraveler(string name, string company)
        {
            _context.Travelers.Add(new Traveler
            {
                Name = name,
                Company = company,
            });
        }

        private async Task CheckViaticTypesAsync()
        {
            if (_context.ViaticTypes.Any())
            {
                AddViaticType("Food");
                AddViaticType("Fuel");
                AddViaticType("Parking");
                AddViaticType("Personal Charges");
                AddViaticType("Phone");
                AddViaticType("Transport");
                AddViaticType("Lodging");
                AddViaticType("Others");
                await _context.SaveChangesAsync();
            }
        }
        private void AddViaticType(string concept)
        {
            _context.ViaticTypes.Add(new ViaticType
            {
                Concept = concept
            });
        }
    }
}

